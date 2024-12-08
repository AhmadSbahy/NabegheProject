using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kavenegar.Models;
using Nabeghe.Application.Extensions;
using Nabeghe.Application.Generators;
using Nabeghe.Application.Security;
using Nabeghe.Application.Senders.Interfaces;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Enums.User;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.User;
using Nabeghe.Domain.ViewModels.Account;

namespace Nabeghe.Application.Services.Implementation
{
	public class AccountService
	(IUserRepository _userRepository, ISmsSender _smsSender)
	: IAccountService
	{
		public async Task<RegisterResult> RegisterAsync(RegisterViewModel model)
		{
			if (await _userRepository.IsExitsMobileAsync(model.Mobile))
				return RegisterResult.MobileDuplicated;

			string hashPassword = PasswordHasher.EncodePasswordMd5(model.Password);

			string randomCode = CodeGenerator.GenerateCode();

			User user = new User()
			{
				CreateDate = DateTime.Now,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Mobile = model.Mobile,
				Password = hashPassword,
				Email = null,
				Status = UserStatus.NotActive,
				ConfirmCode = randomCode,
				Avatar = "NoProfilePicture.png",
			};

			await _userRepository.InsertAsync(user);
			await _userRepository.SaveAsync();

			var result = _smsSender.SendSms(user.Mobile, $" سلام خانم/اقای {user.GetUserFullName()} " +
														 $" از ثبت نام شما در سایت نابغه ممنونیم " +
														 $" کد تائید شما {user.ConfirmCode} ");
		
			if (result.Status == 200 || result.Status == 1)
			{
				return RegisterResult.Success;
			}
			else
			{
				return RegisterResult.Error;
			}
		}

		public async Task<LoginResult> LoginAsync(LoginViewModel model)
		{
			string hashPassword = PasswordHasher.EncodePasswordMd5(model.Password);

			User? user = await _userRepository.GetByMobileAndPasswordAsync(model.Mobile, hashPassword);

			if (user == null)
				return LoginResult.UserNotFound;

			if (user.Status == UserStatus.Ban)
				return LoginResult.UserIsBan;

			if (user.Status == 0)
				return LoginResult.UserIsNotActive;

			return LoginResult.Success;
		}

		public async Task<ForgotPasswordResult> ForgotPasswordAsync(ForgotPasswordViewModel model)
		{
			User? user = await _userRepository.GetByMobileAsync(model.Mobile);

			if (user == null) return ForgotPasswordResult.UserNotFound;

			string randomCode = CodeGenerator.GenerateCode();


			var result = _smsSender.SendSms(user.Mobile, $" سلام خانم/اقای {user.GetUserFullName()} " +
														 $" این کد شما هست جهت تغییر کلمه عبور " +
														 $" کد تایید شما : {randomCode} ");

			if (result.Status == 200 || result.Status == 1)
			{
				#region Update User

				user.ConfirmCode = randomCode;
				_userRepository.Update(user);
				await _userRepository.SaveAsync();

				#endregion
				return ForgotPasswordResult.success;
			}
			else
			{
				return ForgotPasswordResult.Error;
			}

		}

		public async Task<ResetPasswordResult> ResetPasswordAsync(ResetPasswordViewModel model)
		{
			User? user = await _userRepository.GetByMobileAsync(model.Mobile);

			if (user == null) return ResetPasswordResult.UserNotFound;

			if (user.ConfirmCode != model.ConfirmCode) return ResetPasswordResult.CodeIsNotCorrect;

			string hashPassword = PasswordHasher.EncodePasswordMd5(model.Password);

			user.Password = hashPassword;
			user.ConfirmCode = null;

			#region Update User

			_userRepository.Update(user);
			await _userRepository.SaveAsync();

			#endregion

			return ResetPasswordResult.Success;
		}

		public async Task<ActivateUserResult> ActivateUserAsync(ActivateUserViewModel model)
		{
			User? user = await _userRepository.GetByMobileAsync(model.Mobile);

			if (user == null) return ActivateUserResult.UserNotFound;
			
			if (user.ConfirmCode != model.ConfirmCode) return ActivateUserResult.CodeIsNotCorrect;

			user.ConfirmCode = null;
			user.Status = UserStatus.Active;

			#region Update User

			_userRepository.Update(user);
			await _userRepository.SaveAsync();

			#endregion

			return ActivateUserResult.Success;
		}
	}
}
