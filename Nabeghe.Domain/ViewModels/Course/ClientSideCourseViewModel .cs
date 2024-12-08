using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Models.Course;

namespace Nabeghe.Domain.ViewModels.Course
{
	public class ClientSideCourseViewModel
	{
		public int Id { get; set; }

		public string CourseTitle { get; set; }

		public string TeacherName { get; set; }

        public string TeacherAvatar { get; set; }

		public string CategoryName { get; set; }
		
		public int StatusId { get; set; }
		
		public int CoursePrice { get; set; }

		public bool IsDeleted { get; set; }

		public int CategoryId { get; set; }

		public DateTime CreateDate { get; set; }

        public string ImageName { get; set; }

        public int TotalCourseTime { get; set; }

        public string CourseStatus { get; set; }

        public string Slug { get; set; }

        public ICollection<CourseLike> CourseLikes { get; set; }

        public Models.Course.CourseDiscount? CourseDiscount { get; set; }
	}
}
