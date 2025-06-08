using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Context;
using System.Diagnostics;


public class RequestLoggingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger _logger;

	public RequestLoggingMiddleware(RequestDelegate next, ILogger logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		var startTime = Stopwatch.GetTimestamp();
		var requestId = context.TraceIdentifier;

		// Extract user information
		var userId = context.User?.Claims?
			.FirstOrDefault(c => c.Type == "sub" || c.Type == "UserId")?
			.Value ?? "Anonymous";

		var username = context.User?.Identity?.IsAuthenticated == true
			? context.User.Identity.Name
			: "Anonymous";

		var role = context.User?.Claims?
			.FirstOrDefault(c => c.Type == "role")?
			.Value ?? "None";

		// Push properties to log context
		using (LogContext.PushProperty("RequestId", requestId))
		using (LogContext.PushProperty("UserId", userId))
		using (LogContext.PushProperty("Username", username))
		using (LogContext.PushProperty("Role", role))
		{
			try
			{
				// Log request start
				_logger.Information(
					"Starting request {Method} {Path} from {RemoteIpAddress}",
					context.Request.Method,
					context.Request.Path,
					context.Connection.RemoteIpAddress);

				await _next(context);

				// Calculate elapsed time
				var elapsedMs = GetElapsedMilliseconds(startTime, Stopwatch.GetTimestamp());

				// Log successful completion
				_logger.Information(
					"Completed request {Method} {Path} with {StatusCode} in {ElapsedMs}ms",
					context.Request.Method,
					context.Request.Path,
					context.Response.StatusCode,
					elapsedMs);
			}
			catch (Exception ex)
			{
				var elapsedMs = GetElapsedMilliseconds(startTime, Stopwatch.GetTimestamp());

				// Log the error
				_logger.Error(ex,
					"Request {Method} {Path} failed after {ElapsedMs}ms with error: {ErrorMessage}",
					context.Request.Method,
					context.Request.Path,
					elapsedMs,
					ex.Message);

				throw; // Re-throw to let the error handling middleware handle it
			}
		}
	}

	private static double GetElapsedMilliseconds(long start, long stop)
	{
		return (stop - start) * 1000 / (double)Stopwatch.Frequency;
	}
}