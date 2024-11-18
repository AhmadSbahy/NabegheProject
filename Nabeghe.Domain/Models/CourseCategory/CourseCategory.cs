using Nabeghe.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabeghe.Domain.Models.CourseCategory
{
	public class CourseCategory : BaseEntity<int>
	{
		#region Properties

		[Display(Name = "عنوان")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(150, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string Title { get; set; }

		public int? ParentId { get; set; }

		public bool IsDeleted { get; set; }

		#endregion
		
		#region Relations

		[ForeignKey(nameof(ParentId))]
		public CourseCategory? Category { get; set; }
		public ICollection<CourseCategory>? CourseCategories { get; set; }
		public ICollection<Course.Course>? Courses { get; set; }

		#endregion
	}
}
