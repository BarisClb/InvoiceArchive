using FluentValidation.AspNetCore;
using Hangfire;
using InvoiceArchive.Application;
using InvoiceArchive.Application.Settings;
using InvoiceArchive.Infrastructure;
using InvoiceArchive.Persistence;
using InvoiceArchive.Persistence.Contexts;
using InvoiceArchive.WebApi;
using InvoiceArchive.WebApi.Middlewares;
using InvoiceArchive.WebApi.Settings;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining(typeof(InvoiceArchive.Infrastructure.FluentValidation.CreateInvoiceRegisterCommandRequestValidator)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog(SeriLogger.Configure);
builder.Services.PeristenceServiceRegistration(builder.Configuration);
builder.Services.InfrastructureServiceRegistration(builder.Configuration);
builder.Services.ApplicationServiceRegistration(builder.Configuration);
builder.Services.HangfireServiceRegistration(builder.Configuration);
builder.Services.RabbitMqServiceRegistration(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("v1/swagger.json", "InvoiceArchive");
});
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseAuthorization();
app.MapControllers();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<InvoiceArchiveDbContext>();
    //context.Database.Migrate();
    RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
    context.Database.EnsureCreated();
    //try { databaseCreator.CreateTables(); } catch { } // If tables already exists, it will throw an exception.
    //context.Database.EnsureCreated();
}

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireAuthorizationFilter() }
});

builder.Services.HangfireJobRegistration(builder.Configuration);

app.Run();
