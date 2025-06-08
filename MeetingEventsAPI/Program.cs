using Application.Mapper;
using Infrastructure.Provider;
using Infrastructure.Reposatory;
using Infrastructure.ServiceContainer;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure enhanced logging with environment awareness
ServiceContainer.AddConfigurationLog(
	filename: Assembly.GetExecutingAssembly().GetName().Name, // Use assembly name as log filename
	environmentName: builder.Environment.EnvironmentName
);

// Enable Serilog as the logging provider
builder.Host.UseSerilog();

// Configure controllers with improved null handling
builder.Services.AddControllers(options =>
{
	options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
	options.Filters.Clear();
});

// Add API documentation services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register infrastructure services
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP pipeline
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseDeveloperExceptionPage(); // Detailed error pages for development
}

// Security and performance middleware
app.UseHttpsRedirection();

// Add enhanced request logging middleware
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

try
{
	Log.Information("Starting application host");
	app.Run();
}
catch (Exception ex)
{
	Log.Fatal(ex, "Application host terminated unexpectedly");
}
finally
{
	Log.CloseAndFlush(); // Ensure logs are properly flushed
}