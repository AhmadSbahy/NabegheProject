using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Application.Extensions;
using Nabeghe.Application.Services.Implementation;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.Models.Order;
using Nabeghe.Domain.Shared;
using Nabeghe.Domain.ViewModels.Order;
using Nabeghe.Domain.ViewModels.User;
using Nabeghe.Infra.Data.Context;
using Nabeghe.Web.Extensions;

namespace Nabeghe.Web.Controllers
{
	[Authorize]
	public class OrderController(IOrderService orderService,NabegheContext context,ICourseService courseService) : SiteBaseController
	{
		#region List

		[HttpGet("/basket")]
		public async Task<IActionResult> List()
		{
			int userId = User.GetUserId();

			var order = await orderService.GetBasketByUserIdAsync(userId);

			if (order == null || order.OrderDetails.Count <=0)
				return View("NoItemInBasket");

			return View(order);
		}

		#endregion

		#region AddToOrder

		[HttpPost]
		public async Task<IActionResult> AddToOrder(AddToOrderViewModel model)
		{
			int userId = User.GetUserId();
			var courseSlug = courseService.GetCourseSlugByIdAsync(model.CourseId) ?? string.Empty;

			try
			{
				await orderService.AddCourseToOrderAsync(userId, model.CourseId);
				return RedirectToAction(nameof(List));
			}
			catch (InvalidOperationException ex)
			{
				TempData["WarningMessage"] = ex.Message;
				return RedirectToAction("Details", "Course", new { slug = courseSlug });
			}
			catch (KeyNotFoundException ex)
			{
				TempData["ErrorMessage"] = ex.Message;
				return RedirectToAction("Details", "Course", new { slug = courseSlug });
			}
		}

		#endregion

		#region Delete Course From Order

		public async Task<IActionResult> Delete(DeleteCourseFromOrderViewModel model)
		{
			var result = await orderService.DeleteOrder(model);
			switch (result)
			{
				case DeleteCourseFromOrderStatus.Success:
					TempData[SuccessMessage] = "حذف دوره از سبد خرید با موفقیت انجام شد";
					return RedirectToAction(nameof(List));

				case DeleteCourseFromOrderStatus.NotFound:
					TempData[ErrorMessage] = ErrorMessages.CourseNotFound;
					break;
			}

			return RedirectToAction(nameof(List));
		}

		#endregion

		#region Pay order

		public async Task<IActionResult> PayOrder(int id)
		{
			var order = await context.Orders
				.Include(o => o.OrderDetails)
				.FirstOrDefaultAsync(o => o.Id == id);

			if (order == null)
			{
				TempData[ErrorMessage] = "سبد خرید پیدا نشد.";
				return Redirect(nameof(List));
			}


			return RedirectToAction("StartPayByNovino", "Payment", new { area = "", orderId = order.Id });
		}

		#endregion
	}
}
