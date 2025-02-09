using Nabeghe.Application.Convertor;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.ViewModels.CourseDiscount;
using System.Globalization;

namespace Nabeghe.Application.Services.Implementation
{
    public class CourseDiscountService(ICourseDiscountRepository courseDiscountRepository) : ICourseDiscountService
	{
		
		public async Task<CreateCourseDiscountResult> CreateAsync(CreateCourseDiscountViewModel model)
		{
			// تبدیل تاریخ‌ها به DateTime
			string[] std = model.StartDate.Split('/');
			string[] edd = model.EndDate.Split('/');

			var existingRecord = await courseDiscountRepository.GetCourseDiscountAsync(model.CourseId);

			if (existingRecord != null)
			{
				existingRecord.DiscountPercent = model.DiscountPercent;
				existingRecord.StartDate = new DateTime(int.Parse(std[0]), int.Parse(std[1]), int.Parse(std[2]), new PersianCalendar());
				existingRecord.EndDate = new DateTime(int.Parse(edd[0]), int.Parse(edd[1]), int.Parse(edd[2]), new PersianCalendar());
				existingRecord.IsDeleted = false;

				 courseDiscountRepository.Update(existingRecord);
			}
			else
			{
				CourseDiscount courseDiscount = new CourseDiscount()
				{
					CourseId = model.CourseId,
					DiscountPercent = model.DiscountPercent,
					IsDeleted = false,
					EndDate = new DateTime(int.Parse(edd[0]), int.Parse(edd[1]), int.Parse(edd[2]), new PersianCalendar()),
					StartDate = new DateTime(int.Parse(std[0]), int.Parse(std[1]), int.Parse(std[2]), new PersianCalendar())
				};

				await courseDiscountRepository.InsertAsync(courseDiscount);
			}

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
            courseDiscount.StartDate = new DateTime(int.Parse(std[0]), int.Parse(std[1]), int.Parse(std[2]), new PersianCalendar());
            courseDiscount.EndDate = new DateTime(int.Parse(edd[0]), int.Parse(edd[1]), int.Parse(edd[2]), new PersianCalendar());
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
