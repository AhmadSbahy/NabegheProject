using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Models.ContactUs;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.Models.User;
using Nabeghe.Domain.ViewModels.ContactUs;
using Nabeghe.Domain.ViewModels.Course;
using Nabeghe.Domain.ViewModels.CourseCategory;
using Nabeghe.Domain.ViewModels.CourseComment;
using Nabeghe.Domain.ViewModels.User;

namespace Nabeghe.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByMobileAsync(string mobile);
        Task<EditUserProfileViewModel?> GetProfileForEditAsync(int userId);
        Task<EditUserProfileResult> UpdateProfileAsync(EditUserProfileViewModel model);
        Task<AdminSideCreateUserResult> AdminSideCreateAsync(AdminSideCreateUserViewModel model);
		Task<AdminSideEditUserViewModel?> AdminSideGetForEditAsync(int userId);
		Task<AdminSideEditUserResult> AdminSideUpdateAsync(AdminSideEditUserViewModel model);
		Task<FilterUserViewModel> FilterUserAsync(FilterUserViewModel model);
		Task<DeleteUserResult> DeleteAsync(int id);
        Task<User?> GetByIdAsync(int id);
        Task<List<UserFavoriteCourseViewModel>> GetCourseFavoriteListAsync(int userId);
        Task<List<UserCourseCommentViewModel>> GetUserCourseCommentListAsync(int userId);

	}
}
