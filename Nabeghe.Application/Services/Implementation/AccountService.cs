using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kavenegar.Models;
using Nabeghe.Application.Generators;
using Nabeghe.Application.Security;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Enums.User;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.User;
using Nabeghe.Domain.ViewModels.Account;

namespace Nabeghe.Application.Services.Implementation
{
	public class AccountService
	(IUserRepository _userRepository)
	: IAccountService
	{
		public async Task<RegisterResult> RegisterAsync(RegisterViewModel model)
		{
			if (await _userRepository.IsExitsMobileAsync(model.Mobile))
				return RegisterResult.MobileDuplicated;
			string hashPassword = PasswordHasher.EncodePasswordMd5(model.Password);

			User user = new()
			{
				CreateDate = DateTime.Now,
				FirstName = null,
				LastName = null,
				Mobile = model.Mobile,
				Password = hashPassword,
				Email = null,
				Status = UserStatus.Active,
				ConfirmCode = null,
                Avatar = "NoProfilePicture.png"
			};

			await _userRepository.InsertAsync(user);
			await _userRepository.SaveAsync();

			return RegisterResult.Success;
		}

        public async Task<LoginResult> LoginAsync(LoginViewModel model)
        {
            string hashPassword = PasswordHasher.EncodePasswordMd5(model.Password);

            User? user = await _userRepository.GetByMobileAndPasswordAsync(model.Mobile, hashPassword);

            if (user == null)
                return LoginResult.UserNotFound;

            if (user.Status == UserStatus.Ban)
                return LoginResult.UserIsBan;

            return LoginResult.Success;
        }

        public async Task<ForgotPasswordResult> ForgotPasswordAsync(ForgotPasswordViewModel model)
        {
            User? user = await _userRepository.GetByMobileAsync(model.Mobile);

            if (user == null) return ForgotPasswordResult.UserNotFound;

            string randomCode = CodeGenerator.GenerateCode();


            //var result = _smsSender.SendSms(user.Mobile, $"کد تایید شما : {randomCode}");
            var result = new SendResult();
            result.Status = 200;
            if (result.Status == 200)
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
            User? user = await _userRepository.GetByMobileAndConfirmCodeAsync(model.Mobile, model.ConfirmCode);

            if (user == null) return ResetPasswordResult.UserNotFound;

            string hashPassword = PasswordHasher.EncodePasswordMd5(model.Password);

            user.Password = hashPassword;
            user.ConfirmCode = null;

            #region Update User

            _userRepository.Update(user);
            _userRepository.SaveAsync();

            #endregion

            return ResetPasswordResult.Success;
        }
    }
}
