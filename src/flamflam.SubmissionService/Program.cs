using flamflam.SubmissionService.Config;
using flamflam.SubmissionService.Core.Database;
using flamflam.SubmissionService.Core.Errors;
using flamflam.SubmissionService.Core.Telemetry;
using flamflam.SubmissionService.Services;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace flamflam.SubmissionService;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<FlamFlamDbOptions>(
            builder.Configuration.GetSection(FlamFlamDbOptions.Section));

        builder.Services
            .AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
        });

        builder.Services.AddSwaggerGenNewtonsoftSupport();

        builder.Services.AddScoped(typeof(ITelemetryReporter<>), typeof(TelemetryReporter<>));
        
        builder.Services.AddScoped<IMongoDbProvider, MongoDbProvider>();
        builder.Services.AddScoped<MongoDbExceptionHandler>();

        builder.Services.AddScoped<ISubmissionRepository, SubmissionRepository>();

        builder.Services.AddScoped<DefaultExceptionHandler>();
        builder.Services.AddScoped<IEnumerable<IAppExceptionHandler>>(sp =>
        {
            return new List<IAppExceptionHandler>
            {
                sp.GetService<MongoDbExceptionHandler>(),
                sp.GetService<DefaultExceptionHandler>()
            };
        });
        builder.Services.AddScoped<ExceptionHandlerFacade>();

        builder.Services.AddHealthChecks()
            .AddCheck<MongoDbConnectionHealthCheck>(MongoDbConnectionHealthCheck.Name);

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseExceptionHandler(handler =>
        {
            handler.Run(async context =>
            {
                var ex = context.Features.Get<IExceptionHandlerFeature>();
                if (ex == null) return;

                using var scope = handler.ApplicationServices.CreateScope();

                var exceptionHandler = scope.ServiceProvider.GetService<ExceptionHandlerFacade>();
                if (exceptionHandler == null) return;

                var mapped = exceptionHandler.HandleException(ex.Error);

                context.Response.StatusCode = mapped.Status;
                await context.Response.WriteAsJsonAsync(mapped);
            });
        });

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}