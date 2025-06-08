using Domain;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using Serilog;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Domain.Entities;
using Infrastructure.Provider;
using Infrastructure.Reposatory;
using Application.Mapper;
using Application.DTOs;
using Serilog.Sinks.SystemConsole.Themes;

namespace Infrastructure.ServiceContainer
{
	public static class ServiceContainer
	{

		public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			AddDbContext(services, configuration);

			services.AddIdentity<AppUser, IdentityRole>()
				 .AddEntityFrameworkStores<AppDbContext>()
					 .AddDefaultTokenProviders();

			services.AddJWTValidation(configuration);

			InjectSevices(services, configuration);
		}

		private static IServiceCollection AddJWTValidation(this IServiceCollection services, IConfiguration configuration)
		{
             services.AddAuthentication(options=>
			 {
				 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			 }
			 )
              .AddJwtBearer(options =>
              {
              var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]!);
              string issuer = configuration["JWT:issuer"]!;
              string audience = configuration["JWT:audience"]!;
              
              options.SaveToken = true;
              options.RequireHttpsMetadata = false;
              options.TokenValidationParameters = new TokenValidationParameters
              {
              	ValidateIssuer = true,
              	ValidateAudience = true,
              	//ValidateLifetime = true, 
              	//ValidateIssuerSigningKey = true,
              	ValidIssuer = issuer
				?? throw new InvalidOperationException("JWT:ValidIssuer configuration is missing."),
              	ValidAudience = audience
				?? throw new InvalidOperationException("JWT:ValidAudience configuration is missing."),
              	IssuerSigningKey = new SymmetricSecurityKey(key
				?? throw new InvalidOperationException("JWT:Secret configuration is missing."))
              };
              });


			return services;
		}


		private static IServiceCollection InjectSevices(this IServiceCollection Services, IConfiguration configuration)
		{
			Services.Scan(scan => scan
	           .FromAssemblyOf<EventMapper>()
	           .AddClasses(classes => classes.Where(type =>
		        type.Name.EndsWith("Mapper") && !type.IsAbstract && !type.IsInterface))
	            .AsSelf()
	            .WithSingletonLifetime()
                  );

			Services.AddScoped(typeof(IService<>), typeof(Reposatory<>));

			Services.AddScoped<Iuser, AcountReposatory>();
			Services.AddScoped<IProvider<EventSponsorDto, EventSponsorDetails>, EventSponsorReposatory>();
			Services.AddScoped<IProvider<SessionSpeakerDto, SessionSpeakerDetails>, SessionSpeakerReposatory>();

			return Services;

		}

		private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>

			options.UseSqlServer(configuration.GetConnectionString("EventsConstr"), sqlserveroptions =>
			  sqlserveroptions.EnableRetryOnFailure())
				);


			return services;
		}


		public static void AddConfigurationLog(string filename, string environmentName)
		{
			// Set up log directory
			var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
			Directory.CreateDirectory(logDirectory); // Ensure directory exists

			// Configure logger
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug() // Base level
				.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
				.MinimumLevel.Override("System", LogEventLevel.Warning)
				.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
				.Enrich.FromLogContext()
				.Enrich.WithProperty("Environment", environmentName)
				.Enrich.WithMachineName()
				.Enrich.WithProcessId()
				.Enrich.WithThreadId()
				.WriteTo.Console(
					outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] " +
								   "(ReqId: {RequestId}, User: {Username}, " +
								   "Env: {Environment}) {Message:lj}{NewLine}{Exception}",
					theme: AnsiConsoleTheme.Code,
					restrictedToMinimumLevel: environmentName == "Development"
						? LogEventLevel.Debug
						: LogEventLevel.Information
				)
				.WriteTo.File(
					path: Path.Combine(logDirectory, $"{filename}-.log"),
					outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} " +
									"[{Level:u3}] (ReqId: {RequestId}, " +
									"User: {UserId}|{Username}, Role: {Role}, " +
									"Machine: {MachineName}, Thread: {ThreadId}) " +
									"{Message:lj}{NewLine}{Exception}",
					rollingInterval: RollingInterval.Day,
					retainedFileCountLimit: 30, // Keep logs for 30 days
					shared: true,
					restrictedToMinimumLevel: LogEventLevel.Information
				)
				.WriteTo.Debug(restrictedToMinimumLevel: LogEventLevel.Debug)
				.CreateLogger();

			Log.Information("Logger initialized in {Environment} environment", environmentName);
		}


	}



}

