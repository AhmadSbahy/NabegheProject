using Nabeghe.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Models.CourseCategory;
using Nabeghe.Domain.ViewModels.CourseCategory;
using Nabeghe.Domain.ViewModels.CourseStatus;

namespace Nabeghe.Domain.Interfaces
{
	public interface ICourseCategoryRepository
	{
		Task InsertAsync(CourseCategory courseCategory);
		Task SaveAsync();
        void Update(CourseCategory courseCategory);
    	Task<CourseCategory?> GetByIdAsync(int id);
	    Task<List<CourseCategoryViewModel>> GetAllParentsAsync();
	    Task<FilterCourseCategoryViewModel> FilterCourseCategoryAsync(FilterCourseCategoryViewModel model);
	    Task<List<CourseCategoryViewModel>> GetAllChildCategoriesAsync();
        Task<List<CourseStatusViewModel>> GetAllCourseStatusAsync();
    }
}
