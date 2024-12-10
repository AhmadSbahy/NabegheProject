using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Models.CourseCategory;
using Nabeghe.Domain.ViewModels.CourseCategory;
using Nabeghe.Domain.ViewModels.CourseStatus;

namespace Nabeghe.Application.Services.Interfaces
{
	public interface ICourseCategoryService
	{
		Task<CreateCourseCategoryResult> CreateAsync(CreateCourseCategoryViewModel model);
		Task<List<CourseCategoryViewModel>> GetAllParentsAsync();
		Task<UpdateCourseCategoryViewModel?> GetForEditAsync(int id);
		Task<UpdateCourseCategoryResult> UpdateAsync(UpdateCourseCategoryViewModel model);
		Task<DeleteCourseCategoryResult> DeleteAsync(int id);
		Task<FilterCourseCategoryViewModel> FilterCourseCategoryAsync(FilterCourseCategoryViewModel model);
		Task<List<CourseCategoryViewModel>> GetAllChildCategoriesAsync();
		Task<List<CourseStatusViewModel>> GetAllCourseStatusAsync();
		Task<List<CourseCategory>> GetAllCategoriesAsync();
	}
}
