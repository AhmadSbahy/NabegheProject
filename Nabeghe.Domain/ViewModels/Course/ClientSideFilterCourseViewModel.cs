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
		: base(9)
		{

		}
		[Display(Name = "جستجو")]
		public string Param { get; set; }

		[Display(Name = "جستجو")]
		public string ParamRes { get; set; }

		public int? CategoryId { get; set; }

		public bool IsFree { get; set; }

		public bool IsNotFree { get; set; }

		public bool IsFreeRes { get; set; }

		public bool IsNotFreeRes { get; set; }

		public ClientSideFilterCourseStatus Status { get; set; }
	}

	public enum ClientSideFilterCourseStatus
	{
		[Display(Name = "همه")]
		All,
		[Display(Name = "جدید ترین")]
		Newest,
		[Display(Name = "قدیمی ترین")]
		Oldest,
		[Display(Name = "درحال برگزاری")]
		Recording,
		[Display(Name = "به اتمام رسیده")]
		Finished,
		[Display(Name = "رایگان")]
		Free,
		[Display(Name = "غیر رایگان")]
		NotFree
	}
}
