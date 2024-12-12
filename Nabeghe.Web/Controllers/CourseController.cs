using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Application.Extensions;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Enums.Course;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.ViewModels.Course;
using Nabeghe.Domain.ViewModels.CourseComment;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Web.Controllers
{
	public class CourseController(ICourseCommentRepository commentRepository, ICourseService _courseService
		, ICourseEpisodeService _courseEpisodeService,IUserService userService) : SiteBaseController
	{
		#region List

		[Route("/courses")]
		public async Task<IActionResult> List(ClientSideFilterCourseViewModel filter)
		{
			var model = await _courseService.FilterCourseAsync(filter);
			return View(model);
		}

		#endregion

		#region Details

		[HttpGet("/course-details/{slug}/{episodeId?}")]
		public async Task<IActionResult> Details(string slug, int? episodeId)
		{
			var course = await _courseService.GetCourseForDetailAsync(slug);

			if (course == null)
				return NotFound();

			if (slug != null && episodeId != null)
			{
				var episodeFileName = await _courseEpisodeService.GetEpisodeFileNameAsync(course.Id, (int)episodeId);

				if (string.IsNullOrWhiteSpace(episodeFileName))
					return NotFound();

				ViewData["episodeFileName"] = episodeFileName;
			}

			if (User.Identity.IsAuthenticated)
			{
				var user = await userService.GetByIdAsync(User.GetUserId());
				ViewData["currentUserAvatar"] = user?.Avatar;
				ViewData["currentUserFullName"] = user.GetUserFullName();
			}
			return View(course);
		}

        #endregion

        #region AddCourseLike

        [HttpPost]
        public async Task<IActionResult> AddCourseLike([FromBody] CreateCourseLikeViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new
                {
                    success = false,
                    status = 400
                });
            }
            if (_courseService.IsUserLikedCourse(User.GetUserId(), model.CourseId))
            {
                await _courseService.DeleteCourseLikeAsync(User.GetUserId(), model.CourseId);
            }
            else
            {
                await _courseService.CreateCourseLikeAsync(User.GetUserId(), model.CourseId);
            }

            return Json(new { success = true });
        }

        #endregion

        #region Comments

        #region Create Comment

        [HttpPost]
		public async Task<IActionResult> AddComment([FromBody] AddCommentViewModel model)
		{
			if (!User.Identity.IsAuthenticated)
			{
				return Json(new
				{
					success = false,
					status = 400
				});
			}

			var comment = new CourseComment
			{
				CourseId = model.courseId,
				UserId = User.GetUserId(),
				CommentText = model.commentText,
				CreateDate = DateTime.Now,
				Status = CourseCommentStatus.Pending
			};

			await commentRepository.AddCommentAsync(comment);

			return Json(new { success = true });
		}

		#endregion

		#region Create Reply

		[HttpPost]
		public async Task<IActionResult> AddReply([FromBody] AddReplyViewModel model)
		{
			if (!User.Identity.IsAuthenticated)
			{
				return Json(new
				{
					success = false,
					status = 400
				});
			}

			var reply = new CommentReply
			{
				CommentId = model.commentId,
				UserId = User.GetUserId(),
				ReplyText = model.replyText,
				CreateDate = DateTime.Now
			};

			await commentRepository.AddReplyAsync(reply);

			return Json(new { success = true });
		}

		#endregion

		#region AddCommentLike

		[HttpPost]
		public async Task<IActionResult> AddLike([FromBody] AddCommentLikeViewModel model)
		{
			if (!User.Identity.IsAuthenticated)
			{
				return Json(new
				{
					success = false,
					status = 400
				});
			}
			if (commentRepository.IsUserLikedComment(User.GetUserId(), model.CommentId))
			{
				await commentRepository.DeleteCommentLikeAsync(User.GetUserId(), model.CommentId);
			}
			else
			{
				await commentRepository.AddCommentLikeAsync(User.GetUserId(), model.CommentId);
			}

			return Json(new { success = true });
		}


		#endregion

		#endregion
	}

}
