using Nabeghe.Domain.Models.User;
using Nabeghe.Domain.ViewModels.CourseCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Models.ContactUs;
using Nabeghe.Domain.ViewModels.ContactUs;
using Nabeghe.Domain.ViewModels.User;
using Nabeghe.Domain.ViewModels.Course;

namespace Nabeghe.Domain.Interfaces
{
	public interface IUserRepository
	{
		Task<bool> IsExitsMobileAsync(string mobile);
		Task InsertAsync(User user);
		Task SaveAsync();
		Task<User?> GetByMobileAndPasswordAsync(string mobile, string password);
		Task<User?> GetByMobileAsync(string mobile);
        void Update(User user);
        Task<User?> GetByMobileAndConfirmCodeAsync(string mobile, string confirmCode);
		Task<User?> GetByIdAsync(int userId);
		Task<bool> DuplicatedMobileAsync(string mobile, int userId);
		Task<FilterUserViewModel> FilterUserAsync(FilterUserViewModel model);
        Task<List<UserFavoriteCourseViewModel>> GetCourseFavoriteListAsync(int userId);
        Task<List<UserCourseCommentViewModel>> GetUserCourseCommentListAsync(int userId);
        string GetUserFullName(int userId);
        Task<bool> IsUserInCourse(int userId, int courseId);

		#region ContactUs

		Task InsertContactUsAsync(ContactUs contactUs);
        Task<FilterContactUsViewModel> GetContactUsListAsync(FilterContactUsViewModel model);
        Task<ContactUsViewModel?> GetContactUsByIdAsync(int id);
		Task UpdateContactUs(int id,ContactUsViewModel model);
        #endregion
	}
}
