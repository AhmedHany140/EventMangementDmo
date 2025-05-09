using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Linq;
using System.Threading.Tasks;

public class RequestLoggingMiddleware
{
	private readonly RequestDelegate _next;

	public RequestLoggingMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		var requestId = context.TraceIdentifier;

		
		var userId = context.User?.Claims?.FirstOrDefault(c => c.Type == "sub" || c.Type == "UserId")?.Value ?? "Anonymous";
		var username = context.User?.Identity?.IsAuthenticated == true
			? context.User.Identity.Name
			: "Anonymous";

		var role = context.User?.Claims?.FirstOrDefault(c => c.Type == "role")?.Value ?? "None";

		using (LogContext.PushProperty("RequestId", requestId))
		using (LogContext.PushProperty("UserId", userId))
		using (LogContext.PushProperty("Username", username))
        using(LogContext.PushProperty("Role",role))
		{
			await _next(context);
		}
	}
}
