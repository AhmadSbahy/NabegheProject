using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Enums.Blog;
using Nabeghe.Domain.Enums.Course;
using Nabeghe.Domain.Models.Common;
using Nabeghe.Domain.Models.Course;

namespace Nabeghe.Domain.Models.Blog
{
    public class Blog : BaseEntity<int>
    {
        #region Properties

        public int AuthorId { get; set; }

        [Display(Name = "عکس مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
        public string BlogImage { get; set; }

        [Display(Name = "عنوان مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(600, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
        public string BlogTitle { get; set; }

        [Display(Name = "عنوان مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string BlogDescription { get; set; }

        #endregion

        #region Relations

        [ForeignKey(nameof(AuthorId))]
        public User.User? User { get; set; }

        public ICollection<BlogComment> BlogComments { get; set; }

        #endregion
    }
    public class BlogComment : BaseEntity<int>
    {
        #region Properties

        public int BlogId { get; set; }
        public int UserId { get; set; }
        [Display(Name = "کامنت مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(800, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
        public string CommentText { get; set; }
        public BlogCommentStatus Status { get; set; }

        #endregion

        #region Relations

        [ForeignKey(nameof(BlogId))]
        public Blog? Blog { get; set; }

        [ForeignKey(nameof(UserId))]
        public User.User? User { get; set; }

        public List<BlogCommentReply>? Replies { get; set; }
        public List<BlogCommentLike>? CommentLikes { get; set; }

        #endregion
    }
    public class BlogCommentReply : BaseEntity<int>
    {
        #region Properties

        public int CommentId { get; set; }
        public int UserId { get; set; }
        [Display(Name = "پاسخ کامنت مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(800, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
        public string ReplyText { get; set; }

        #endregion

        #region Relations

        [ForeignKey(nameof(UserId))]
        public User.User? User { get; set; }

        [ForeignKey(nameof(CommentId))]
        public BlogComment? BlogComment { get; set; }

        #endregion
    }
    public class BlogCommentLike
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CommentId { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(UserId))]
        public User.User User { get; set; }

        [ForeignKey(nameof(CommentId))]
        public BlogComment? BlogComment { get; set; }
        #endregion

    }
}
