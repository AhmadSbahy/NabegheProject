using Nabeghe.Domain.ViewModels.ContactUs;
using Nabeghe.Domain.ViewModels.NewsLetter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Application.Services.Interfaces
{
	public interface IContactUsService
	{
		#region Contact Us

		Task CreateContactUsAsync(CreateContactUsViewModel model);
		Task<FilterContactUsViewModel> GetContactUsListAsync(FilterContactUsViewModel model);
		Task<ContactUsViewModel?> GetContactUsByIdAsync(int id);
		Task<(string Message, int StatusCode)> CreateAnswerAsync(CreateAnswerContactUsViewModel model, int userId);

		#endregion

		#region News Letter

		Task<CreateNewsLetterStatus> CreateNewsLetterAsync(CreateNewsLetterViewModel model);
		Task<List<NewsLetterViewModel>> GetNewsLetterListAsync();
		Task<(string Message, int StatusCode)> SendEmailForNewsLetter(SendEmailForNewsLetterViewModel model);
		#endregion
	}
}
