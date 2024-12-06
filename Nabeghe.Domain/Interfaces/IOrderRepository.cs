using Nabeghe.Domain.Models.Order;
using Nabeghe.Domain.ViewModels.Order;
using Nabeghe.Domain.ViewModels.User;

namespace Nabeghe.Domain.Interfaces;

public interface IOrderRepository
{
	Task<Order?> GetActiveOrderByUserIdAsync(int userId);
	Task<Order?> GetOrderWithDetailsByUserIdAsync(int userId);
	Task AddOrderAsync(Order order);
	Task AddOrderDetailAsync(OrderDetail orderDetail);
	Task<bool> IsCourseInOrderAsync(int orderId, int courseId);
	Task<OrderDetail?> GetOrderDetailByIdForEditAsync(int courseId,int orderId);
	Task DeleteOrderDetail(OrderDetail orderDetail);
	Task<List<UserOrderViewModel>?> GetAllUserOrders(int userId);
    Task<List<UserPurchasedCourseViewModel>> GetUserPurchasedCoursesAsync(int userId);

}