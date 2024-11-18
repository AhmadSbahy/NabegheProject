using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.Course
{
	public class CourseViewModel
	{
		public int Id { get; set; }

		public string CourseTitle { get; set; }

		public int TeacherId { get; set; }

		public int CategoryId { get; set; }
		
		public int StatusId { get; set; }
		
		public int CoursePrice { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime CreateDate { get; set; }

        public string? CourseImage { get; set; }

        public Models.Course.CourseDiscount? CourseDiscount { get; set; }	
	}
}
