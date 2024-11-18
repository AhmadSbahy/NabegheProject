using Nabeghe.Domain.Models.Common;

namespace Nabeghe.Domain.Models.Order
{
	public class OrderDetail : BaseEntity<int>
    {
        #region Properties

        public int OrderId { get; set; }

        public int CourseId { get; set; }

        public int Price { get; set; }

        #endregion

        #region Relations
        
        public Order? Order { get; set; }

        public Course.Course? Course { get; set; }

        #endregion
    }
}
