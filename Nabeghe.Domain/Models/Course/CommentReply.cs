using Nabeghe.Domain.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;
using Nabeghe.Domain.Enums.Course;

namespace Nabeghe.Domain.Models.Course;

//public class CommentReply : BaseEntity<int>
//{
//    #region Properties

//    public int CommentId { get; set; }
//    public int UserId { get; set; }
//    public string ReplyText { get; set; }
//    public CourseCommentStatus Status { get; set; }

//	#endregion

//	#region Relations

//	[ForeignKey(nameof(UserId))]
//    public User.User? User { get; set; }

//    [ForeignKey(nameof(CommentId))]
//    public CourseComment? CourseComment { get; set; }

//    #endregion
//}
public class CommentReply : BaseEntity<int>
{
	public int CommentId { get; set; }
	public int UserId { get; set; }
	public string ReplyText { get; set; }
	public CourseCommentStatus Status { get; set; } // همین فیلد را دارید

	[ForeignKey(nameof(UserId))]
	public User.User? User { get; set; }

	[ForeignKey(nameof(CommentId))]
	public CourseComment? CourseComment { get; set; }
}