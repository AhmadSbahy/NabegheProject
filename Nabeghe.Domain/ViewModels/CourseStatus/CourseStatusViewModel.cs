using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nabeghe.Domain.ViewModels.CourseStatus
{
   public class CourseStatusViewModel
    {
        public int Id { get; set; } 

        [Display(Name = "عنوان وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
        public string StatusTitle { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
