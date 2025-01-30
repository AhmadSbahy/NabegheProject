using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Models.Course;

namespace Nabeghe.Domain.Interfaces
{
    public interface ICourseStatusRepository
    {
	    Task<CourseStatus> GetAsync(int id);
	    Task<IReadOnlyList<CourseStatus>> GetAllAsync();
	    Task<bool> IsExist(int id);
	    Task<CourseStatus> AddAsync(CourseStatus courseStatus);
	    Task UpdateAsync(CourseStatus courseStatus);
	    Task DeleteAsync(CourseStatus courseStatus);
	    Task DeleteAsync(int id);
	}
}
