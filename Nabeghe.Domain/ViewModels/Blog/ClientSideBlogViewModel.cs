using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Models.Blog;

namespace Nabeghe.Domain.ViewModels.Blog
{
    public class ClientSideBlogViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان مقاله")]
        public string BlogTitle { get; set; }

        public string Slug { get; set; }

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

        public ICollection<BlogLike>? BlogLikes { get; set; }
    }
}
