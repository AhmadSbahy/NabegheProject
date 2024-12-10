using Microsoft.AspNetCore.Mvc;
using Nabeghe.Web.Utilities;

namespace Nabeghe.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        //[CheckPermission("AdminSite")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
