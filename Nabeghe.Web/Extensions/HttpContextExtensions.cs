namespace Nabeghe.Web.Extensions
{
	public static class HttpContextExtensions
	{
		public static string GetUserIp(this HttpContext context)
		{
			return context.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
		}
	}
}
