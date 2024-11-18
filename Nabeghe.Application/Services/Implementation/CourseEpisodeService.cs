using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Application.Extensions;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Application.Statics;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.ViewModels.Course;
using Nabeghe.Domain.ViewModels.CourseEpisode;
using Nabeghe.Infra.Data.Repositories;

namespace Nabeghe.Application.Services.Implementation
{
	public class CourseEpisodeService(ICourseEpisodeRepository _courseEpisodeRepository) : ICourseEpisodeService
	{
		public async Task<FilterCourseEpisodeViewModel> FilterCourseAsync(FilterCourseEpisodeViewModel model)
		{
			return await _courseEpisodeRepository.FilterCourseEpisodeAsync(model);
		}

		public async Task<CreateCourseEpisodeResult> CreateAsync(CreateCourseEpisodeViewModel model)
		{
			CourseEpisode courseEpisode = new CourseEpisode()
			{
				CreateDate = DateTime.Now,
				IsDeleted = false,
				CourseId = model.CourseId,
				EpisodeTime = model.EpisodeTime,
				EpisodeTitle = model.EpisodeTitle
			};

			#region Add Video

			if (model.EpisodeFile != null)
			{
				string videoName = Guid.NewGuid() + Path.GetExtension(model.EpisodeFile.FileName);
				string path = PathTools.CourseFilePath;
				await model.EpisodeFile.AddVideoToServer(videoName, path);
				courseEpisode.EpisodeFileName = videoName;
			}

			#endregion

			await _courseEpisodeRepository.InsertAsync(courseEpisode);
			await _courseEpisodeRepository.SaveAsync();
			return CreateCourseEpisodeResult.Success;
		}

		public async Task<DeleteCourseEpisodeResult> DeleteAsync(int id)
		{
			var courseEpisode = await _courseEpisodeRepository.GetByIdAsync(id);
			if (courseEpisode == null)
			{
				return DeleteCourseEpisodeResult.NotFound;
			}

			#region Delete Video

			 courseEpisode.EpisodeFileName.DeleteFile(PathTools.CourseFilePath);

			#endregion

			courseEpisode.IsDeleted = true;

			_courseEpisodeRepository.Update(courseEpisode);
			await _courseEpisodeRepository.SaveAsync();

			return DeleteCourseEpisodeResult.Success;
		}

		public async Task<UpdateCourseEpisodeViewModel?> GetForEditAsync(int id)
		{
			var courseEpisode = await _courseEpisodeRepository.GetByIdAsync(id);
			if (courseEpisode == null) return null;

			return new UpdateCourseEpisodeViewModel()
			{
				Id = courseEpisode.Id,
				CourseId = courseEpisode.CourseId,
				EpisodeTime = courseEpisode.EpisodeTime,
				EpisodeTitle = courseEpisode.EpisodeTitle,
				EpisodeVideoName = courseEpisode.EpisodeFileName,
				CreateDate = courseEpisode.CreateDate
			};
		}

		public async Task<UpdateCourseEpisodeResult> UpdateAsync(UpdateCourseEpisodeViewModel model)
		{
			CourseEpisode courseEpisode = new CourseEpisode()
			{
				CreateDate = model.CreateDate,
				IsDeleted = false,
				Id = model.Id,
				CourseId = model.CourseId,
				EpisodeTime = model.EpisodeTime,
				EpisodeTitle = model.EpisodeTitle,
				EpisodeFileName = model.EpisodeVideoName
			};

			#region Add Video

			if (model.NewEpisodeFile != null)
			{
				string videoName = Guid.NewGuid() + Path.GetExtension(model.NewEpisodeFile.FileName);
				string path = PathTools.CourseFilePath;
				await model.NewEpisodeFile.AddVideoToServer(videoName, path, model.EpisodeVideoName);
				courseEpisode.EpisodeFileName = videoName;
			}

			#endregion

			_courseEpisodeRepository.Update(courseEpisode);
			await _courseEpisodeRepository.SaveAsync();
			return UpdateCourseEpisodeResult.Success;
		}

        public async Task<string?> GetEpisodeFileNameAsync(int courseId, int episodeId)
        {
            return await _courseEpisodeRepository.GetEpisodeFileNameAsync(courseId, episodeId);
        }
    }
}
