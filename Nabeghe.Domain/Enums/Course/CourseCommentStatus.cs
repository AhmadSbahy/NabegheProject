using System.ComponentModel.DataAnnotations;

namespace Nabeghe.Domain.Enums.Course
{
	public enum CourseCommentStatus
	{
		[Display(Name = "منتظر بررسی")] Pending,

		[Display(Name = "تایید شده")] Confirmed,

		[Display(Name = "رد شده")] Rejected
	}
}
