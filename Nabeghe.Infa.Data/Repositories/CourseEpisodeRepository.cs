using Nabeghe.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.ViewModels.Course;
using Nabeghe.Domain.ViewModels.CourseEpisode;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Infra.Data.Repositories
{
  public  class CourseEpisodeRepository(NabegheContext _context) : ICourseEpisodeRepository
	{
		public async Task InsertAsync(CourseEpisode courseEpisode)
			=> await _context.AddAsync(courseEpisode);

		public async Task SaveAsync()
			=> await _context.SaveChangesAsync();

		public void Update(CourseEpisode courseEpisode)
			=> _context.Update(courseEpisode);

		public async Task<CourseEpisode?> GetByIdAsync(int id)
			=> await _context.CourseEpisodes.FirstOrDefaultAsync(c => c.Id == id);

		public async Task<FilterCourseEpisodeViewModel> FilterCourseEpisodeAsync(FilterCourseEpisodeViewModel model)
		{
			var query = _context.CourseEpisodes.Where(c=>c.CourseId == model.CourseId).AsQueryable();

			#region Filter

			if (!string.IsNullOrWhiteSpace(model.Param))
			{
				query = query.Where(c => c.EpisodeFileName.Contains(model.Param) || c.EpisodeTitle.Contains(model.Param));
			}

			switch (model.Status)
			{
				case FilterCourseEpisodeStatus.All:
					query = query;
					break;

				case FilterCourseEpisodeStatus.NotDeleted:
					query = query.Where(u => !u.IsDeleted);
					break;

				case FilterCourseEpisodeStatus.Deleted:
					query = query.Where(u => u.IsDeleted);
					break;
			}
			#endregion

			query = query.OrderByDescending(u => u.CreateDate);

			await model.Paging(query.Select(c => new CourseEpisodeViewModel
			{
				CourseId = c.CourseId,
				EpisodeTitle = c.EpisodeTitle,
				EpisodeTime = c.EpisodeTime,
				EpisodeFileName = c.EpisodeFileName,
				CreateDate = c.CreateDate,
				Id = c.Id
			}));

			return model;
		}

		public async Task<List<CourseEpisode>> GetEpisodesAsync()
			=> await _context.CourseEpisodes.ToListAsync();

        public async Task<string?> GetEpisodeFileNameAsync(int courseId, int episodeId)
        {
            return  await _context.CourseEpisodes
                .Where(e => e.Id == episodeId && e.CourseId == courseId && !e.IsDeleted)
                .Select(e => e.EpisodeFileName)
                .FirstOrDefaultAsync();

        }
    }
}
