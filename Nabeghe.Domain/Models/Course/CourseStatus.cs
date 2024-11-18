using Nabeghe.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Nabeghe.Domain.Models.Course
{
	public class CourseStatus : BaseEntity<int>
	{
		#region Properties

		[Display(Name = "عنوان وضعیت")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(150, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string StatusTitle { get; set; }

		#endregion

		#region Relations

		public ICollection<Course>? Courses { get; set; }

		#endregion
	}
}
