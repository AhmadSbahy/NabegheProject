using Nabeghe.Domain.ViewModels.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Application.Services.Interfaces
{
	public interface IContactUsService
	{
		Task CreateContactUsAsync(CreateContactUsViewModel model);
		Task<FilterContactUsViewModel> GetContactUsListAsync(FilterContactUsViewModel model);
		Task<ContactUsViewModel?> GetContactUsByIdAsync(int id);
		Task<(string Message, int StatusCode)> CreateAnswerAsync(CreateAnswerContactUsViewModel model, int userId);
	}
}
