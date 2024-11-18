using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Web.Areas.UserPanel.Components
{
	public class UserPanelSideBarViewComponent(NabegheContext context) : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			var user = await context.Users.FirstOrDefaultAsync(p => p.Id == userId);
			return View("UserPanelSideBar",user);
		}
	}
}
