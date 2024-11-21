using Nabeghe.Domain.Models.Blog;
using Nabeghe.Domain.ViewModels.Blog;

namespace Nabeghe.Application.Services.Interfaces
{
    public interface IBlogService
    {
        Task<AdminEditBlogViewModel?> GetBlogDetailsAsync(int blogId);
        Task<AdminCreateBlogStatus> AddNewBlogAsync(AdminCreateBlogViewModel model);
        Task<AdminEditBlogStatus> EditBlogAsync(AdminEditBlogViewModel model);
        Task<AdminDeleteBlogStatus> DeleteBlogAsync(int blogId);
        Task<ClientSideFilterBlogViewModel> FilterBlogAsync(ClientSideFilterBlogViewModel model);
        Task AddBlogLikeAsync(CreateBlogLikeViewModel model);
        Task RemoveBlogLike(int userId,int blogId);
        Task<bool> IsUserLikedBlogAsync(int userId,int blogId);
        Task<AdminFilterBlogViewModel> FilterBlogAsync(AdminFilterBlogViewModel model);
        Task<Blog> GetBlogDetailsByIdAsync(int blogId);

    }
}
