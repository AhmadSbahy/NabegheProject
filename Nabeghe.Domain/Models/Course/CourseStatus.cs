using Nabeghe.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Nabeghe.Domain.Models.Course
{
	public class CourseStatus : BaseEntity<int>
	{
		#region Properties

		public string StatusTitle { get; set; }

		#endregion

		#region Relations

		public ICollection<Course>? Courses { get; set; }

		#endregion
	}
}
