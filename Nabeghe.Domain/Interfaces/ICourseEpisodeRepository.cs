using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.Models.User;
using Nabeghe.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.ViewModels.Course;
using Nabeghe.Domain.ViewModels.CourseEpisode;

namespace Nabeghe.Domain.Interfaces
{
	public interface ICourseEpisodeRepository
	{
		Task InsertAsync(CourseEpisode courseEpisode);
		Task SaveAsync();
		void Update(CourseEpisode courseEpisode);
		Task<CourseEpisode?> GetByIdAsync(int id);
		Task<FilterCourseEpisodeViewModel> FilterCourseEpisodeAsync(FilterCourseEpisodeViewModel model);
		Task<List<CourseEpisode>> GetEpisodesAsync();
        Task<string?> GetEpisodeFileNameAsync(int courseId,int episodeId);
    }
}
