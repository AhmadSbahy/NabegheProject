using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.Order
{
	public class DeleteCourseFromOrderViewModel
	{
		public int OrderId { get; set; }	
		public int CourseId { get;set; }
	}

	public enum DeleteCourseFromOrderStatus
	{
		Success,
		NotFound
	}
}
