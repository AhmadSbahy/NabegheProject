using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.ViewModels.Course;
using Nabeghe.Domain.ViewModels.CourseDiscount;
using Nabeghe.Domain.ViewModels.User;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Infra.Data.Repositories
{
	public class CourseRepository(NabegheContext _context) : ICourseRepository
	{
		public async Task InsertAsync(Course course)
		=> await _context.AddAsync(course);

		public async Task SaveAsync()
			=> await _context.SaveChangesAsync();

		public void Update(Course course)
			=> _context.Update(course);

		public async Task<Course?> GetByIdAsync(int id)
			=> await _context.Courses.Include(c=>c.CourseDiscount).FirstOrDefaultAsync(c => c.Id == id);

		public async Task<AdminSideFilterCourseViewModel> FilterCourseAsync(AdminSideFilterCourseViewModel model)
		{
			var query = _context.Courses.Include(c => c.CourseDiscount).AsQueryable();

			#region Filter

			if (!string.IsNullOrWhiteSpace(model.Param))
			{
				query = query.Where(c => c.CourseTitle.Contains(model.Param) || c.CourseSubTitle.Contains(model.Param)
				|| c.CourseDescription.Contains(model.Param));
			}

			if (model.Price > 0)
			{
				query = query.Where(c => c.CoursePrice == model.Price);
			}
			switch (model.Status)
			{
				case FilterCourseStatus.All:
					query = query;
					break;

				case FilterCourseStatus.NotDeleted:
					query = query.Where(u => !u.IsDeleted);
					break;

				case FilterCourseStatus.Deleted:
					query = query.Where(u => u.IsDeleted);
					break;
			}
			#endregion

			query = query.OrderByDescending(u => u.CreateDate);

			await model.Paging(query.Select(c => new CourseViewModel()
			{
				CoursePrice = c.CoursePrice,
				IsDeleted = c.IsDeleted,
				CourseTitle = c.CourseTitle,
				StatusId = c.StatusId,
				CreateDate = c.CreateDate,
				CategoryId = c.CategoryId,
				Id = c.Id,
				TeacherId = c.TeacherId,
				CourseDiscount = c.CourseDiscount,
				CourseImage = c.CourseImageName
			}));

			return model;
		}

		public async Task<bool> IsCourseExist(int id)
			=> await _context.Courses.AnyAsync(c => c.Id == id && !c.IsDeleted);

		public async Task<ClientSideFilterCourseViewModel> FilterCourseAsync(ClientSideFilterCourseViewModel model)
		{
			var query = _context.Courses
				.Include(c => c.User)
				.Include(c => c.CourseLikes)
				.Include(c => c.CourseDiscount)
				.Include(c => c.CourseStatus)
				.Include(c => c.CourseCategory)
				.Include(c => c.CourseEpisodes)
				.Where(c => !c.IsDeleted).AsQueryable();

			// فیلتر کردن و مرتب‌سازی
			#region Filter

			if (!string.IsNullOrWhiteSpace(model.Param) || !string.IsNullOrWhiteSpace(model.ParamRes))
			{
				query = query.Where(c => c.CourseTitle.Contains(model.Param ?? model.ParamRes) || c.CourseSubTitle.Contains(model.Param ?? model.ParamRes)
																			 || c.CourseDescription.Contains(model.Param ?? model.ParamRes));
			}

			if (model.IsFree || model.IsFreeRes)
			{
				query = query.Where(c => c.CoursePrice == 0);
			}

			if (model.IsNotFree || model.IsNotFreeRes)
			{
				query = query.Where(c => c.CoursePrice > 0);
			}

			if (model.CategoryId.HasValue)
			{
				query = query.Where(c => c.CategoryId == model.CategoryId.Value);
			}

			switch (model.Status)
			{
				case ClientSideFilterCourseStatus.All:
					query = query;
					break;
				case ClientSideFilterCourseStatus.Newest:
					query = query.OrderByDescending(u => u.CreateDate);
					break;
				case ClientSideFilterCourseStatus.Oldest:
					query = query.OrderBy(u => u.CreateDate);
					break;
				case ClientSideFilterCourseStatus.Recording:
					query = query.Where(c => c.StatusId == 1);
					break;
				case ClientSideFilterCourseStatus.Finished:
					query = query.Where(c => c.StatusId == 2);
					break;
				case ClientSideFilterCourseStatus.Free:
					query = query.Where(c => c.CoursePrice == 0);
					break;
				case ClientSideFilterCourseStatus.NotFree:
					query = query.Where(c => c.CoursePrice > 0);
					break;
			}
			#endregion

			// دریافت داده‌ها از پایگاه‌داده و تبدیل به لیست
			var courseList = await query.ToListAsync();

			// محاسبات زمان کل دوره‌ها را در سمت کلاینت انجام می‌دهیم
			var courseViewModelList = courseList.Select(c => new ClientSideCourseViewModel()
			{
				CoursePrice = c.CoursePrice,
				IsDeleted = c.IsDeleted,
				CourseTitle = c.CourseTitle,
				StatusId = c.StatusId,
				CreateDate = c.CreateDate,
				ImageName = c.CourseImageName,
				CategoryName = c.CourseCategory.Title,
				Id = c.Id,
				TeacherAvatar = c.User.Avatar,
				TeacherName = c.User.FirstName + " " + c.User.LastName,
				CourseLikes = c.CourseLikes,
				Slug = c.Slug,
				CategoryId = c.CategoryId,
				CourseDiscount = c.CourseDiscount,
				CourseStatus = c.CourseStatus.StatusTitle// محاسبه زمان کل دوره در کلاینت
			}).ToList();

			// تغییر به IQueryable
			await model.Paging(courseViewModelList.AsQueryable()); // تبدیل لیست به IQueryable برای ارسال به متد Paging

			return model;

		}

		public async Task<Course?> GetByIdWithIncludesAsync(int id)
			=> await _context.Courses.Include(c => c.User)
				.Include(c => c.CourseStatus)
				.Include(c => c.CourseCategory)
				.Include(c => c.CourseEpisodes).FirstOrDefaultAsync(c => c.Id == id);

		public async Task<Course?> GetBySlugWithIncludesAsync(string slug)
			=> await _context.Courses.Include(c => c.User)
				.Include(c => c.CourseStatus)
				.Include(c => c.CourseDiscount)
				.Include(c => c.CourseLikes)
				.Include(c => c.CourseCategory)
				.Include(c => c.CourseComments)
				.ThenInclude(c => c.User)
				.Include(c => c.CourseComments)
				.ThenInclude(c => c.Replies)
				.ThenInclude(cr => cr.User)
				.Include(c => c.CourseEpisodes)
				.Include(c => c.CourseComments)
				.ThenInclude(c => c.CommentLikes)
                .Include(c=> c.OrderDetails)
                .ThenInclude(c=>c.Order)
				.FirstOrDefaultAsync(c => c.Slug == slug);

		public string GetCourseSlugByIdAsync(int id)
		{
			return _context.Courses.SingleOrDefault(c => c.Id == id).Slug;
		}

		public bool IsUserLikedCourse(int userId, int courseId)
		{
			return _context.CourseLikes.Any(cl => cl.CourseId == courseId && cl.UserId == userId);
		}

		public async Task CreateCourseLikeAsync(int userId, int courseId)
		{
			var courseLike = new CourseLike()
			{
				UserId = userId,
				CourseId = courseId
			};
			await _context.AddAsync(courseLike);
		}

		public async Task DeleteCourseLikeAsync(int userId, int courseId)
		{
			var courseLike =
				await _context.CourseLikes.FirstOrDefaultAsync(cl => cl.UserId == userId && cl.CourseId == courseId);
			if (courseLike != null)
			{
				_context.CourseLikes.Remove(courseLike);
				await SaveAsync();
			}
		}

		
	}
}
