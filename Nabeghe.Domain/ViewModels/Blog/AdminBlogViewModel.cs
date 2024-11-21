using System.ComponentModel.DataAnnotations;

namespace Nabeghe.Domain.ViewModels.Blog;

public class AdminBlogViewModel
{
	public int Id { get; set; }

	[Display(Name = "عنوان مقاله")]
	public string BlogTitle { get; set; }

	[Display(Name = "عکس مقاله")]
	public string BlogImage { get; set; }

	[Display(Name = "تاریخ ایجاد")]
	public DateTime CreateDate { get; set; }

	[Display(Name = "نام نویسنده")]
	public string AuthorName { get; set; }
}