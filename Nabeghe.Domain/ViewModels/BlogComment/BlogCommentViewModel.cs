using Nabeghe.Domain.Enums.Blog;

namespace Nabeghe.Domain.ViewModels.BlogComment;

public class BlogCommentViewModel
{
	public int Id { get; set; }
	public int BlogId { get; set; }
	public int UserId { get; set; }
	public string CommentText { get; set; }
	public BlogCommentStatus Status { get; set; }
	public DateTime CreateDate { get; set; }
}