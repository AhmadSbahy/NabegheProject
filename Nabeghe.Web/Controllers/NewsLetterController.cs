using Microsoft.AspNetCore.Mvc;
using Nabeghe.Application.Services.Implementation;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Shared;
using Nabeghe.Domain.ViewModels.ContactUs;
using Nabeghe.Domain.ViewModels.NewsLetter;
using Nabeghe.Web.Areas.Admin.Controllers;
using Nabeghe.Web.Extensions;

namespace Nabeghe.Web.Controllers
{
    public class NewsLetterController(IContactUsService contactUsService) : SiteBaseController
    {
		#region News Letter

		[HttpPost]
		public async Task<IActionResult> NewsLetter(CreateNewsLetterViewModel model)
		{
			#region Validations

			if (!ModelState.IsValid)
			{
				TempData[ErrorMessage] = "لطفا مقادیر الزامی را به درستی وارد کنید";
				return Redirect("/");
			}

			#endregion

			model.Ip = HttpContext.GetUserIp();

			var result = await contactUsService.CreateNewsLetterAsync(model);

			switch (result)
			{
				case CreateNewsLetterStatus.Success:
					TempData[SuccessMessage] = "ایمیل شما با موفقیت در خبر نامه ثبت شد";
					return Redirect("/");

				case CreateNewsLetterStatus.Duplicated:
					TempData[ErrorMessage] = "ایمیل شما قبلا در خبر نامه ثبت شده";
					return Redirect("/");

			}

			return Redirect("/");
		}


		#endregion
	}
}
