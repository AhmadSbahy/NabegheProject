using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Blog;
using Nabeghe.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Enums.Blog;
using Nabeghe.Domain.ViewModels.Blog;

namespace Nabeghe.Infra.Data.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly NabegheContext _context;

        public BlogRepository(NabegheContext context)
        {
            _context = context;
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            return await _context.Blogs.Include(b => b.User).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Blog>> GetAllBlogsAsync()
        {
            return await _context.Blogs.Include(b => b.User).ToListAsync();
        }

        public async Task AddBlogAsync(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBlogAsync(Blog blog)
        {
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBlogAsync(int id)
        {
            var blog = await GetBlogByIdAsync(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ClientSideFilterBlogViewModel> FilterBlogAsync(ClientSideFilterBlogViewModel model)
        {
            var query = _context.Blogs
                .Include(b => b.User)
                .Include(b => b.BlogComments)
                .ThenInclude(c => c.CommentLikes)
                .AsQueryable();

            #region Filters
            if (!string.IsNullOrWhiteSpace(model.SearchParam))
            {
                query = query.Where(b => b.BlogTitle.Contains(model.SearchParam)
                                      || b.BlogDescription.Contains(model.SearchParam));
            }

            if (model.AuthorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == model.AuthorId);
            }

            if (model.FromDate.HasValue)
            {
                query = query.Where(b => b.CreateDate >= model.FromDate.Value);
            }

            if (model.ToDate.HasValue)
            {
                query = query.Where(b => b.CreateDate <= model.ToDate.Value);
            }

            if (model.Status.HasValue)
            {
                query = query.Where(b => b.BlogComments.Any(c => c.Status == model.Status.Value));
            }
            #endregion

            query = query.OrderByDescending(b => b.CreateDate);

            // دریافت داده‌ها از پایگاه‌داده و تبدیل به لیست
            var blogList = await query.ToListAsync();

            // تبدیل Blog به ClientSideBlogViewModel
            var blogViewModelList = blogList.Select(b => new ClientSideBlogViewModel
            {
                Id = b.Id,
                BlogTitle = b.BlogTitle,
                BlogImage = b.BlogImage,
                BlogDescription = b.BlogDescription,
                AuthorName = b.User != null ? $"{b.User.FirstName} {b.User.LastName}" : "نامشخص",
                AuthorAvatar = b.User?.Avatar,
                CreateDate = b.CreateDate,
                Status = b.BlogComments.Any(c => c.Status == BlogCommentStatus.Confirmed) ? "منتشر شده" : "در انتظار",
                CommentCount = b.BlogComments.Count,
                LikeCount = b.BlogComments.SelectMany(c => c.CommentLikes).Count()
            }).ToList();

            // صفحه‌بندی
            await model.Paging(blogViewModelList.AsQueryable());

            return model;
        }


    }
}
