using Nabeghe.Domain.ViewModels.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.ViewModels.CourseDiscount;

namespace Nabeghe.Application.Services.Interfaces
{
    public interface ICourseDiscountService
    {
		Task<CreateCourseDiscountResult> CreateAsync(CreateCourseDiscountViewModel model);
		Task<UpdateCourseDiscountViewModel?> GetForEditAsync(int id);
		Task<UpdateCourseDiscountResult> UpdateAsync(UpdateCourseDiscountViewModel model);
		Task<DeleteCourseDiscountResult> DeleteAsync(int id);
	}
}
