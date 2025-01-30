using Nabeghe.Web.Models.CourseStatus;
using Nabeghe.Web.Services.Base;

namespace Nabeghe.Web.Contracts
{
	public interface ICourseStatusService
	{
		Task<List<CourseStatusViewModel>> GetCourseStatusListAsync();
		Task<CourseStatusViewModel> GetCourseStatusDetailAsync(int id);
		Task<Response<int>> CreateCourseStatusAsync(CreateCourseStatusViewModel model);
		Task<Response<int>> UpdateCourseStatusAsync(CourseStatusViewModel model);
		Task<Response<int>> DeleteCourseStatusAsync(int id);
		Task<Response<int>> DeleteCourseStatusAsync(CourseStatusViewModel model);
	}
}
