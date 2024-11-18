using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Nabeghe.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Error404()
		{
			return View();
		}
		[HttpGet("about-us")]
		public IActionResult AboutUs()
		{
			return View();
		}
        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/CkeditorFiles",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }



            var url = $"{"/CkeditorFiles/"}{fileName}";


            return Json(new { uploaded = true, url });
        }
    }
}
