using Nabeghe.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Application.DTOs.CourseStatus
{
	public class UpdateCourseStatusDto : BaseDto, ICourseStatusDto
	{
		#region Properties

		public string StatusTitle { get; set; }

		#endregion
	}
}
