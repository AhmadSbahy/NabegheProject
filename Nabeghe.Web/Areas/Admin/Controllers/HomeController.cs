using Microsoft.AspNetCore.Mvc;

namespace Nabeghe.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
