using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Nabeghe.Application.DTOs.CourseStatus.Validators
{
	public class CourseStatusDtoValidator : AbstractValidator<ICourseStatusDto>
	{
		public CourseStatusDtoValidator()
		{
			RuleFor(p => p.StatusTitle)
				.NotNull().NotEmpty()
				.WithMessage("مقدار وارد شده معتبر نمی باشد");

			RuleFor(p => p.Id).GreaterThan(0)
				.WithMessage("وضعیت دوره پیدا نشد");
		}
	}
}
