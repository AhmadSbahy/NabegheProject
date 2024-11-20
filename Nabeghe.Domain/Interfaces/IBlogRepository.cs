using Nabeghe.Domain.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.ViewModels.Blog;

namespace Nabeghe.Domain.Interfaces
{
    public interface IBlogRepository
    {
        Task<Blog> GetBlogByIdAsync(int id);
        Task<IEnumerable<Blog>> GetAllBlogsAsync();
        Task AddBlogAsync(Blog blog);
        Task UpdateBlogAsync(Blog blog);
        Task DeleteBlogAsync(int id);
        Task<ClientSideFilterBlogViewModel> FilterBlogAsync(ClientSideFilterBlogViewModel model);
        Task AddBlogLikeAsync(CreateBlogLikeViewModel model);
        Task RemoveBlogLike(int userId, int blogId);
        Task<bool> IsUserLikedBlogAsync(int userId, int blogId);
	}
}
