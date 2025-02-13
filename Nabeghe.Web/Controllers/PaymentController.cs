﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Application.Extensions;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.DTOs.NovinoPay;
using Nabeghe.Domain.Models.Order;
using Nabeghe.Domain.ViewModels.Payment;
using Nabeghe.Infra.Data.Context;
using Nabeghe.Web.Controllers;

namespace Eshop.Web.Controllers
{
	public class PaymentController
		(INovinoService novinoService,
		NabegheContext context,
		ICourseService courseService,
		IHttpContextAccessor contextAccessor
		)
		: SiteBaseController
	{
		[Authorize]
		public async Task<IActionResult> StartPayByNovino(int orderId)
		{
			Order? order = null;
			int price = 0;
			string invoiceId = "";

			if (orderId > 0)
			{
				order = await context.Orders
				.Include(o => o.OrderDetails)
				.ThenInclude(o => o.Course)
				.ThenInclude(o => o.CourseDiscount)
				.FirstOrDefaultAsync(o => o.Id == orderId);

				if (order == null)
					return NotFound();

				int totalPriceWithDiscount = order.OrderDetails.Sum(od =>
				{
					if (courseService.IsCourseHasDiscount(od.Course.CourseDiscount))
					{
						return od.Price - (od.Price * od.Course.CourseDiscount.DiscountPercent / 100);
					}
					return od.Price;
				});
				

				price = totalPriceWithDiscount * 10;
				
				invoiceId = $"order_{order.Id}";
			}
			else
			{
				return NotFound();
			}

			var user = await context.Users
				.FirstOrDefaultAsync(u => u.Id == User.GetUserId());

			#region Send request to payment gateway

			string domainName = contextAccessor.HttpContext?.Request.Host.Value ?? String.Empty ;

			var result = await novinoService.CreateRequestAsync(new NovinoGetPaymentUrlRequestDto()
			{
				MerchantId = "test",
				Amount = price,
				CallbackUrl = $"https://{domainName}/payment/NovinoCallback?orderId={order.Id}",
				Description = "خرید دوره",
				InvoiceId = invoiceId,
				CallbackMethod = "POST",
				Email = user.Email,
				Mobile = user.Mobile,
				Name = $"{user.FirstName} {user.LastName}"
			});

			#endregion


			if (result.Status != "100")
			{

				TempData[ErrorMessage] = "عملیات پرداخت با شکست مواجه شد لطفا دقایقی دیگر تلاش کنید.";
				return RedirectToAction("Index", "Home");
			}

			#region Save authority

			if (order != null)
			{
				order.Authority = result.Data.Authority;
                order.TotalOrderPrice = price;
                context.Orders.Update(order);
				await context.SaveChangesAsync();
			}

			#endregion

			return Redirect(result.Data.PaymentUrl);
		}

		[HttpPost]
		public async Task<IActionResult> NovinoCallback(string paymentStatus, string authority, string invoiceID, int orderId)
		{
			if (paymentStatus.ToLower() == "ok")
			{
				string correctAuthority = "";
				int price = 0;

				if (orderId > 0)
				{
					var order = await context.Orders
						.Include(o=>o.OrderDetails)
						.ThenInclude(o=>o.Course)
						.ThenInclude(o=>o.CourseDiscount)
						 .FirstOrDefaultAsync(w => w.Id == orderId);

					if (order != null)
					{
						int totalPriceWithDiscount = order.OrderDetails.Sum(od =>
						{
							if (od.Course.CourseDiscount != null && od.Course.CourseDiscount.StartDate <= DateTime.Now &&
							    od.Course.CourseDiscount.EndDate >= DateTime.Now && !od.Course.CourseDiscount.IsDeleted)
							{
								return od.Price - (od.Price * od.Course.CourseDiscount.DiscountPercent / 100);
							}
							return od.Price;
						});

						price = totalPriceWithDiscount * 10;

						correctAuthority = order.Authority;
					}
				}
				else
				{
					return NotFound();
				}

				var result = await novinoService.VerifyAsync(new NovinoVerifyPaymentRequestDto()
				{
					Amount = price,
					Authority = correctAuthority,
					MerchantId = "test"
				});

				if (result.Status != "100")
				{
					return View("ErrorPayment", new ErrorPaymentViewModel()
					{
						Message = "خطایی رخ داده است. لطفا از طریق تیکت به پشتیبانی اعلام کنید."
					});
				}
				if (orderId > 0)
				{
					var order = await context.Orders
						.Include(o => o.OrderDetails)
						.FirstOrDefaultAsync(o => o.Id == orderId);

					#region Order

					order.IsFinally = true;
					order.RefId = result.Data.RefId;
					context.Orders.Update(order);
					await context.SaveChangesAsync();

					#endregion

					TempData[SuccessMessage] = "پرداخت شما با موفقیت انجام شد..";
					return RedirectToAction("Index", "Home", new { area = "" });
				}
			}
			else
			{
				TempData[ErrorMessage] = "عملیات پرداخت با خطا مواجه شد. لطفا به پشتیبانی اطلاع دهید.";
				return RedirectToAction("Index","Home",new{area=""});
			}

			TempData[ErrorMessage] = "عملیات پرداخت با خطا مواجه شد. لطفا به پشتیبانی اطلاع دهید.";
			return RedirectToAction("Index", "Home", new { area = "" });
		}
	}
}
