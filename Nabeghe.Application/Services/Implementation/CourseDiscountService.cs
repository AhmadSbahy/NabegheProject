using Nabeghe.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.ViewModels.CourseDiscount;
using Nabeghe.Domain.Models.User;
using Nabeghe.Domain.ViewModels.Role;
using System.Globalization;
using Nabeghe.Application.Convertor;

namespace Nabeghe.Application.Services.Implementation
{
	public class CourseDiscountService(ICourseDiscountRepository courseDiscountRepository) : ICourseDiscountService
	{
		public async Task<CreateCourseDiscountResult> CreateAsync(CreateCourseDiscountViewModel model)
		{
            string[] std = model.StartDate.Split('/');
            string[] edd = model.EndDate.Split('/');

            CourseDiscount courseDiscount = new CourseDiscount()
			{
				CourseId = model.CourseId,
				DiscountPercent = model.DiscountPercent,
				IsDeleted = false,
				EndDate = new DateTime(int.Parse(edd[0]), int.Parse(edd[1]), int.Parse(edd[2]), new PersianCalendar()),
				StartDate = new DateTime(int.Parse(std[0]), int.Parse(std[1]), int.Parse(std[2]), new PersianCalendar())
            };
			await courseDiscountRepository.InsertAsync(courseDiscount);
			await courseDiscountRepository.SaveAsync();
			return CreateCourseDiscountResult.Success;
		}

		public async Task<UpdateCourseDiscountViewModel?> GetForEditAsync(int id)
		{
			var courseDiscount = await courseDiscountRepository.GetByIdAsync(id);

			if (courseDiscount == null)
			{
				return null;
			}


			return new UpdateCourseDiscountViewModel
			{
				DiscountId = courseDiscount.DiscountId,
				DiscountPercent = courseDiscount.DiscountPercent,
				StartDate = courseDiscount.StartDate.Toshamsi(),
				EndDate = courseDiscount.EndDate.Toshamsi(),
				IsDeleted = courseDiscount.IsDeleted
			};
		}
		public async Task<UpdateCourseDiscountResult> UpdateAsync(UpdateCourseDiscountViewModel model)
		{
			var courseDiscount = await courseDiscountRepository.GetByIdAsync(model.DiscountId);

			if (courseDiscount == null) return UpdateCourseDiscountResult.NotFound;

			string[] std = model.StartDate.Split('/');
			string[] edd = model.EndDate.Split('/');
			// ویرایش مقادیر موجودیت قدیمی به جای ساختن شیء جدید
			courseDiscount.EndDate = new DateTime(int.Parse(edd[0]), int.Parse(edd[1]), int.Parse(edd[2]), new PersianCalendar());
			courseDiscount.StartDate = new DateTime(int.Parse(std[0]), int.Parse(std[1]), int.Parse(std[2]), new PersianCalendar());
			courseDiscount.DiscountPercent = model.DiscountPercent;

			courseDiscountRepository.Update(courseDiscount);
			
			await courseDiscountRepository.SaveAsync();
			return UpdateCourseDiscountResult.Success;
		}
		public async Task<DeleteCourseDiscountResult> DeleteAsync(int id)
		{
			var courseDiscount = await courseDiscountRepository.GetByIdAsync(id);

			if (courseDiscount == null) return DeleteCourseDiscountResult.NotFound;

			courseDiscountRepository.Remove(courseDiscount);

			await courseDiscountRepository.SaveAsync();

			return DeleteCourseDiscountResult.Success;
		}

	}
}
