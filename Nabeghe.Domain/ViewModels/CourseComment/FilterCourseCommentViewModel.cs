using System.ComponentModel.DataAnnotations;
using Nabeghe.Domain.ViewModels.Common;

namespace Nabeghe.Domain.ViewModels.CourseComment;

public class FilterCourseCommentViewModel : BasePaging<CourseCommentViewModel>
{
	public int CourseId { get; set; }
	[Display(Name = "جستجو")]
	public string Param { get; set; }
	[Display(Name = "وضعیت")]
	public FilterCourseCommentStatus Status { get; set; }
}