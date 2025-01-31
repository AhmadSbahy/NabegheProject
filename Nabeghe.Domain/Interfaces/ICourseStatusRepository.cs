using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.ViewModels.CourseStatus;

namespace Nabeghe.Domain.Interfaces
{
    public interface ICourseStatusRepository
    {
	    Task<CourseStatus> GetAsync(int id);
	    Task<CourseStatusFilterViewModel> GetAllAsync(CourseStatusFilterViewModel model);
	    Task<bool> IsExist(int id);
	    Task<CourseStatus> AddAsync(CourseStatus courseStatus);
	    Task UpdateAsync(CourseStatus courseStatus);
	    Task DeleteAsync(CourseStatus courseStatus);
	    Task DeleteAsync(int id);
	}
}
