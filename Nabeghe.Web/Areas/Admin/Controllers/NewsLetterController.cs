using Microsoft.AspNetCore.Mvc;
using Nabeghe.Application.Extensions;
using Nabeghe.Application.Services.Implementation;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.ViewModels.ContactUs;
using Nabeghe.Domain.ViewModels.NewsLetter;
using Nabeghe.Domain.ViewModels.Role;
using Nabeghe.Web.Utilities;

namespace Nabeghe.Web.Areas.Admin.Controllers
{
	public class NewsLetterController(IContactUsService contactUsService) : AdminBaseController
	{
		#region List

		[CheckPermission("ManageBlog")]
		public async Task<IActionResult> Index()
		{
			var model = await contactUsService.GetNewsLetterListAsync();
			return View(model);
		}

		#endregion

		#region Send Email
		[HttpGet]
		public async Task<IActionResult> SendEmail()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendEmail(SendEmailForNewsLetterViewModel model)
		{
			var result = await contactUsService.SendEmailForNewsLetter(model);

			return Ok(new { message = result.Message, status = result.StatusCode });
		}

		#endregion
	}
}
