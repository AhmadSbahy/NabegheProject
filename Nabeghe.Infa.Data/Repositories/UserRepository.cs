using Nabeghe.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nabeghe.Domain.Enums.Course;
using Nabeghe.Domain.Enums.User;
using Nabeghe.Domain.Models.ContactUs;
using Nabeghe.Domain.Models.User;
using Nabeghe.Domain.ViewModels.Course;
using Nabeghe.Domain.ViewModels.User;
using Nabeghe.Infra.Data.Context;
using Nabeghe.Domain.ViewModels.CourseCategory;
using Nabeghe.Domain.ViewModels.ContactUs;

namespace Nabeghe.Infra.Data.Repositories
{
    public class UserRepository
        (NabegheContext _context)
        : IUserRepository
    {
        public async Task<bool> IsExitsMobileAsync(string mobile)
            => await _context.Users.AnyAsync(u => u.Mobile == mobile);

        public async Task InsertAsync(User user)
            => await _context.Users.AddAsync(user);

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();

        public async Task<User?> GetByMobileAndPasswordAsync(string mobile, string password)
            => await _context.Users.FirstOrDefaultAsync(u => u.Mobile == mobile && u.Password == password);

        public async Task<User?> GetByMobileAsync(string mobile)
            => await _context.Users.FirstOrDefaultAsync(u => u.Mobile == mobile);

        public void Update(User user)
            => _context.Users.Update(user);

        public async Task<User?> GetByMobileAndConfirmCodeAsync(string mobile, string confirmCode)
            => await _context.Users.FirstOrDefaultAsync(u => u.Mobile == mobile && u.ConfirmCode == confirmCode);

        public async Task<User?> GetByIdAsync(int userId)
            => await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        public async Task<bool> DuplicatedMobileAsync(string mobile, int userId)
            => await _context.Users.AnyAsync(u => u.Mobile == mobile && u.Id != userId);

        public async Task<FilterUserViewModel> FilterUserAsync(FilterUserViewModel model)
        {
            var query = _context.Users.AsQueryable();

            #region Filter

            if (!string.IsNullOrEmpty(model.Param))
            {
                query = query.Where(u => u.LastName.Contains(model.Param) || u.FirstName.Contains(model.Param)
                      || u.Mobile.Contains(model.Param) || u.Email.Contains(model.Param));
            }

            switch (model.Status)
            {
                case FilterUserStatus.All:
                    query = query;
                    break;

                case FilterUserStatus.NotDeleted:
                    query = query.Where(u => !u.IsDeleted);
                    break;

                case FilterUserStatus.Deleted:
                    query = query.Where(u => u.IsDeleted);
                    break;

                case FilterUserStatus.Active:
                    query = query.Where(u => u.Status == UserStatus.Active);
                    break;

                case FilterUserStatus.NotActive:
                    query = query.Where(u => u.Status == UserStatus.NotActive);
                    break;

                case FilterUserStatus.Ban:
                    query = query.Where(u => u.Status == UserStatus.Ban);
                    break;
            }
            #endregion

            query = query.OrderByDescending(u => u.CreateDate);

            await model.Paging(query.Select(u => new UserViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Mobile = u.Mobile,
                Email = u.Email,
                IsDelete = u.IsDeleted,
                Status = u.Status,
                CreateDate = u.CreateDate
            }));

            return model;
        }

        public async Task<List<UserFavoriteCourseViewModel>> GetCourseFavoriteListAsync(int userId)
        {
            var favoriteCourses = await _context.CourseLikes
                .Include(c => c.Course)
                .ThenInclude(c => c.CourseStatus)
                .Include(c => c.Course)
                .ThenInclude(c => c.User)
                .Where(cl => cl.UserId == userId)
                .Select(cl => new UserFavoriteCourseViewModel()
                {
                    Id = cl.Course.Id,
                    CourseTitle = cl.Course.CourseTitle,
                    Price = cl.Course.CoursePrice,
                    Status = cl.Course.CourseStatus.StatusTitle,
                    Slug = cl.Course.Slug,
                    TeacherFullName = cl.Course.User.FirstName + " " + cl.Course.User.LastName,
                    CourseImageName = cl.Course.CourseImageName,
                    TeacherImage = cl.Course.User.Avatar
                })
                .ToListAsync();

            return favoriteCourses;
        }

        public async Task<List<UserCourseCommentViewModel>> GetUserCourseCommentListAsync(int userId)
        {
			var courseComment = await _context.CourseComments
				.Where(c => c.UserId == userId)
				.OrderByDescending(c=>c.CreateDate)
				.Select(c => new UserCourseCommentViewModel
				{
					CourseId = c.Course.Id,
					CourseImage = c.Course.CourseImageName,
					CourseTitle = c.Course.CourseTitle,
					Slug = c.Course.Slug,
					CommentText = c.CommentText,
					Status = c.Status
				})
				.ToListAsync();

			return courseComment;
		}

        public string GetUserFullName(int userId)
        {
            return _context.Users.Where(u => u.Id == userId).Select(u => $"{u.FirstName} {u.LastName}").SingleOrDefault() ?? string.Empty;
        }

        public async Task<bool> IsUserInCourse(int userId, int courseId)
        {
	        return await _context.Orders
		        .Include(o=> o.OrderDetails)
		        .Where(order => order.UserId == userId && order.IsFinally) // سفارش‌های کاربر که پرداخت شده‌اند
		        .SelectMany(order => order.OrderDetails) // دریافت جزئیات سفارش
		        .AnyAsync(orderDetail => orderDetail.CourseId == courseId); // بررسی دوره
        }


		public async Task InsertContactUsAsync(ContactUs contactUs)
        {
            await _context.ContactUs.AddAsync(contactUs);
        }

        public async Task<FilterContactUsViewModel> GetContactUsListAsync(FilterContactUsViewModel model)
        {
            var query = _context.ContactUs.AsQueryable();

            #region Filter

            if (!string.IsNullOrEmpty(model.Param))
            {
                query = query.Where(c=>c.Message.Contains(model.Param) || c.Answer.Contains(model.Param) || c.FullName.Contains(model.Param) 
                                       || c.Mobile.Contains(model.Param)
                                       || c.Email.Contains(model.Param));
            }

            switch (model.FilterContactUsStatus)
            {
                case FilterContactUsStatus.All:
                    query = query;
                    break;

                case FilterContactUsStatus.Answered:
                    query = query.Where(c=> c.Answer != null);
                    break;

                case FilterContactUsStatus.NotAnswered:
                    query = query.Where(c => c.Answer == null);
                    break;
            }
            #endregion

            query = query.OrderByDescending(c => c.CreateDate);

            await model.Paging(query.Select(c => new ContactUsViewModel()
            {
                Id = c.Id,
                Mobile = c.Mobile,
                Email = c.Email,
                Answer = c.Answer,
                AnswerUser = c.AnswerUser,
                AnswerUserId = c.AnswerUserId,
                Ip = c.Ip,
                FullName = c.FullName,
                Message = c.Message,
                CreateDate = c.CreateDate
            }));

            return model;
        }

        public async Task<ContactUsViewModel?> GetContactUsByIdAsync(int id)
        {
            var contactUs = await _context.ContactUs
                .Include(c => c.AnswerUser)
                .FirstOrDefaultAsync(c => c.Id == id);
            return new ContactUsViewModel
            {
                FullName = contactUs.FullName,
                Email = contactUs.Email,
                Mobile = contactUs.Mobile,
                Message = contactUs.Message,
                Answer = contactUs.Answer,
                AnswerUserId = contactUs.AnswerUserId,
                Ip = contactUs.Ip,
                AnswerUser = contactUs.AnswerUser,
                CreateDate = contactUs.CreateDate,
                Id = contactUs.Id
            };
        }

        public async Task UpdateContactUs(int id,ContactUsViewModel model)
        {
            var contactUs = await _context.ContactUs
                .Include(c => c.AnswerUser)
                .FirstOrDefaultAsync(c => c.Id == id);
           contactUs.Answer = model.Answer;
           contactUs.AnswerUserId = model.AnswerUserId;
            if (contactUs != null) _context.ContactUs.Update(contactUs);
        }
    }
}
