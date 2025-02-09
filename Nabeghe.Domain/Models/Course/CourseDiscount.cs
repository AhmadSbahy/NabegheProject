using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.Models.Course
{
    public class CourseDiscount
    {
        [Key]
        public int DiscountId { get; set; }

        public int CourseId { get; set; }

        [Display(Name = "درصد تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        public int DiscountPercent { get; set; }

        [Display(Name = "تاریخ شروع")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        public DateTime StartDate { get; set; }

        [Display(Name = "تاریخ پایان")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        public DateTime EndDate { get; set; }

        public bool IsDeleted { get; set; }
			
        [ForeignKey(nameof(CourseId))]
        public Course? Course { get; set; }

       
    }
}
