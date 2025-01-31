using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.ViewModels.CourseStatus;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Infra.Data.Repositories
{
	public class CourseStatusRepository : ICourseStatusRepository
	{
		private readonly NabegheContext _context;

		public CourseStatusRepository(NabegheContext context)
		{
			_context = context;
		}
		
		public async Task<CourseStatus> GetAsync(int id)
		{
			var courseStatuses = await _context.CourseStatus
				.FirstOrDefaultAsync(cs => cs.Id == id);

			return courseStatuses ?? new CourseStatus();
		}

		public async Task<CourseStatusFilterViewModel> GetAllAsync(CourseStatusFilterViewModel model)
		{
			var query = _context.CourseStatus.AsQueryable();

			#region Filter

			if (!string.IsNullOrWhiteSpace(model.Param))
			{
				query = query.Where(c => c.StatusTitle.Contains(model.Param));
			}

			switch (model.FilterCourseStatusOrder)
			{
				case FilterCourseStatusOrder.Newest:
					query = query.OrderByDescending(cs => cs.CreateDate);
					break;

				case FilterCourseStatusOrder.Oldest:
					query = query.OrderBy(cs => cs.CreateDate);
					break;
			}
			#endregion


			await model.Paging(query.Select(c => new CourseStatusViewModel()
			{
				Id = c.Id,
				StatusTitle = c.StatusTitle,
				CreateDate = c.CreateDate
			}));

			return model;
		}

		public async Task<bool> IsExist(int id)
		{
			bool isExist = await _context.CourseStatus.AnyAsync(cs => cs.Id == id);
			return isExist;
		}

		public async Task<CourseStatus> AddAsync(CourseStatus courseStatus)
		{
			courseStatus.CreateDate = DateTime.Now;
			await _context.CourseStatus.AddAsync(courseStatus);
			await _context.SaveChangesAsync();
			return courseStatus;
		}

		public async Task UpdateAsync(CourseStatus courseStatus)
		{
			_context.CourseStatus.Update(courseStatus);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(CourseStatus courseStatus)
		{
			_context.CourseStatus.Remove(courseStatus);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var courseStatus = await GetAsync(id);
			await DeleteAsync(courseStatus);
		}
	}
}
