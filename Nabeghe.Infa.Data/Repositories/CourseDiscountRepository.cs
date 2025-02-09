using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.Models.User;
using Nabeghe.Domain.ViewModels.CourseDiscount;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Infra.Data.Repositories
{
   public class CourseDiscountRepository(NabegheContext context) : ICourseDiscountRepository
	{
		public async Task InsertAsync(CourseDiscount courseDiscount)
		{
			await context.AddAsync(courseDiscount);
		}

		public async Task SaveAsync()
		{
			await context.SaveChangesAsync();
		}

		public void Update(CourseDiscount courseDiscount)
			=> context.CourseDiscounts.Update(courseDiscount);

		public async Task<CourseDiscount?> GetByIdAsync(int id)
			=> await context.CourseDiscounts.FirstOrDefaultAsync(r => r.DiscountId == id);

		public void Remove(CourseDiscount courseDiscount)
			=>  context.CourseDiscounts.Remove(courseDiscount);

		public async Task<CourseDiscount?> GetCourseDiscountAsync(int courseId)
			=> await context.CourseDiscounts.FirstOrDefaultAsync(cd => cd.CourseId == courseId);
	}
}
