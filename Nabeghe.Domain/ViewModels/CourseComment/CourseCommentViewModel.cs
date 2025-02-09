using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Enums.Course;
using Nabeghe.Domain.Models.Course;

namespace Nabeghe.Domain.ViewModels.CourseComment
{
	//public class CourseCommentViewModel
	//{
	//	public int Id { get; set; }
	//	public int CourseId { get; set; }
	//	public int UserId { get; set; }
	//	public string CommentText { get; set; }
	//	public CourseCommentStatus Status { get; set; }
	//	public DateTime CreateDate { get; set; }
	//}
	public class CourseCommentViewModel
	{
		public int Id { get; set; }
		public int CourseId { get; set; }
		public int UserId { get; set; }
		public string CommentText { get; set; }
		public CourseCommentStatus Status { get; set; }
		public DateTime CreateDate { get; set; }

		// افزودن این ویژگی‌ها برای نمایش پاسخ‌ها و لایک‌ها
		public List<CommentReply>? Replies { get; set; }
		public List<CommentLike>? CommentLikes { get; set; }
	}
}
