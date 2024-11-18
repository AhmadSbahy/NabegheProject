using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Models.User;

namespace Nabeghe.Application.Extensions
{
    public static class UserExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            string? userId = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrWhiteSpace(userId))
                return int.Parse(userId);
            else
                return default;

        }

        public static string GetUserFullName(this User? user)
        {
            return $"{user?.FirstName} {user?.LastName}";
        }
    }
}
