using System.ComponentModel.DataAnnotations;

namespace Nabeghe.Domain.ViewModels.BlogComment;

public enum FilterBlogCommentStatus
{
	[Display(Name = "همه")] All,

	[Display(Name = "منتظر بررسی")] Pending,

	[Display(Name = "تایید شده")] Confirmed,

	[Display(Name = "رد شده")] Rejected
}