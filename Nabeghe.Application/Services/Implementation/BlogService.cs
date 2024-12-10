using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Application.Extensions;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Application.Statics;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Blog;
using Nabeghe.Domain.ViewModels.Blog;

namespace Nabeghe.Application.Services.Implementation
{
	public class BlogService : IBlogService
	{
		private readonly IBlogRepository _blogRepository;

		public BlogService(IBlogRepository blogRepository)
		{
			_blogRepository = blogRepository;
		}

		public async Task<Blog> GetBlogDetailsByIdAsync(int blogId)
		{
			return await _blogRepository.GetBlogByIdAsync(blogId);
		}

		public async Task<AdminEditBlogViewModel?> GetBlogDetailsAsync(int blogId)
		{
			var blog = await _blogRepository.GetBlogByIdAsync(blogId);
			if (blog == null)
			{
				return null;
			}
			return new AdminEditBlogViewModel()
			{
				AuthorId = blog.AuthorId,
				BlogDescription = blog.BlogDescription,
				BlogImageName = blog.BlogImage,
				BlogTitle = blog.BlogTitle,
				Id = blog.Id
			};
		}

		public async Task<AdminCreateBlogStatus> AddNewBlogAsync(AdminCreateBlogViewModel model)
		{
			var newBlog = new Blog
			{
				BlogTitle = model.BlogTitle,
				BlogDescription = model.BlogDescription,
				AuthorId = model.AuthorId,
				CreateDate = DateTime.Now
			};

			#region Add Image

			if (model.BlogImage != null)
			{
				string imageName = Guid.NewGuid() + Path.GetExtension(model.BlogImage.FileName);
				string path = PathTools.BlogImagePath;
				model.BlogImage.AddImageToServer(imageName, path);
				newBlog.BlogImage = imageName;
			}

			#endregion

			await _blogRepository.AddBlogAsync(newBlog);
			return AdminCreateBlogStatus.Success;
		}

		public async Task<AdminEditBlogStatus> EditBlogAsync(AdminEditBlogViewModel model)
		{
			var blog = await _blogRepository.GetBlogByIdAsync(model.Id);
			if (blog == null) return AdminEditBlogStatus.NotFound;

			blog.BlogTitle = model.BlogTitle;
			blog.BlogDescription = model.BlogDescription;

			#region Add Image

			if (model.BlogImage != null)
			{
				string imageName = Guid.NewGuid() + Path.GetExtension(model.BlogImage.FileName);
				string path = PathTools.BlogImagePath;
				model.BlogImage.AddImageToServer(imageName, path,null,null,null,blog.BlogImage);
				blog.BlogImage = imageName;
			}

			#endregion

			await _blogRepository.UpdateBlogAsync(blog);
			return AdminEditBlogStatus.Success;
		}

		public async Task<AdminDeleteBlogStatus> DeleteBlogAsync(int blogId)
		{
			var blog = await _blogRepository.GetBlogByIdAsync(blogId);
			if (blog == null)
			{
				return AdminDeleteBlogStatus.NotFound;
			}
			blog.IsDeleted = true;
			await _blogRepository.UpdateBlogAsync(blog);
			return AdminDeleteBlogStatus.Success;
		}

		public async Task<ClientSideFilterBlogViewModel> FilterBlogAsync(ClientSideFilterBlogViewModel model)
		{
			return await _blogRepository.FilterBlogAsync(model);
		}

		public async Task AddBlogLikeAsync(CreateBlogLikeViewModel model)
		{
			await _blogRepository.AddBlogLikeAsync(model);
		}

		public async Task RemoveBlogLike(int userId, int blogId)
		{
			await _blogRepository.RemoveBlogLike(userId, blogId);
		}

		public async Task<bool> IsUserLikedBlogAsync(int userId, int blogId)
		{
			return await _blogRepository.IsUserLikedBlogAsync(userId, blogId);
		}

		public async Task<AdminFilterBlogViewModel> FilterBlogAsync(AdminFilterBlogViewModel model)
		{
			return await _blogRepository.FilterBlogAsync(model);
		}
		public Task<List<Blog>> GetLatestBlogsAsync(int count)
		{
			return _blogRepository.GetLatestBlogsAsync(count);
		}
	}
}
