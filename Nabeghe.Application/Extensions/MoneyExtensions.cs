using Nabeghe.Domain.Models.Course;

namespace Nabeghe.Application.Extensions;

public static class MoneyExtensions
{
    public static string ToMoney(this int money)
    {
        return money.ToString("#,0");
    }

    public static string GetPriceAfterDiscount(this int price, int discountPercent)
    {
	    var discountedPrice = price * (1 - (double)discountPercent / 100);
	    return discountedPrice.ToString("#,0");
    }
    public static bool CourseHasDiscount(CourseDiscount? courseDiscount)
    {
	    if (courseDiscount != null && courseDiscount.StartDate <= DateTime.Now &&
	        courseDiscount.EndDate >= DateTime.Now && !courseDiscount.IsDeleted)
	    {
		    return true;
	    }

	    return false;
    }
}