using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.Shared
{
	public class SuccessMessages
	{
		#region Account

		public static string RegisterSuccessfullyDone = "ثبت نام شما با موفقیت انجام شد.";
		public static string ForgotPasswordSuccessfullyDone = "کد تایید برای شماره موبایل شما ارسال شد.";
		public static string ResetPasswordSuccessfullyDone = "تغیر کلمه عبور شما با موفقیت انجام شد.";

		#endregion

		#region User

		public static string UpdateProfileSuccessfullyDone = "ویرایش حساب کاربری شما با موفقیت انجام شد.";
		public static string CreateUserSuccessfullyDone = ".حساب کاربری شما با موفقیت ساخته شد";
		public static string EditUserSuccessfullyDone = ".حساب کاربری شما با موفقیت ویرایش شد";
		public static string DeleteUserSuccessfullyDone = ".حساب کاربری شما با موفقیت حذف شد";


		#endregion

		#region Role

		public static string CreateRoleSuccessfullyDone = ".نقش با موفقیت ساخته شد";
		public static string EditRoleSuccessfullyDone = ".نقش با موفقیت ویرایش شد";
		public static string DeleteRoleSuccessfullyDone = ".نقش با موفقیت حذف شد";

		#endregion

		#region Course Category

		public static string CreateCourseCategorySuccessfullyDone = "دسته بندی با موفقیت ساخته شد.";
		public static string UpdateCourseCategorySuccessfullyDone = "دسته بندی با موفقیت ویرایش شد.";
		public static string DeleteCourseCategorySuccessfullyDone = "دسته بندی با موفقیت حذف شد.";

		#endregion

		#region Course

		public static string CreateCourseSuccessfullyDone = "دوره با موفقیت ساخته شد.";
		public static string UpdateCourseSuccessfullyDone = "دوره با موفقیت ویرایش شد.";
		public static string DeleteCourseSuccessfullyDone = "دوره با موفقیت حذف شد.";

		#endregion

		#region Course Episode

		public static string CreateCourseEpisodeSuccessfullyDone = "قسمت با موفقیت ساخته شد.";
		public static string UpdateCourseEpisodeSuccessfullyDone = "قسمت با موفقیت ویرایش شد.";
		public static string DeleteCourseEpisodeSuccessfullyDone = "قسمت با موفقیت حذف شد.";

		#endregion

		#region Course Discount

		public static string CreateCourseDiscountSuccessfullyDone = "تخفیف برای دوره با موفقیت ساخته شد.";
		public static string UpdateCourseDiscountSuccessfullyDone = "تخفیف برای دوره با موفقیت ویرایش شد.";
		public static string DeleteCourseDiscountSuccessfullyDone = "تخفیف برای دوره با موفقیت حذف شد.";

		#endregion

		#region Blog

		public static string CreateBlogSuccessfullyDone = "مقاله با موفقیت ساخته شد.";
		public static string UpdateBlogSuccessfullyDone = "مقاله با موفقیت ویرایش شد.";
		public static string DeleteBlogSuccessfullyDone = "مقاله با موفقیت حذف شد.";
		#endregion

	}

	public class ErrorMessages
	{
		public static string OperationFailed = "خطایی رخ داده است لطفا بعدا تلاش کنید.";

		#region User
		public static string UserIsBan = "حساب کاربری شما مسدود است.";
		public static string UserIsNotLogin = "برای دسترسی به این قسمت باید ثبت نام کنید.";
		public static string UserIsNotActive = "حساب کاربری شما غیرفعال است.";
		public static string UserNotFound = "حساب کاربری پیدا نشد.";
		public static string CurrentPasswordNotCorrect = "کلمه عبور فعلی شما صحیح نمی باشد.";
		public static string DuplicatedMobile = "کاربری با این شماره موبایل ثبت نام کرده است.";
		#endregion

		#region Role

		public static string NotSelectedRole = ".نقشی انتخاب نشده است";
		public static string NotSelectedPermission = ".دسترسی انتخاب نشده است";
		public static string RoleNotFound = "نقشی پیدا نشد.";

		#endregion

		#region Course Category

		public static string CourseCategoryNotFound = "دسته بندی پیدا نشد.";

		#endregion

		#region Course

		public static string CourseNotFound = "دوره ای پیدا نشد.";

		#endregion

		#region Course Episode

		public static string CourseEpisodeNotFound = "قسمتی پیدا نشد.";

		#endregion

		#region Course Discount

		public static string CourseDiscountNotFound = "تخفیفی پیدا نشد.";

		#endregion

		#region Blog

		public static string BlogNotFound = "مقاله ای پیدا نشد.";

		#endregion
	}
}
