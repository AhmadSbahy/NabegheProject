using Nabeghe.Application.Security;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Application.Statics;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.ContactUs;
using Nabeghe.Domain.Models.User;
using Nabeghe.Domain.ViewModels.ContactUs;
using Nabeghe.Domain.ViewModels.User;

namespace Nabeghe.Application.Services.Implementation
{
	public class UserService(IUserRepository _userRepository
        , IRoleRepository _roleRepository
        , IUserRoleRepository _userRoleRepository) : IUserService
    {
        public async Task<User?> GetByMobileAsync(string mobile)
            => await _userRepository.GetByMobileAsync(mobile);

        public async Task<EditUserProfileViewModel?> GetProfileForEditAsync(int userId)
        {
            User? user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return null;

            return new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.Mobile,
                currentPassword = user.Password,
                Avatar = user.Avatar
            };
        }

        public async Task<EditUserProfileResult> UpdateProfileAsync(EditUserProfileViewModel model)
        {
            if (!model.UserId.HasValue)
                return EditUserProfileResult.UserNotFound;

            User? user = await _userRepository.GetByIdAsync(model.UserId.Value);
            if (user == null)
                return EditUserProfileResult.UserNotFound;

            #region Update User

            // به‌روزرسانی فقط در صورتی که مقدار داشته باشد
            if (!string.IsNullOrWhiteSpace(model.FirstName))
            {
                user.FirstName = model.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(model.LastName))
            {
                user.LastName = model.LastName;
            }

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                user.Email = model.Email;
            }

            if (!string.IsNullOrWhiteSpace(model.Mobile))
            {
                user.Mobile = model.Mobile;
            }

            // اگر password خالی نبود، آن را به‌روزرسانی کن
            if (!string.IsNullOrWhiteSpace(model.currentPassword) && !string.IsNullOrWhiteSpace(model.Password))
            {
                if (user.Password != PasswordHasher.EncodePasswordMd5(model.currentPassword))
                    return EditUserProfileResult.CurrentPasswordNotCorrect;


                user.Password = PasswordHasher.EncodePasswordMd5(model.Password);
            }

            #region Avatar

            if (model.NewAvatar != null)
            {
                if (user.Avatar != null)
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), PathTools.UserAvatarPath, user.Avatar);

                    if (System.IO.File.Exists(deletePath))
                    {
                        System.IO.File.Delete(deletePath);
                    }
                }
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.NewAvatar.FileName);
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), PathTools.UserAvatarPath, imageName);
                using (var stream = System.IO.File.Create(savePath))
                {
                    model.NewAvatar.CopyTo(stream);
                }

                user.Avatar = imageName;
            }

            #endregion

            _userRepository.Update(user);
            await _userRepository.SaveAsync();

            #endregion

            return EditUserProfileResult.Success;

        }

        public async Task<AdminSideCreateUserResult> AdminSideCreateAsync(AdminSideCreateUserViewModel model)
        {
            #region Validations

            if (await _userRepository.IsExitsMobileAsync(model.Mobile))
                return AdminSideCreateUserResult.MobileDuplicated;

            if (model.RoleIds.Count < 1)
                return AdminSideCreateUserResult.NotSelectedRole;

            #endregion

            User user = new User
            {
                CreateDate = DateTime.Now,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Mobile = model.Mobile,
                Password = PasswordHasher.EncodePasswordMd5(model.Password),
                Email = model.Email,
                IsDeleted = false,
                Status = model.Status,
                Avatar = null,
            };


            #region Save Avatar

            if (model.Avatar != null)
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.Avatar.FileName);
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), PathTools.UserAvatarPath, imageName);
                using (var stream = File.Create(savePath))
                {
                    model.Avatar.CopyTo(stream);
                }

                user.Avatar = imageName;
            }

            #endregion

            await _userRepository.InsertAsync(user);
            await _userRepository.SaveAsync();


            #region Role

            foreach (var roleId in model.RoleIds)
                await _userRoleRepository.InsertAsync(new UserRole()
                {
                    CreateDate = DateTime.Now,
                    RoleId = roleId,
                    UserId = user.Id
                });

            await _userRepository.SaveAsync();

            #endregion

            return AdminSideCreateUserResult.Success;
        }

        public async Task<AdminSideEditUserViewModel?> AdminSideGetForEditAsync(int userId)
        {
            User? user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return null;

            List<int> roleIds = await _userRoleRepository.GetRoleIdsAsync(userId);

            return new AdminSideEditUserViewModel
            {
                Id = userId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Mobile = user.Mobile,
                Email = user.Email,
                Status = user.Status,
                RoleIds = roleIds,
                Avatar = user.Avatar,
                Password = user.Password
            };
        }

        public async Task<AdminSideEditUserResult> AdminSideUpdateAsync(AdminSideEditUserViewModel model)
        {
            User? user = await _userRepository.GetByIdAsync(model.Id);

            if (user == null)
                return AdminSideEditUserResult.UserNotFound;

            if (model.RoleIds.Count < 1)
                return AdminSideEditUserResult.NotSelectedRole;

            if (await _userRepository.DuplicatedMobileAsync(model.Mobile, user.Id))
                return AdminSideEditUserResult.MobileDuplicated;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Status = model.Status;
            user.Mobile = model.Mobile;
            user.Email = model.Email;
            if (model.Password != user.Password)
            {
                user.Password = PasswordHasher.EncodePasswordMd5(model.Password);
            }

            #region Avatar

            if (model.NewAvatar != null)
            {
                if (user.Avatar != null)
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), PathTools.UserAvatarPath, user.Avatar);

                    if (System.IO.File.Exists(deletePath))
                    {
                        System.IO.File.Delete(deletePath);
                    }
                }
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.NewAvatar.FileName);
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), PathTools.UserAvatarPath, imageName);
                using (var stream = System.IO.File.Create(savePath))
                {
                    model.NewAvatar.CopyTo(stream);
                }

                user.Avatar = imageName;
            }

            #endregion

            #region Update User

            _userRepository.Update(user);
            await _userRepository.SaveAsync();

            #endregion

            #region User Roles

            var userRoleIds = await _userRoleRepository.GetUserRoleIdsAsync(user.Id);
            foreach (var userRoleId in userRoleIds)
                _userRoleRepository.Remove(new UserRole()
                {
                    Id = userRoleId
                });

            await _userRoleRepository.SaveAsync();

            foreach (var roleId in model.RoleIds)
                await _userRoleRepository.InsertAsync(new UserRole()
                {
                    CreateDate = DateTime.Now,
                    RoleId = roleId,
                    UserId = user.Id
                });

            await _userRoleRepository.SaveAsync();
            #endregion

            return AdminSideEditUserResult.Success;
        }

        public async Task<FilterUserViewModel> FilterUserAsync(FilterUserViewModel model)
        {
            return await _userRepository.FilterUserAsync(model);
        }

        public async Task<DeleteUserResult> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return DeleteUserResult.UserNotFound;
            }
            user.IsDeleted = true;

            _userRepository.Update(user);
            await _userRepository.SaveAsync();

            return DeleteUserResult.Success;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<List<UserFavoriteCourseViewModel>> GetCourseFavoriteListAsync(int userId)
        {
            return await _userRepository.GetCourseFavoriteListAsync(userId);
        }

        public async Task<List<UserCourseCommentViewModel>> GetUserCourseCommentListAsync(int userId)
        {
            return await _userRepository.GetUserCourseCommentListAsync(userId);
        }

        public string GetUserFullName(int userId)
        {
            return _userRepository.GetUserFullName(userId);
        }
        public async Task<bool> IsUserInCourse(int userId, int courseId)
        {
	        return await _userRepository.IsUserInCourse(userId, courseId);
        }
        public bool UserHasPermission(int userId, string permission)
        {
	        var userRoles = _roleRepository.GetRolesUser(userId);
	        var roleInPermission = _roleRepository
		        .GetRolesInPermission(permission);

	        foreach (var role in userRoles)
	        {
		        if (roleInPermission.Any(r => r.Id == role.Id))
			        return true;
	        }

	        return false;
		}
	}
}
