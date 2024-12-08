using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.Account
{
   public class ActivateUserViewModel
    {
        public string? Mobile { get; set; }
        [Display(Name = "کد تایید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string ConfirmCode { get; set; }
    }
    public enum ActivateUserResult
	{
	    Success,
	    UserNotFound,
		CodeIsNotCorrect
	}
}
