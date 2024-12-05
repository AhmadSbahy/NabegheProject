using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Order;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Infra.Data.Repositories;

public class OrderRepository : IOrderRepository
{
	private readonly NabegheContext _context;

	public OrderRepository(NabegheContext context)
	{
		_context = context;
	}

	public async Task<Order?> GetActiveOrderByUserIdAsync(int userId)
	{
		return await _context.Orders
			.FirstOrDefaultAsync(o => o.UserId == userId && !o.IsFinally);
	}

	public async Task<Order?> GetOrderWithDetailsByUserIdAsync(int userId)
	{
		return await _context.Orders
			.Include(o => o.OrderDetails)
			.ThenInclude(od => od.Course)
			.ThenInclude(c => c.CourseStatus)
			.Include(o => o.OrderDetails)
			.ThenInclude(od => od.Course)
			.ThenInclude(c => c.CourseDiscount)
			.Include(o => o.OrderDetails)
			.ThenInclude(od => od.Course)
			.ThenInclude(c => c.User)
			.ThenInclude(u => u.CourseLikes)
			.FirstOrDefaultAsync(o => o.UserId == userId && !o.IsFinally);
	}

	public async Task AddOrderAsync(Order order)
	{
		await _context.Orders.AddAsync(order);
		await _context.SaveChangesAsync();
	}

	public async Task AddOrderDetailAsync(OrderDetail orderDetail)
	{
		await _context.OrderDetails.AddAsync(orderDetail);
		await _context.SaveChangesAsync();
	}

	public async Task<bool> IsCourseInOrderAsync(int orderId, int courseId)
	{
		return await _context.OrderDetails.AnyAsync(od => od.OrderId == orderId && od.CourseId == courseId);
	}

	public async Task<OrderDetail?> GetOrderDetailByIdForEditAsync(int courseId, int orderId)
	{
		return await _context.OrderDetails.FirstOrDefaultAsync(od => od.CourseId == courseId && od.OrderId == orderId);
	}

	public async Task DeleteOrderDetail(OrderDetail orderDetail)
	{
		_context.OrderDetails.Remove(orderDetail);
		await _context.SaveChangesAsync();
	}
}