using System.ComponentModel.DataAnnotations;

namespace Nabeghe.Domain.Models.Common
{
	public class BaseEntity<T>
	{
		[Key]
		public T Id { get; set; }

		public DateTime CreateDate { get; set; }
	}
}
