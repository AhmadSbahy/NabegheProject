using Nabeghe.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.ViewModels.Common;

namespace Nabeghe.Domain.ViewModels.Course
{
	public class ClientSideFilterCourseViewModel : BasePaging<ClientSideCourseViewModel>
	{
		public ClientSideFilterCourseViewModel()
		:base(9)
		{

		}
		[Display(Name = "جستجو")]
		public string Param { get; set; }
		
		[Display(Name = "قیمت")]
		public int Price { get; set; }
	}
}
