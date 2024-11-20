using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabeghe.Domain.Models.Blog;

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