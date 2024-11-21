using System.ComponentModel.DataAnnotations;
using Nabeghe.Domain.ViewModels.Common;

namespace Nabeghe.Domain.ViewModels.Blog;

public class AdminFilterBlogViewModel : BasePaging<AdminBlogViewModel>
{
	public AdminFilterBlogViewModel() : base(15) // تعداد آیتم‌ها در هر صفحه
	{
	}

	[Display(Name = "جستجو")]
	public string SearchParam { get; set; }

	[Display(Name = "نویسنده")]
	public string AuthorName { get; set; }
}
