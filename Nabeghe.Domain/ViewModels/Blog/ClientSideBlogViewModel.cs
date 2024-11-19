using Nabeghe.Domain.Enums.Blog;
using Nabeghe.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.Blog
{
    public class ClientSideBlogViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان مقاله")]
        public string BlogTitle { get; set; }

        [Display(Name = "نام نویسنده")]
        public string AuthorName { get; set; }

        [Display(Name = "تصویر نویسنده")]
        public string AuthorAvatar { get; set; }

        [Display(Name = "عکس مقاله")]
        public string BlogImage { get; set; }

        [Display(Name = "توضیحات مقاله")]
        public string BlogDescription { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "وضعیت انتشار")]
        public string Status { get; set; }

        [Display(Name = "تعداد نظرات")]
        public int CommentCount { get; set; }

        [Display(Name = "تعداد لایک‌ها")]
        public int LikeCount { get; set; }
    }
    public class ClientSideFilterBlogViewModel : BasePaging<ClientSideBlogViewModel>
    {
        public ClientSideFilterBlogViewModel() : base(9) // تعداد آیتم‌ها در هر صفحه
        {
        }

        [Display(Name = "جستجو")]
        public string SearchParam { get; set; }

        [Display(Name = "نویسنده")]
        public int? AuthorId { get; set; }

        [Display(Name = "تاریخ ایجاد از")]
        public DateTime? FromDate { get; set; }

        [Display(Name = "تاریخ ایجاد تا")]
        public DateTime? ToDate { get; set; }

        [Display(Name = "وضعیت")]
        public BlogCommentStatus? Status { get; set; }
    }

}
