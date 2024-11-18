using Microsoft.AspNetCore.Mvc;
using Nabeghe.Application.Extensions;

namespace Nabeghe.Web.Areas.UserPanel.Controllers
{
    public class HomeController : UserPanelBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
