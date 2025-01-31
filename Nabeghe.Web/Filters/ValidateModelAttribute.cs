using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Nabeghe.Web.Filters
{
	public class ValidateModelAttribute : Attribute, IActionFilter
	{
		public void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				var ctrl = (Controller)context.Controller;

				var view = new ViewResult()
				{
					ViewName = context.RouteData.Values["Action"]?.ToString(),
					ViewData = ctrl.ViewData
				};

				context.Result = view;
			}
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			
		}
	}
}
