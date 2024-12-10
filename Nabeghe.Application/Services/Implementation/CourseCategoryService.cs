using Nabeghe.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.CourseCategory;
using Nabeghe.Domain.ViewModels.CourseCategory;
using Nabeghe.Domain.ViewModels.CourseStatus;

namespace Nabeghe.Application.Services.Implementation
{
	public class CourseCategoryService(ICourseCategoryRepository _courseCategoryRepository) : ICourseCategoryService
	{
		public async Task<CreateCourseCategoryResult> CreateAsync(CreateCourseCategoryViewModel model)
		{
			CourseCategory CourseCategory = new CourseCategory()
			{
				CreateDate = DateTime.Now,
				ParentId = model.ParentId,
				Title = model.Title
			};
			await _courseCategoryRepository.InsertAsync(CourseCategory);
			await _courseCategoryRepository.SaveAsync();
			return CreateCourseCategoryResult.Success;
		}
		public async Task<List<CourseCategoryViewModel>> GetAllParentsAsync()
		{
			return await _courseCategoryRepository.GetAllParentsAsync();
		}

		public async Task<UpdateCourseCategoryViewModel?> GetForEditAsync(int id)
		{
			var CourseCategory = await _courseCategoryRepository.GetByIdAsync(id);
			if (CourseCategory == null)
			{
				return null;
			}

			return new UpdateCourseCategoryViewModel()
			{
				Id = CourseCategory.Id,
				Title = CourseCategory.Title,
				ParentId = CourseCategory.ParentId
			};
		}

		public async Task<UpdateCourseCategoryResult> UpdateAsync(UpdateCourseCategoryViewModel model)
		{
			var CourseCategory = await _courseCategoryRepository.GetByIdAsync(model.Id);
			if (CourseCategory == null)
			{
				return UpdateCourseCategoryResult.CourseCategoryNotFound;
			}

			CourseCategory.Title = model.Title;
			CourseCategory.ParentId = model.ParentId;
			_courseCategoryRepository.Update(CourseCategory);
			await _courseCategoryRepository.SaveAsync();
			return UpdateCourseCategoryResult.Success;
		}

		public async Task<DeleteCourseCategoryResult> DeleteAsync(int id)
		{
			var CourseCategory = await _courseCategoryRepository.GetByIdAsync(id);
			if (CourseCategory == null)
			{
				return DeleteCourseCategoryResult.CourseCategoryNotFound;
			}
			CourseCategory.IsDeleted = true;

			_courseCategoryRepository.Update(CourseCategory);
			await _courseCategoryRepository.SaveAsync();

			return DeleteCourseCategoryResult.Success;
		}

		public async Task<FilterCourseCategoryViewModel> FilterCourseCategoryAsync(FilterCourseCategoryViewModel model)
		{
			return await _courseCategoryRepository.FilterCourseCategoryAsync(model);
		}

		public async Task<List<CourseCategoryViewModel>> GetAllChildCategoriesAsync()
		{
			return await _courseCategoryRepository.GetAllChildCategoriesAsync();
		}

        public async Task<List<CourseStatusViewModel>> GetAllCourseStatusAsync()
        {
            return await _courseCategoryRepository.GetAllCourseStatusAsync();
        }
        public Task<List<CourseCategory>> GetAllCategoriesAsync()
        {
	        return _courseCategoryRepository.GetAllCategoriesAsync();
        }
	}
}
