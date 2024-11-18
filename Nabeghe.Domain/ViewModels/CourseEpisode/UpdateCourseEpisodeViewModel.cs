using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Nabeghe.Domain.ViewModels.CourseEpisode
{
   public class UpdateCourseEpisodeViewModel
    {
	    public int Id { get; set; }

	    public int CourseId { get; set; }

	    [Display(Name = "عنوان بخش")]
	    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	    [MaxLength(400, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
	    public string EpisodeTitle { get; set; }

	    [Display(Name = "زمان")]
	    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	    public TimeSpan EpisodeTime { get; set; }

	    public DateTime CreateDate { get; set; }


	    [Display(Name = "فایل ویدیو جدید")]
	    public IFormFile? NewEpisodeFile { get; set; }


	    public string? EpisodeVideoName { get; set; }
	}

    public enum UpdateCourseEpisodeResult
    {
        Success,
        NotFound
    }
}
