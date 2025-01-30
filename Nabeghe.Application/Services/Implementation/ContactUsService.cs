using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Models.ContactUs;
using Nabeghe.Domain.ViewModels.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Application.Senders.Interfaces;
using Nabeghe.Domain.Interfaces;

namespace Nabeghe.Application.Services.Implementation
{
	public class ContactUsService(IUserRepository _userRepository,IEmailSender emailSender) : IContactUsService
	{
		public async Task CreateContactUsAsync(CreateContactUsViewModel model)
		{
			var contactUs = new ContactUs()
			{
				CreateDate = DateTime.Now,
				Email = model.Email,
				Ip = model.Ip,
				Mobile = model.Mobile,
				FullName = model.FullName,
				Message = model.Message
			};
			await _userRepository.InsertContactUsAsync(contactUs);
			await _userRepository.SaveAsync();
		}

		public async Task<FilterContactUsViewModel> GetContactUsListAsync(FilterContactUsViewModel model)
		{
			return await _userRepository.GetContactUsListAsync(model);
		}

		public async Task<ContactUsViewModel?> GetContactUsByIdAsync(int id)
		{
			return await _userRepository.GetContactUsByIdAsync(id);
		}

		public async Task<(string Message, int StatusCode)> CreateAnswerAsync(CreateAnswerContactUsViewModel model, int userId)
		{
			#region Validations

			if (model.Id <= 0 || string.IsNullOrWhiteSpace(model.Answer))
				return ("مقادیر مدنظر را وارد کنید.", 101);

			#endregion

			var contactUs = await GetContactUsByIdAsync(model.Id);

			if (contactUs == null)
				return ("تماس با ما پیدا نشد.", 101);

			contactUs.Answer = model.Answer;
			contactUs.AnswerUserId = userId;

			await _userRepository.UpdateContactUs(contactUs.Id,contactUs);
			await _userRepository.SaveAsync();

			string body = $@"
<h3>پاسخ به تماس با ما  به شرح زیر می باشد.</h3>
<p>پاسخ: {contactUs.Answer}</p>

";
			#region Send Activation Email
			  emailSender.SendEmail(contactUs.Email, "پاسخ به تماس با ما", body);
			 #endregion
			

			return ("به تماس با ما با موفقیت پاسخ داده شد.", 100);
		}
	}
}
