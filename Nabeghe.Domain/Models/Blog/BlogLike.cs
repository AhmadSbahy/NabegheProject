using System.ComponentModel.DataAnnotations.Schema;

namespace Nabeghe.Domain.Models.Blog;

public class BlogLike
{
	#region Properties

	public int Id { get; set; }
	public int UserId { get; set; }
	public int BlogId { get; set; }

	#endregion

	#region Relations

	[ForeignKey(nameof(UserId))]
	public User.User User { get; set; }

	[ForeignKey(nameof(BlogId))]
	public Blog Blog { get; set; }

	#endregion
}