using System.ComponentModel.DataAnnotations;
using Nabeghe.Domain.Enums.Blog;
using Nabeghe.Domain.ViewModels.Common;

namespace Nabeghe.Domain.ViewModels.Blog;

public class ClientSideFilterBlogViewModel : BasePaging<ClientSideBlogViewModel>
{
	public ClientSideFilterBlogViewModel() : base(9) // تعداد آیتم‌ها در هر صفحه
	{
	}

	[Display(Name = "جستجو")]
	public string SearchParam { get; set; }

	[Display(Name = "نویسنده")]
	public int? AuthorId { get; set; }

	[Display(Name = "تاریخ ایجاد از")]
	public DateTime? FromDate { get; set; }

	[Display(Name = "تاریخ ایجاد تا")]
	public DateTime? ToDate { get; set; }

	[Display(Name = "وضعیت")]
	public BlogCommentStatus? Status { get; set; }
}