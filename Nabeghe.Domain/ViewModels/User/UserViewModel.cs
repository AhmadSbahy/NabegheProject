using Nabeghe.Domain.Enums.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.User
{
	public class UserViewModel
	{
		public int Id { get; set; }

		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public string Mobile { get; set; }

		public string? Email { get; set; }

		public bool IsDelete { get; set; }

		public UserStatus Status { get; set; }

		public DateTime CreateDate { get; set; }
	}
}
