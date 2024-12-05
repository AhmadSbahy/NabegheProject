using Nabeghe.Domain.Models.Common;

namespace Nabeghe.Domain.Models.Order
{
	public class Order : BaseEntity<int>
    {
        #region Properties

        public int UserId { get; set; }

        public bool IsFinally { get; set; }

        public string? Authority { get; set; } = string.Empty;

        public string? RefId { get; set; } = string.Empty;

        #endregion

        #region Relations

        public User.User? User { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }

        #endregion
    }
}
