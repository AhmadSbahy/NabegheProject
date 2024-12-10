using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Application.Extensions;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.ViewModels.ContactUs;
using Nabeghe.Infra.Data.Context;
using Nabeghe.Web.Utilities;

namespace Nabeghe.Web.Areas.Admin.Controllers
{
    public class ContactUsController(IContactUsService contactUsService) : AdminBaseController
    {
		#region List
		[CheckPermission("ManageContactUs")]
		public async Task<IActionResult> Index(FilterContactUsViewModel filter)
		{
			var model = await contactUsService.GetContactUsListAsync(filter);

			return View(model);
		}

		#endregion

		#region Details
		[CheckPermission("ContactUsDetail")]
		public async Task<IActionResult> Details(int id)
		{
			var contactUs = await contactUsService.GetContactUsByIdAsync(id);

			if (contactUs == null)
				return NotFound();

			return View(contactUs);
		}

		#endregion

		#region Answer

		[HttpPost]
		[CheckPermission("AnswerContactUs")]
		public async Task<IActionResult> Answer(CreateAnswerContactUsViewModel model)
		{
			var result = await contactUsService.CreateAnswerAsync(model, User.GetUserId());

			return Ok(new { message = result.Message, status = result.StatusCode });
		}

		#endregion
	}
}
