using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Enums.Course;

namespace Nabeghe.Domain.ViewModels.User
{
	public class UserCourseCommentViewModel
	{
		public int CourseId { get; set; }
		public string CourseImage { get; set; }
		public string CourseTitle { get; set; }
		public string Slug { get; set; }
		public string CommentText { get; set; }
		public CourseCommentStatus Status { get; set; }
	}
}
