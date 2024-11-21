using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Nabeghe.Domain.ViewModels.Blog;

public class AdminEditBlogViewModel
{
	public int Id { get; set; }

	[Display(Name = "عنوان مقاله")]
	[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	public string BlogTitle { get; set; }

	[Display(Name = "توضیحات مقاله")]
	[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	public string BlogDescription { get; set; }

	[Display(Name = "عکس مقاله")]
	public IFormFile? BlogImage { get; set; }

	[Display(Name = "نویسنده")]
	[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	public int AuthorId { get; set; }

	public string? BlogImageName { get; set; }
}

public enum AdminEditBlogStatus
{
	Success,
	NotFound
}