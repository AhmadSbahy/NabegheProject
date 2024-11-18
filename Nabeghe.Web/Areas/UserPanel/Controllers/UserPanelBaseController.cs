using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Nabeghe.Web.Areas.UserPanel.Controllers
{
	[Authorize]
	[Area("UserPanel")]
	public class UserPanelBaseController : Controller
	{
		protected string SuccessMessage = "SuccessMessage";
		protected string ErrorMessage = "ErrorMessage";
		protected string WarningMessage = "WarningMessage";
		protected string InfoMessage = "InfoMessage";
	}
}
