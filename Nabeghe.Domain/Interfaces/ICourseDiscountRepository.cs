using Nabeghe.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.ViewModels.CourseDiscount;

namespace Nabeghe.Domain.Interfaces
{
	public interface ICourseDiscountRepository
	{
		Task InsertAsync(CourseDiscount courseDiscount);
		Task SaveAsync();
		void Update(CourseDiscount courseDiscount);
		Task<CourseDiscount?> GetByIdAsync(int id);
        void Remove(CourseDiscount courseDiscount);
        Task<CourseDiscount?> GetCourseDiscountAsync(int courseId);
	}
}
