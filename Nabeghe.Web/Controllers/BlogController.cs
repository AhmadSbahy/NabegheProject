using Microsoft.AspNetCore.Mvc;
using Nabeghe.Application.Extensions;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Enums.Blog;
using Nabeghe.Domain.Models.Blog;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.ViewModels.Blog;
using Nabeghe.Domain.ViewModels.CourseComment;

namespace Nabeghe.Web.Controllers
{
    public class BlogController(IBlogService blogService,IBlogCommentService blogCommentService,IUserService userService) : Controller
    {

        #region List

        [Route("/blogs")]
        public async Task<IActionResult> List(ClientSideFilterBlogViewModel filter)
        {
            var model = await blogService.FilterBlogAsync(filter);
            return View(model);
        }

        #endregion

        #region Details

        [HttpGet("/blog-details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var blog = await blogService.GetBlogDetailsAsync(id);

            if (blog == null)
                return NotFound();

            if (User.Identity.IsAuthenticated)
            {
	            var user = await userService.GetByIdAsync(User.GetUserId());
	            ViewData["currentUserAvatar"] = user?.Avatar;
	            ViewData["currentUserFullName"] = user.GetUserFullName();
            }

			return View(blog);
        }

        #endregion

        //#region AddCourseLike

        //[HttpPost]
        //public async Task<IActionResult> AddCourseLike([FromBody] CreateCourseLikeViewModel model)
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return Json(new
        //        {
        //            success = false,
        //            status = 400
        //        });
        //    }
        //    if (_courseService.IsUserLikedCourse(User.GetUserId(), model.CourseId))
        //    {
        //        await _courseService.DeleteCourseLikeAsync(User.GetUserId(), model.CourseId);
        //    }
        //    else
        //    {
        //        await _courseService.CreateCourseLikeAsync(User.GetUserId(), model.CourseId);
        //    }

        //    return Json(new { success = true });
        //}

        //#endregion

        #region Comments

        #region Create Comment

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] AddBlogCommentViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new
                {
                    success = false,
                    status = 400
                });
            }

            var comment = new BlogComment()
            {
                BlogId = model.BlogId,
                UserId = User.GetUserId(),
                CommentText = model.CommentText,
                CreateDate = DateTime.Now,
                Status = BlogCommentStatus.Pending
            };

            await blogCommentService.AddCommentAsync(comment);

            return Json(new { success = true });
        }

        #endregion

        #region Create Reply

        [HttpPost]
        public async Task<IActionResult> AddReply([FromBody] AddBlogCommentReplyViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new
                {
                    success = false,
                    status = 400
                });
            }

            var reply = new BlogCommentReply
            {
                CommentId = model.CommentId,
                UserId = User.GetUserId(),
                ReplyText = model.ReplyText,
                CreateDate = DateTime.Now
            };

            await blogCommentService.AddReplyToCommentAsync(reply);

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
            if (blogCommentService.IsUserLikedBlogComment(User.GetUserId(), model.commentId))
            {
                 blogCommentService.DeleteBlogCommentLike(User.GetUserId(), model.commentId);
            }
            else
            {
	            var blogComment = new BlogCommentLike()
	            {
		            UserId = User.GetUserId(),
		            CommentId = model.commentId
	            };
                await blogCommentService.LikeCommentAsync(blogComment);
            }

            return Json(new { success = true });
        }


        #endregion

        #endregion
    }
}
