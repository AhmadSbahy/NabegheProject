using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.User
{
	public class UserPurchasedCourseViewModel
	{
		public int CourseId { get; set; }
		public string CourseSlug { get; set; }
		public string CourseTitle { get; set; }
		public string CourseImage { get; set; }
		public string CourseStatus { get; set; }
		public string CategoryName { get; set; }
		public string TeacherAvatar { get; set; }
		public string TeacherFullName { get; set; }
		public int Price { get; set; }
		public Models.Course.CourseDiscount CourseDiscount { get; set; }	
	}
}
