using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabeghe.Application.Extensions;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Shared;
using Nabeghe.Domain.ViewModels.User;

namespace Nabeghe.Web.Areas.UserPanel.Controllers
{
	public class UserController
		(IUserService _userService,
			IOrderService orderService)
    : UserPanelBaseController
    {
        #region Actions

        #region Edit Profile

		public async Task<IActionResult> EditProfile()
		{
            EditUserProfileViewModel? model = await _userService.GetProfileForEditAsync(User.GetUserId());
            if (model == null)
                return NotFound();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditUserProfileViewModel model)
		{
            #region Validations
            if (!ModelState.IsValid)
                return View(model);
            #endregion

            model.UserId = User.GetUserId();

            EditUserProfileResult result = await _userService.UpdateProfileAsync(model);

            switch (result)
            {
                case EditUserProfileResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.UpdateProfileSuccessfullyDone;
                    return RedirectToAction(nameof(EditProfile));
                
                case EditUserProfileResult.CurrentPasswordNotCorrect:
                    TempData[ErrorMessage] = ErrorMessages.CurrentPasswordNotCorrect;
                    return RedirectToAction(nameof(EditProfile));

                case EditUserProfileResult.UserNotFound:
                    TempData[ErrorMessage] = ErrorMessages.UserNotFound;
                    return RedirectToAction(nameof(EditProfile));
            }

            return View(model);
        }
		#endregion

		#region Favorite Course

		public async Task<IActionResult> FavoriteCourse()
        {
            var courses = await _userService.GetCourseFavoriteListAsync(User.GetUserId());
			return View(courses);
		}

		#endregion

		#region User Comments

		public async Task<IActionResult> UserComment()
		{
			var userComments = await _userService.GetUserCourseCommentListAsync(User.GetUserId());
			return View(userComments);
		}

        #endregion

        #region User Orders

        public async Task<IActionResult> Orders()
        {
	        var orders = await orderService.GetOrderByUserIdAsync(User.GetUserId());
            return View(orders);
        }

        #endregion

        #endregion
    }
}
	