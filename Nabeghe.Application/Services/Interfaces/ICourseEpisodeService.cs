using Nabeghe.Domain.ViewModels.Course;
using Nabeghe.Domain.ViewModels.CourseEpisode;

namespace Nabeghe.Application.Services.Interfaces
{
	public interface ICourseEpisodeService
	{
		Task<FilterCourseEpisodeViewModel> FilterCourseAsync(FilterCourseEpisodeViewModel model);
		Task<CreateCourseEpisodeResult> CreateAsync(CreateCourseEpisodeViewModel model);
		Task<DeleteCourseEpisodeResult> DeleteAsync(int id);
		Task<UpdateCourseEpisodeViewModel?> GetForEditAsync(int id);
		Task<UpdateCourseEpisodeResult> UpdateAsync(UpdateCourseEpisodeViewModel model);
        Task<string?> GetEpisodeFileNameAsync(int courseId,int episodeId);
    }
}
	