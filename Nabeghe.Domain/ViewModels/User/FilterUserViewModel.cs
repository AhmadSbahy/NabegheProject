using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.ViewModels.Common;

namespace Nabeghe.Domain.ViewModels.User
{
	public class FilterUserViewModel : BasePaging<UserViewModel>
	{
		[Display(Name = "جستجو")]
		public string Param { get; set; }

		[Display(Name = "وضعیت")]
		public FilterUserStatus Status { get; set; }
	}
	public enum FilterUserStatus
	{
		[Display(Name = "همه")]
		All,

		[Display(Name = "حذف نشده ها")]
		NotDeleted,
		
		[Display(Name = "حذف شده ها")]
		Deleted,

		[Display(Name = "فعال شده")]
		Active,

		[Display(Name = "فعال نشده")]
		NotActive,

		[Display(Name = "مسدود شده")]
		Ban,
	}
}
