using Microsoft.AspNetCore.Mvc;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.ViewModels.ContactUs;
using Nabeghe.Web.Extensions;

namespace Nabeghe.Web.Controllers
{
    public class ContactUsController(IContactUsService contactUsService) : SiteBaseController
    {
        #region Contact Us
        [HttpGet("contact-us")]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost("contact-us")]
        public async Task<IActionResult> ContactUs(CreateContactUsViewModel model)
        {
            #region Validations

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            model.Ip = HttpContext.GetUserIp();
            
            await contactUsService.CreateContactUsAsync(model);

            TempData[SuccessMessage] = "تماس با مای شما با موفقیت ثبت شد";
            return RedirectToAction(nameof(ContactUs));
        }

        #endregion
    }
}
