using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nabeghe.Domain.Enums.Blog;
using Nabeghe.Domain.Models.Common;

namespace Nabeghe.Domain.Models.Blog;

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