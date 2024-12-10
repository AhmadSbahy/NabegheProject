using Nabeghe.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Models.User;
using Nabeghe.Infra.Data.Context;
using Nabeghe.Domain.Models.CourseCategory;
using Nabeghe.Domain.ViewModels.CourseCategory;
using Nabeghe.Domain.ViewModels.CourseStatus;

namespace Nabeghe.Infra.Data.Repositories
{
	public class CourseCategoryRepository(NabegheContext _context) : ICourseCategoryRepository
	{
		public async Task InsertAsync(CourseCategory courseCategory)
			=> await _context.AddAsync(courseCategory);

		public async Task SaveAsync()
			=> await _context.SaveChangesAsync();
	
		public void Update(CourseCategory courseCategory)
			=> _context.Update(courseCategory);
		
		public async Task<CourseCategory?> GetByIdAsync(int id)
			=> await _context.CourseCategories.FirstOrDefaultAsync(u => u.Id == id);

		public async Task<List<CourseCategoryViewModel>> GetAllParentsAsync()
		{
			return await _context.CourseCategories.Where(pc => pc.ParentId == null)
				.Select(pc => new CourseCategoryViewModel
				{
					Id = pc.Id,
					Title = pc.Title,
					ParentId = pc.ParentId,
					CreateDate = pc.CreateDate
				})
				.ToListAsync();
		}

		public async Task<FilterCourseCategoryViewModel> FilterCourseCategoryAsync(FilterCourseCategoryViewModel model)
		{
			var query = _context.CourseCategories.AsQueryable();

			#region Filter

			if (!string.IsNullOrEmpty(model.Title))
			{
				query = query.Where(pc => pc.Title.Contains(model.Title));
			}
			switch (model.Status)
			{
				case FilterCourseCategoryStatus.NotDeleted:
					query = query.Where(p => !p.IsDeleted);
					break;
				case FilterCourseCategoryStatus.All:
					query = query;
					break;
				case FilterCourseCategoryStatus.Deleted:
					query = query.Where(p => p.IsDeleted);
					break;
			}
			#endregion

			query = query.OrderByDescending(pc => pc.CreateDate);

			await model.Paging(query.Select(pc => new CourseCategoryViewModel()
			{
				ParentId = pc.ParentId,
				Title = pc.Title,
				CreateDate = pc.CreateDate,
				Id = pc.Id
			}));

			return model;
		}

		public async Task<List<CourseCategoryViewModel>> GetAllChildCategoriesAsync()
		{
			return await _context.CourseCategories.Where(pc => pc.ParentId.HasValue && !pc.IsDeleted)
				.Select(pc => new CourseCategoryViewModel
				{
					Id = pc.Id,
					Title = pc.Title,
					ParentId = pc.ParentId,
					CreateDate = pc.CreateDate
				}).ToListAsync();
		}

        public async Task<List<CourseStatusViewModel>> GetAllCourseStatusAsync()
        {
            return await _context.CourseStatus
                .Select(cs => new CourseStatusViewModel
                {
                    StatusTitle = cs.StatusTitle,
					Id = cs.Id
                }).ToListAsync();
        }
        public async Task<List<CourseCategory>> GetAllCategoriesAsync()
        {
	        return await _context.CourseCategories
		        .Where(c => !c.IsDeleted)
		        .ToListAsync();
        }
	}
}
