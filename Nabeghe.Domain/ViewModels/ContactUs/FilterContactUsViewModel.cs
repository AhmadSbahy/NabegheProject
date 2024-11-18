using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.ViewModels.Common;

namespace Nabeghe.Domain.ViewModels.ContactUs
{
    public class FilterContactUsViewModel : BasePaging<ContactUsViewModel>
    {
        [Display(Name = "جستجو")]
        public string Param { get; set; }
        [Display(Name = "وضعیت")]
		public FilterContactUsStatus FilterContactUsStatus { get; set; }
    }

    public enum FilterContactUsStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "پاسخ داده شده")]
        Answered,
        [Display(Name = "پاسخ داده نشده")]
        NotAnswered
    }
}
