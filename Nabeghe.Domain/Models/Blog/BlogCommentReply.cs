using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nabeghe.Domain.Models.Common;

namespace Nabeghe.Domain.Models.Blog;

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