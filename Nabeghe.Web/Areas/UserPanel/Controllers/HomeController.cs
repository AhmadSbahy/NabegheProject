using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Application.Extensions;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.ViewModels.User;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Web.Areas.UserPanel.Controllers
{
    public class HomeController : UserPanelBaseController
    {
        private readonly IOrderService _orderService;

        public HomeController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            int userId = User.GetUserId();

            var courses = await _orderService.GetUserPurchasedCoursesAsync(userId);

            return View(courses);
        }
    }
}
