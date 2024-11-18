using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Application.Extensions;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Web.Controllers
{
	public class OrderController(NabegheContext context) : SiteBaseController
	{
		[HttpGet("/basket")]
		public async Task<IActionResult> List()
		{
			int userId = User.GetUserId();

            var order = await context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Course)
                .ThenInclude(o=> o.CourseStatus)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Course)
                .ThenInclude(o => o.CourseDiscount)
				.FirstOrDefaultAsync(o => o.UserId == userId && !o.IsFinally);
            if (order == null)
            {
                return View("NoItemInBasket");
            }
			return View(order);
		}
	}
}
