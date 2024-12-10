using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.Models.User;
using Nabeghe.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.ViewModels.Course;
using Nabeghe.Domain.ViewModels.CourseDiscount;

namespace Nabeghe.Domain.Interfaces
{
	public interface ICourseRepository
	{
		Task InsertAsync(Course course);
		Task SaveAsync();
		void Update(Course course);
		Task<Course?> GetByIdAsync(int id);
		Task<AdminSideFilterCourseViewModel> FilterCourseAsync(AdminSideFilterCourseViewModel model);
		Task<bool> IsCourseExist(int id);
		Task<ClientSideFilterCourseViewModel> FilterCourseAsync(ClientSideFilterCourseViewModel model);
        Task<Course?> GetByIdWithIncludesAsync(int id);
        Task<Course?> GetBySlugWithIncludesAsync(string slug);
        string GetCourseSlugByIdAsync(int id);
        bool IsUserLikedCourse(int userId, int courseId);
        Task CreateCourseLikeAsync(int userId, int courseId);
        Task DeleteCourseLikeAsync(int userId, int courseId);
        Task<List<Course>> GetLatestCoursesAsync(int count);
	}
}
