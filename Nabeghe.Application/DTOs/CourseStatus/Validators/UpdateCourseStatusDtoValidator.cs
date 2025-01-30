using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Application.DTOs.CourseStatus.Validators
{
	public class UpdateCourseStatusDtoValidator : AbstractValidator<UpdateCourseStatusDto>
	{
		public UpdateCourseStatusDtoValidator()
		{
			RuleFor(p => p.StatusTitle)
				.NotNull().NotEmpty()
				.WithMessage("مقدار وارد شده معتبر نمی باشد");
		}
	}
}
