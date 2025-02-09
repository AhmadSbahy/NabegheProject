using Nabeghe.Domain.Enums.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.CourseComment
{
	public class CommentReplyViewModel
	{
		public int Id { get; set; }
		public int CommentId { get; set; }
		public int UserId { get; set; }
		public string ReplyText { get; set; }
		public CourseCommentStatus Status { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
