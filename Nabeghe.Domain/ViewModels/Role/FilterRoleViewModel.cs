using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.ViewModels.Common;

namespace Nabeghe.Domain.ViewModels.Role
{
	public class FilterRoleViewModel : BasePaging<RoleViewModel>
	{
		[Display(Name = "جستجو")]
		public string Param { get; set; }
	}
}
