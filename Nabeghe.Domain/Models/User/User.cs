using Nabeghe.Domain.Enums.User;
using Nabeghe.Domain.Models.Common;
using Nabeghe.Domain.Models.Course;
using System.ComponentModel.DataAnnotations;
using Nabeghe.Domain.Models.Blog;

namespace Nabeghe.Domain.Models.User
{
    public class User : BaseEntity<int>
	{
		#region Properties

		[MaxLength(150,ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string? FirstName { get; set; }

		[MaxLength(150, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string? LastName { get; set; }
		
		[Display(Name = "موبایل")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(150, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string Mobile { get; set; }

		[Display(Name = "کد تایید")]
		[MaxLength(12, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string? ConfirmCode { get; set; }
		
		[Display(Name = "کلمه عبور")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(400, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string Password { get; set; }

		[MaxLength(350, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string? Email { get; set; }

		public string? Avatar { get; set; }

		public bool IsDeleted { get; set; } = false;

		public UserStatus Status { get; set; }

		#endregion

		#region Relations

		public ICollection<UserRole>? UserRoles { get; set; }
		public ICollection<Course.Course>? Courses { get; set; }
		public ICollection<Order.Order>? Orders { get; set; }
		public ICollection<CourseComment>? CourseComments { get; set; }
		public ICollection<CommentReply>? CommentReplies { get; set; }
		public ICollection<CommentLike>? CommentLikes { get; set; }
        public ICollection<CourseLike>? CourseLikes { get; set; }
        public ICollection<ContactUs.ContactUs>? ContactUs { get; set; }
        public ICollection<Blog.Blog>? Blogs { get; set; }
        public ICollection<Blog.BlogComment>? BlogComments { get; set; }
        public ICollection<Blog.BlogCommentReply>? BlogCommentReplies { get; set; }
        public ICollection<BlogCommentLike>? BlogCommentLikes { get; set; }
        public ICollection<BlogLike>? BlogLikes { get; set; }

        #endregion
    }
}
