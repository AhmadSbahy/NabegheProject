using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Order;
using Nabeghe.Domain.ViewModels.Order;
using Nabeghe.Domain.ViewModels.User;
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
		return await _context.OrderDetails.Include(od=>od.Order).FirstOrDefaultAsync(od => od.CourseId == courseId && od.OrderId == orderId);
	}

	public async Task DeleteOrderDetail(OrderDetail orderDetail)
    {
        _context.OrderDetails.Remove(orderDetail);
        await _context.SaveChangesAsync();
	}

	public async Task<List<UserOrderViewModel>?> GetAllUserOrders(int userId)
	{
		return await _context.Orders
			.Include(o => o.OrderDetails)
			.ThenInclude(od => od.Course)
			.ThenInclude(c => c.CourseDiscount)
			.Where(o => o.UserId == userId)
			.Select(o => new UserOrderViewModel
			{
				OrderId = o.Id,
				CreateDate = o.CreateDate,
				IsPayed = o.IsFinally,
				Price = o.IsFinally
					? o.TotalOrderPrice
					: o.OrderDetails.Sum(od =>
						(int)(od.Price * (1 - (od.Course.CourseDiscount != null
							? od.Course.CourseDiscount.DiscountPercent / 100m
							: 0))))
			})
			
			.OrderByDescending(o=> o.CreateDate)
			.ToListAsync();
	}
    public async Task<List<UserPurchasedCourseViewModel>> GetUserPurchasedCoursesAsync(int userId)
    {
        return await _context.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Course)
            .ThenInclude(c => c.User)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Course)
            .ThenInclude(c => c.CourseCategory)
            .ThenInclude(cc => cc.Courses)
            .ThenInclude(c => c.CourseStatus)
            .ThenInclude(cs => cs.Courses)
            .ThenInclude(c => c.CourseDiscount)
            .Where(o => o.UserId == userId && o.IsFinally)
            .SelectMany(o => o.OrderDetails)
            .Select(od => new UserPurchasedCourseViewModel
            {
                CourseId = od.CourseId,
                CourseTitle = od.Course.CourseTitle,
                CourseSlug = od.Course.Slug,
                Price = od.Price,
                CategoryName = od.Course.CourseCategory.Title,
                CourseImage = od.Course.CourseImageName,
                TeacherFullName = od.Course.User != null ? $"{od.Course.User.FirstName} {od.Course.User.LastName}" : "نامشخص",
                TeacherAvatar = od.Course.User.Avatar,
                CourseStatus = od.Course.CourseStatus.StatusTitle,
                CourseDiscount = od.Course.CourseDiscount
            })
            .ToListAsync();
    }
}