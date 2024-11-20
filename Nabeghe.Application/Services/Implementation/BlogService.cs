using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Application.Services.Interfaces;
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

        public async Task<Blog> GetBlogDetailsAsync(int blogId)
        {
            return await _blogRepository.GetBlogByIdAsync(blogId);
        }

        public async Task AddNewBlogAsync(Blog blog)
        {
            await _blogRepository.AddBlogAsync(blog);
        }

        public async Task EditBlogAsync(Blog blog)
        {
            await _blogRepository.UpdateBlogAsync(blog);
        }

        public async Task DeleteBlogAsync(int blogId)
        {
            await _blogRepository.DeleteBlogAsync(blogId);
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
    }
}
