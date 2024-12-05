using Nabeghe.Domain.Models.Order;
using Nabeghe.Domain.ViewModels.Order;

namespace Nabeghe.Application.Services.Interfaces;

public interface IOrderService
{
	Task<Order?> GetBasketByUserIdAsync(int userId);
	Task AddCourseToOrderAsync(int userId, int courseId);
	Task<DeleteCourseFromOrderStatus> DeleteOrder(DeleteCourseFromOrderViewModel model);
}