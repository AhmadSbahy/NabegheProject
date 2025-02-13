﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Nabeghe.Domain.ViewModels.CourseEpisode
{
	public class CreateCourseEpisodeViewModel
	{
		public int CourseId { get; set; }

		[Display(Name = "عنوان بخش")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(400, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string EpisodeTitle { get; set; }

		[Display(Name = "زمان")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public TimeSpan EpisodeTime { get; set; }


		[Display(Name = "فایل ویدیو")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public IFormFile EpisodeFile { get; set; }
	}

	public enum CreateCourseEpisodeResult
	{
		Success
	}
}
