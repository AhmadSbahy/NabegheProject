using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Application.Extensions;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.ViewModels.Course;
using Nabeghe.Application.Statics;
using Nabeghe.Domain.Models.User;
using Nabeghe.Domain.ViewModels.User;
using Nabeghe.Infra.Data.Repositories;
using Nabeghe.Domain.ViewModels.CourseCategory;

namespace Nabeghe.Application.Services.Implementation
{
	public class CourseService(ICourseRepository _courseRepository) : ICourseService
	{
		public async Task<AdminSideFilterCourseViewModel> FilterCourseAsync(AdminSideFilterCourseViewModel model)
		{
			return await _courseRepository.FilterCourseAsync(model);
		}

		public async Task<CreateCourseResult> CreateAsync(CreateCourseViewModel model)
		{
			Course course = new Course()
			{
				CoursePrice = model.CoursePrice,
				CourseTitle = model.CourseTitle,
				CourseDescription = model.CourseDescription,
				StatusId = model.StatusId,
				CreateDate = DateTime.Now,
				IsDeleted = false,
				CategoryId = model.CategoryId,
				TeacherId = (int)model.TeacherId,
				CourseSubTitle = model.CourseSubTitle,
				Slug = model.Slug
			};

			#region Add Image

			if (model.CourseImage != null)
			{
				string imageName = Guid.NewGuid() + Path.GetExtension(model.CourseImage.FileName);
				string path = PathTools.CourseImagePath;
				model.CourseImage.AddImageToServer(imageName, path);
				course.CourseImageName = imageName;
			}

			#endregion

			await _courseRepository.InsertAsync(course);
			await _courseRepository.SaveAsync();
			return CreateCourseResult.Success;
		}

		public async Task<DeleteCourseResult> DeleteAsync(int id)
		{
			var course = await _courseRepository.GetByIdAsync(id);
			if (course == null)
			{
				return DeleteCourseResult.CourseNotFound;
			}

			course.IsDeleted = true;

			_courseRepository.Update(course);
			await _courseRepository.SaveAsync();

			return DeleteCourseResult.Success;
		}

		public async Task<UpdateCourseViewModel?> GetForEditAsync(int id)
		{
			var course = await _courseRepository.GetByIdAsync(id);
			if (course == null) return null;

			return new UpdateCourseViewModel
			{
				Id = course.Id,
				TeacherId = course.TeacherId,
				CategoryId = course.CategoryId,
				StatusId = course.StatusId,
				CourseTitle = course.CourseTitle,
				CourseSubTitle = course.CourseSubTitle,
				CourseDescription = course.CourseDescription,
				CoursePrice = course.CoursePrice,
				ImageName = course.CourseImageName,
				Slug = course.Slug,
				CreateDate = course.CreateDate
			};
		}

		public async Task<UpdateCourseResult> UpdateAsync(UpdateCourseViewModel model)
		{
			Course course = new Course()
			{
				CoursePrice = model.CoursePrice,
				CourseTitle = model.CourseTitle,
				CourseDescription = model.CourseDescription,
				StatusId = model.StatusId,
				CreateDate = model.CreateDate,
				IsDeleted = false,
				CategoryId = model.CategoryId,
				TeacherId = model.TeacherId,
				CourseSubTitle = model.CourseSubTitle,
				Id = model.Id,
				CourseImageName = model.ImageName,
				Slug = model.Slug,
				
			};

			#region Add Image

			if (model.NewImageFile != null)
			{
				string imageName = Guid.NewGuid() + Path.GetExtension(model.NewImageFile.FileName);
				string path = PathTools.CourseImagePath;
				model.NewImageFile.AddImageToServer(imageName, path, null, null, null, model.ImageName);
				course.CourseImageName = imageName;
			}

			#endregion

			_courseRepository.Update(course);
			await _courseRepository.SaveAsync();
			return UpdateCourseResult.Success;
		}

		public async Task<bool> IsCourseExist(int id)
		{
			return await _courseRepository.IsCourseExist(id);
		}

		public async Task<ClientSideFilterCourseViewModel> FilterCourseAsync(ClientSideFilterCourseViewModel model)
		{
			return await _courseRepository.FilterCourseAsync(model);
		}

		public async Task<CourseDetailViewModel?> GetCourseForDetailAsync(string slug)
		{
			var course = await _courseRepository.GetBySlugWithIncludesAsync(slug);
			if (course == null)
				return null;

			return new CourseDetailViewModel
			{
				Id = course.Id,
				CourseTitle = course.CourseTitle,
				TeacherId = course.TeacherId,
				CategoryId = course.CategoryId,
				StatusId = course.StatusId,
				CoursePrice = course.CoursePrice,
				IsDeleted = course.IsDeleted,
				CreateDate = course.CreateDate,
				CourseStatus = course.CourseStatus,
				CourseEpisodes = course.CourseEpisodes,
				CourseCategory = course.CourseCategory,
				User = course.User,
				CourseSubTitle = course.CourseSubTitle,
				CourseDescription = course.CourseDescription,
				ImageName = course.CourseImageName,
				Slug = course.Slug,
				CourseComments = course.CourseComments,
				CourseLikes = course.CourseLikes,
                Participants = course.OrderDetails?.Count(od => od.Order != null && od.Order.IsFinally) ?? 0,
                CourseDiscount = course.CourseDiscount,
				totalCourseTime = course.CourseEpisodes
					.Where(e => !e.IsDeleted)
					.Aggregate(TimeSpan.Zero, (sum, episode) => sum.Add(episode.EpisodeTime))
			};

		}

		public string GetCourseSlugByIdAsync(int id)
		{
			return _courseRepository.GetCourseSlugByIdAsync(id);
		}

		public bool IsUserLikedCourse(int userId, int courseId)
		{
			return _courseRepository.IsUserLikedCourse(userId, courseId);
		}

		public async Task CreateCourseLikeAsync(int userId, int courseId)
		{
			await _courseRepository.CreateCourseLikeAsync(userId, courseId);
			await _courseRepository.SaveAsync();
		}

		public async Task DeleteCourseLikeAsync(int userId, int courseId)
		{
			await _courseRepository.DeleteCourseLikeAsync(userId, courseId);

		}

		public bool IsCourseHasDiscount(CourseDiscount? courseDiscount)
		{
			if (courseDiscount != null && courseDiscount.StartDate <= DateTime.Now &&
			    courseDiscount.EndDate >= DateTime.Now && !courseDiscount.IsDeleted)
			{
				return true;
			}

			return false;
		}

		public async Task<Course?> GetCourseByIdAsync(int courseId)
		{
			return await _courseRepository.GetByIdAsync(courseId);
		}
		public Task<List<Course>> GetLatestCoursesAsync(int count)
		{
			return _courseRepository.GetLatestCoursesAsync(count);
		}
	}
}

