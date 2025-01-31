using System.ComponentModel.DataAnnotations;

namespace Nabeghe.Web.Models.CourseStatus
{
	public class CreateCourseStatusViewModel
	{
		public DateTime CreateDate { get; set; }
		[Required(ErrorMessage = "لطفا عنوان وضعیت دوره را وارد کنید")]
		public string StatusTitle { get; set; }
	}
}
