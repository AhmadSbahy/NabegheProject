﻿using Nabeghe.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Application.Services.Interfaces
{
	public interface IAccountService
	{
		Task<RegisterResult> RegisterAsync(RegisterViewModel model);
        Task<LoginResult> LoginAsync(LoginViewModel model);
        Task<ForgotPasswordResult> ForgotPasswordAsync(ForgotPasswordViewModel model);
        Task<ResetPasswordResult> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<ActivateUserResult> ActivateUserAsync(ActivateUserViewModel model);
	}
}
