using Nabeghe.Domain.Enums.Course;
using Nabeghe.Domain.ViewModels.CourseComment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.ViewModels.Common;

namespace Nabeghe.Domain.ViewModels.BlogComment
{
	public class FilterBlogCommentViewModel : BasePaging<BlogCommentViewModel>
	{
		public int BlogId { get; set; }
		[Display(Name = "جستجو")]
		public string Param { get; set; }
		[Display(Name = "وضعیت")]
		public FilterBlogCommentStatus Status { get; set; }
	}
}
