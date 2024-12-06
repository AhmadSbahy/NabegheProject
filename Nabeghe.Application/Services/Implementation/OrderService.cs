using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Order;
using Nabeghe.Domain.ViewModels.Order;
using Nabeghe.Domain.ViewModels.User;

namespace Nabeghe.Application.Services.Implementation;

public class OrderService : IOrderService
{
	private readonly IOrderRepository _orderRepository;
	private readonly ICourseService _courseService;

	public OrderService(IOrderRepository orderRepository, ICourseService courseService)
	{
		_orderRepository = orderRepository;
		_courseService = courseService;
	}

	public async Task<Order?> GetBasketByUserIdAsync(int userId)
	{
		return await _orderRepository.GetOrderWithDetailsByUserIdAsync(userId);
	}

	public async Task AddCourseToOrderAsync(int userId, int courseId)
	{
		var order = await _orderRepository.GetActiveOrderByUserIdAsync(userId);

		if (order != null)
		{
			if (await _orderRepository.IsCourseInOrderAsync(order.Id, courseId))
				throw new InvalidOperationException("این دوره قبلاً به سبد خرید اضافه شده است.");

			var course = await _courseService.GetCourseByIdAsync(courseId)
			             ?? throw new KeyNotFoundException("دوره مورد نظر یافت نشد.");

			var orderDetail = new OrderDetail
			{
				OrderId = order.Id,
				CourseId = courseId,
				CreateDate = DateTime.Now,
				Price = course.CoursePrice
			};

			await _orderRepository.AddOrderDetailAsync(orderDetail);
		}
		else
		{
			order = new Order
			{
				CreateDate = DateTime.Now,
				IsFinally = false,
				UserId = userId
			};

			await _orderRepository.AddOrderAsync(order);

			var course = await _courseService.GetCourseByIdAsync(courseId)
			             ?? throw new KeyNotFoundException("دوره مورد نظر یافت نشد.");

			var orderDetail = new OrderDetail
			{
				OrderId = order.Id,
				CourseId = courseId,
				CreateDate = DateTime.Now,
				Price = course.CoursePrice
			};

			await _orderRepository.AddOrderDetailAsync(orderDetail);
		}
	}

	public async Task<DeleteCourseFromOrderStatus> DeleteOrder(DeleteCourseFromOrderViewModel model)
	{
		var orderDetail = await _orderRepository.GetOrderDetailByIdForEditAsync(model.CourseId, model.OrderId);
	
		if (orderDetail == null)
			return DeleteCourseFromOrderStatus.NotFound;

		await _orderRepository.DeleteOrderDetail(orderDetail);

		return DeleteCourseFromOrderStatus.Success;
	}

	public async Task<List<UserOrderViewModel>?> GetOrderByUserIdAsync(int userId)
	{
		return await _orderRepository.GetAllUserOrders(userId);
	}
    public async Task<List<UserPurchasedCourseViewModel>> GetUserPurchasedCoursesAsync(int userId)
    {
        return await _orderRepository.GetUserPurchasedCoursesAsync(userId);
    }
}