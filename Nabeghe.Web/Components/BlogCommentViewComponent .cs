using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Enums.Course;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Web.Components
{
    public class BlogCommentViewComponent(IBlogCommentService blogCommentService) : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(int blogId)
		{
			var blogComments = await blogCommentService.GetBlogCommentsAsync(blogId);

			return View("/Views/Shared/Components/BlogComment.cshtml", blogComments);
		}
	}
}
