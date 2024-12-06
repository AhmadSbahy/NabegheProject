using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.Order
{
    public class UserOrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsPayed { get; set; }
        public int Price { get; set; }
    }
}
