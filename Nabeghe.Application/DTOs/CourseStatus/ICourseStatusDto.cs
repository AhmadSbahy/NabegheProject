using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Application.DTOs.CourseStatus
{
	public interface ICourseStatusDto
	{
		#region Properties

		public int Id { get; set; }
		public string StatusTitle { get; set; }

		#endregion
	}
}
