using Application.Mapper;
using Infrastructure.Provider;
using Infrastructure.Reposatory;
using Infrastructure.ServiceContainer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
// Logging configuration
ServiceContainer.AddConfigurationLog("Logs/log");
builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddControllers(options =>
{
	options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
	options.Filters.Clear(); 
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
