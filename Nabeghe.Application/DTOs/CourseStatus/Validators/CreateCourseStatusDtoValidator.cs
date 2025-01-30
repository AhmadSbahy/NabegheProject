using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Nabeghe.Application.DTOs.CourseStatus.Validators
{
	public class CreateCourseStatusDtoValidator : AbstractValidator<CreateCourseStatusDto>
	{
        public CreateCourseStatusDtoValidator()
        {
	        RuleFor(p => p.StatusTitle)
		        .NotNull().NotEmpty()
		        .WithMessage("مقدار وارد شده معتبر نمی باشد");
		}
	}
}
