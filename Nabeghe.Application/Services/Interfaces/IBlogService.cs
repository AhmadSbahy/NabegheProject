using Nabeghe.Domain.Models.Blog;
using Nabeghe.Domain.ViewModels.Blog;

namespace Nabeghe.Application.Services.Interfaces
{
    public interface IBlogService
    {
        Task<Blog> GetBlogDetailsAsync(int blogId);
        Task AddNewBlogAsync(Blog blog);
        Task EditBlogAsync(Blog blog);
        Task DeleteBlogAsync(int blogId);
        Task<ClientSideFilterBlogViewModel> FilterBlogAsync(ClientSideFilterBlogViewModel model);

    }
}
