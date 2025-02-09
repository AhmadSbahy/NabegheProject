using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Shared;
using Nabeghe.Domain.ViewModels.Account;
using System.Security.Claims;
using Nabeghe.Domain.Models.User;

namespace Nabeghe.Web.Controllers
{
    public class AccountController
        (IAccountService _accountService,
            IUserService _userService)
        : SiteBaseController
    {
        #region Actions

        #region Register
       
        [HttpGet("register")]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", new { area = "UserPanel" });

            return View();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            #region Validations

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            RegisterResult result = await _accountService.RegisterAsync(model);

            switch (result)
            {
                case RegisterResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.RegisterSuccessfullyDone;
                    TempData["Mobile"] = model.Mobile;
					return RedirectToAction("CheckMobileCode", "Account");
                    break;

                case RegisterResult.MobileDuplicated:
                    TempData[ErrorMessage] = ErrorMessages.DuplicatedMobile;
                    break;

                case RegisterResult.Error:
                    TempData[ErrorMessage] = ErrorMessages.OperationFailed;
                    break;
            }

            return View(model);
        }

        #endregion

        #region Check the Rigister Mobile

        [HttpGet("activate")]
        public IActionResult CheckMobileCode()
        {
            string mobile = TempData["Mobile"]?.ToString();

            if (string.IsNullOrEmpty(mobile))
            {
                return NotFound();
            }
            return View(new ActivateUserViewModel()
            {
	            Mobile = mobile
            });
        }

        [HttpPost("activate")]
        public async Task<IActionResult> CheckMobileCode(ActivateUserViewModel model)
        {
	        #region Validations

	        if (!ModelState.IsValid)
	        {
		        return View(model);
	        }

	        #endregion

	        ActivateUserResult result = await _accountService.ActivateUserAsync(model);

	        switch (result)
	        {
		        case ActivateUserResult.Success:
			        TempData[SuccessMessage] = SuccessMessages.ActivateUserSuccessfullyDone;
			        return RedirectToAction(nameof(Login));

		        case ActivateUserResult.CodeIsNotCorrect:
			        TempData[ErrorMessage] = ErrorMessages.CodeIsNotCorrect;
			        break;
		       
		        case ActivateUserResult.UserNotFound:
			        TempData[ErrorMessage] = ErrorMessages.UserNotFound;
					break;

	        }
	        return View();
        }

		#endregion

		#region Login
		[HttpGet("login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", new { area = "UserPanel" });
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            #region Validations

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            LoginResult result = await _accountService.LoginAsync(model);

            switch (result)
            {
                case LoginResult.Success:
                    User? user = await _userService.GetByMobileAsync(model.Mobile);

                    if (user == null)
                    {
                        TempData[ErrorMessage] = ErrorMessages.UserNotFound;
                        return View(model);
                    }

                    #region Login

                    List<Claim> claims = new List<Claim>()
                    {
                        new(ClaimTypes.MobilePhone,model.Mobile),
                        new(ClaimTypes.NameIdentifier,user.Id.ToString())
                    };

                    ClaimsIdentity claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(claimIdentity);

                    HttpContext.SignInAsync(claimPrincipal, new AuthenticationProperties() { IsPersistent = true });

                    #endregion

                    return RedirectToAction("Index", "Home");
                    break;

                case LoginResult.UserNotFound:
                    TempData[ErrorMessage] = ErrorMessages.UserNotFound;
                    break;

                case LoginResult.UserIsBan:
                    TempData[ErrorMessage] = ErrorMessages.UserIsBan;
                    break;

                case LoginResult.UserIsNotActive:
                    TempData[ErrorMessage] = ErrorMessages.UserIsNotActive;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return View(model);
        }

        #endregion

        #region Logout

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        #endregion

        #region Forgot Password
        [HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", new { area = "UserPanel" });
            return View();
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            #region Validations

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            ForgotPasswordResult result = await _accountService.ForgotPasswordAsync(model);

            switch (result)
            {
                case ForgotPasswordResult.success:
                    TempData["Mobile"] = model.Mobile;
                    TempData[SuccessMessage] = SuccessMessages.ForgotPasswordSuccessfullyDone;
                    return RedirectToAction(nameof(ResetPassword));

                case ForgotPasswordResult.UserNotFound:
                    TempData[ErrorMessage] = ErrorMessages.UserNotFound;
                    return RedirectToAction(nameof(ForgotPassword));

                case ForgotPasswordResult.Error:
                    TempData[ErrorMessage] = ErrorMessages.OperationFailed;
                    return RedirectToAction(nameof(ForgotPassword));
            }
            return View(model);
        }
        #endregion

        #region ResetPassword
        [HttpGet("reset-password")]
        public IActionResult ResetPassword()
        {
            string mobile = TempData["Mobile"]?.ToString();
            if (string.IsNullOrEmpty(mobile))
            {
                return NotFound();
            }
            return View(new ResetPasswordViewModel()
            {
                Mobile = mobile
            });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            #region Validations

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            ResetPasswordResult result = await _accountService.ResetPasswordAsync(model);

            switch (result)
            {
                case ResetPasswordResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.ResetPasswordSuccessfullyDone;
                    return RedirectToAction(nameof(Login));
                case ResetPasswordResult.UserNotFound:
                    TempData[ErrorMessage] = ErrorMessages.UserNotFound;
                    return RedirectToAction(nameof(ForgotPassword));
                case ResetPasswordResult.CodeIsNotCorrect:
                    TempData[ErrorMessage] = ErrorMessages.CodeIsNotCorrect;
                    break;
            }
            return View();
        }

        #endregion

        #endregion
    }
}