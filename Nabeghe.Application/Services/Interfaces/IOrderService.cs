using Nabeghe.Domain.Models.Order;
using Nabeghe.Domain.ViewModels.Order;
using Nabeghe.Domain.ViewModels.User;

namespace Nabeghe.Application.Services.Interfaces;

public interface IOrderService
{
	Task<Order?> GetBasketByUserIdAsync(int userId);
	Task AddCourseToOrderAsync(int userId, int courseId);
	Task<DeleteCourseFromOrderStatus> DeleteOrder(DeleteCourseFromOrderViewModel model);
	Task<List<UserOrderViewModel>?> GetOrderByUserIdAsync(int userId);
    Task<List<UserPurchasedCourseViewModel>> GetUserPurchasedCoursesAsync(int userId);

}