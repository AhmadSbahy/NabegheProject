using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Models.ContactUs;
using Nabeghe.Domain.ViewModels.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Nabeghe.Application.Senders.Interfaces;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.NewsLetter;
using Nabeghe.Domain.ViewModels.NewsLetter;

namespace Nabeghe.Application.Services.Implementation
{
	public class ContactUsService(IUserRepository _userRepository,IEmailSender emailSender,IMapper mapper) : IContactUsService
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
			#region Send Email
			  emailSender.SendEmail(contactUs.Email, "پاسخ به تماس با ما", body);
			 #endregion
			

			return ("به تماس با ما با موفقیت پاسخ داده شد.", 100);
		}

		public async Task<CreateNewsLetterStatus> CreateNewsLetterAsync(CreateNewsLetterViewModel model)
		{
			if (! await _userRepository.CheckEmailForNewsLatter(model.Email))
			{
				var newsLatter = new NewsLetter()
				{
					Email = model.Email,
					Ip = model.Ip,
					CreateDate = DateTime.Now,
				};
				await _userRepository.InsertNewsLatterAsync(newsLatter);
				await _userRepository.SaveAsync();
				return CreateNewsLetterStatus.Success;
			}
			else
			{
				return CreateNewsLetterStatus.Duplicated;
			}
		}

		public async Task<List<NewsLetterViewModel>> GetNewsLetterListAsync()
		{
			var newsLetterListAsync = await _userRepository.GetNewsLetterListAsync();						
			return mapper.Map<List<NewsLetterViewModel>>(newsLetterListAsync);
		}

		public async Task<(string Message, int StatusCode)> SendEmailForNewsLetter(SendEmailForNewsLetterViewModel model)
		{
			if (string.IsNullOrWhiteSpace(model.Message))
				return ("مقادیر مدنظر را وارد کنید.", 101);


			List<string> emails = await _userRepository.GetEmailsListForNewsLetter();
			#region Send Email

			foreach (var email in emails)
			{
				bool status = emailSender.SendEmail(email,model.Subject, model.Message,model.IsHtml);
				if (!status)
				{
					return ("ارسال ایمیل با خطا مواجه شد", 103);
				}
			}

		
			#endregion

			return ("ایمیل با موفقیت ارسال شد", 100);
		}
	}
}
