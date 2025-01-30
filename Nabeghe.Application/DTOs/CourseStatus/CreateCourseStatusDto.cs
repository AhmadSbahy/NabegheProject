using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Application.DTOs.Common;

namespace Nabeghe.Application.DTOs.CourseStatus
{
	public class CreateCourseStatusDto : BaseDto,ICourseStatusDto
	{
		#region Properties

		public string StatusTitle { get; set; }

		#endregion
	}
}
