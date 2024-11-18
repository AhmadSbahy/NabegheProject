using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.User;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Infra.Data.Repositories
{
	public class UserRoleRepository(NabegheContext _context) : IUserRoleRepository
	{
		public async Task InsertAsync(UserRole userRole)
			=> await _context.AddAsync(userRole);

		public async Task SaveAsync()
			=> await _context.SaveChangesAsync();

		public void Update(UserRole userRole)
			=> _context.Update(userRole);

		public async Task<List<int>> GetRoleIdsAsync(int userId)
			=> await _context.UserRoles
				.Where(ur => ur.UserId == userId)
				.Select(ur => ur.RoleId).ToListAsync();

		public async Task<List<int>> GetUserRoleIdsAsync(int userId)
			=> await _context.UserRoles
				.Where(ur => ur.UserId == userId)
				.Select(ur => ur.Id).ToListAsync();

		public void Remove(UserRole userRole)
			=> _context.UserRoles.Remove(userRole);
	}
}
