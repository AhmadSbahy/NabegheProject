using Nabeghe.Domain.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;
using Nabeghe.Domain.Enums.Course;

namespace Nabeghe.Domain.Models.Course
{
	//public class CourseComment : BaseEntity<int>
	//{
	//    #region Properties

	//    public int CourseId { get; set; }
	//    public int UserId { get; set; }
	//    public string CommentText { get; set; }
	//    public CourseCommentStatus Status { get; set; }
	//    #endregion

	//    #region Relations

	//    [ForeignKey(nameof(CourseId))]
	//    public Course? Course { get; set; }
	//    [ForeignKey(nameof(UserId))]
	//    public User.User? User { get; set; }

	//    public List<CommentReply>? Replies { get; set; }
	//    public List<CommentLike>? CommentLikes { get; set; }

	//    #endregion
	//}
	public class CourseComment : BaseEntity<int>
	{
		public int CourseId { get; set; }
		public int UserId { get; set; }
		public string CommentText { get; set; }
		public CourseCommentStatus Status { get; set; }

		[ForeignKey(nameof(CourseId))]
		public Course? Course { get; set; }

		[ForeignKey(nameof(UserId))]
		public User.User? User { get; set; }

		public List<CommentReply>? Replies { get; set; }
		public List<CommentLike>? CommentLikes { get; set; }
	}
}
