using System.ComponentModel.DataAnnotations;

namespace Nabeghe.Domain.ViewModels.CourseCategory
{
	public class UpdateCourseCategoryViewModel
	{
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
        public string Title { get; set; }

        [Display(Name = "دسته بندی والد")]
        public int? ParentId { get; set; }

    }

    public enum UpdateCourseCategoryResult
	{
        Success,
		CourseCategoryNotFound
	}
}
