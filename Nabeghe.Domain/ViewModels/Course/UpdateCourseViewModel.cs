using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Nabeghe.Domain.ViewModels.Course
{
	public class UpdateCourseViewModel
    {
	    public int Id { get; set; }	

	    [Display(Name = "استاد")]
	    public int TeacherId { get; set; }

	    [Display(Name = "دسته بندی")]
	    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	    public int CategoryId { get; set; }

	    [Display(Name = "وضعیت دوره")]
	    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	    public int StatusId { get; set; }

	    [Display(Name = "عنوان دوره")]
	    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	    [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
	    public string CourseTitle { get; set; }

	    [Display(Name = "Slug")]
	    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	    public string Slug { get; set; }

		[Display(Name = "شرح عنوان دوره")]
	    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	    [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
	    public string CourseSubTitle { get; set; }

	    [Display(Name = "شرح دوره")]
	    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	    public string CourseDescription { get; set; }

	    [Display(Name = "قیمت دوره")]
	    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	    public int CoursePrice { get; set; }


	    public DateTime CreateDate { get; set; }

	    [Display(Name = "عکس دوره")]
	    public IFormFile? NewImageFile { get; set; }

	    public string? ImageName { get; set; }
	}
	public enum UpdateCourseResult
	{
		Success,
		CourseNotFound
	}
}
