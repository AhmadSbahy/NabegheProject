using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Nabeghe.Domain.ViewModels.Blog;

public class AdminCreateBlogViewModel
{
	[Display(Name = "عنوان مقاله")]
	[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	public string BlogTitle { get; set; }

	[Display(Name = "Slug")]
	[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	public string Slug { get; set; }

	[Display(Name = "توضیحات مقاله")]
	[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	public string BlogDescription { get; set; }

	[Display(Name = "عکس مقاله")]
	[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	public IFormFile BlogImage { get; set; }

	[Display(Name = "نویسنده")]
	[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	public int AuthorId { get; set; }
}

public enum AdminCreateBlogStatus
{
	Success
}