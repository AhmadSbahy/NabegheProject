using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.ViewModels.Course;

namespace Nabeghe.Application.Services.Interfaces
{
	public interface ICourseService
	{
		Task<AdminSideFilterCourseViewModel> FilterCourseAsync(AdminSideFilterCourseViewModel model);
		Task<CreateCourseResult> CreateAsync(CreateCourseViewModel model);
		Task<DeleteCourseResult> DeleteAsync(int id);
		Task<UpdateCourseViewModel?> GetForEditAsync(int id);
		Task<UpdateCourseResult> UpdateAsync(UpdateCourseViewModel model);
		Task<bool> IsCourseExist(int id);
		Task<ClientSideFilterCourseViewModel> FilterCourseAsync(ClientSideFilterCourseViewModel model);
		Task<CourseDetailViewModel?> GetCourseForDetailAsync(string slug);
		string GetCourseSlugByIdAsync(int id);
        bool IsUserLikedCourse(int userId, int courseId);
        Task CreateCourseLikeAsync(int userId, int courseId);
        Task DeleteCourseLikeAsync(int userId, int courseId);
        bool IsCourseHasDiscount(CourseDiscount? courseDiscount);
        Task<Course?> GetCourseByIdAsync(int courseId);
	}
}	
	