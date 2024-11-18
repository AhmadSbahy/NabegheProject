using Nabeghe.Domain.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace Nabeghe.Domain.ViewModels.CourseCategory
{
	public class FilterCourseCategoryViewModel : BasePaging<CourseCategoryViewModel>
    {
	    [Display(Name = "عنوان")]
		public string? Title { get; set; }

        [Display(Name = "وضعیت")]
        public FilterCourseCategoryStatus Status { get; set; }
	}
	public enum FilterCourseCategoryStatus	
	{
		[Display(Name = "حذف نشده ها")]
		NotDeleted,
		[Display(Name = "همه")]
		All,
		[Display(Name = "حذف شده ها")]
		Deleted
	}
}
