using BancoDigitalDesafio.Data;
using BancoDigitalDesafio.Mappings;
using BancoDigitalDesafio.Repositories;
using BancoDigitalDesafio.Services.Interfaces;
using BancoDigitalDesafio.Services.Refit;
using Microsoft.EntityFrameworkCore;
using Refit;

namespace BancoDigitalDesafio.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection service)
    {
        service.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlite());
        
        //AutoMapper
        service.AddAutoMapper(typeof(UserMappingProfile));
        service.AddAutoMapper(typeof(TransactionMappingProfile));
        
        //Dependency Injection
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<ITransactionRepository, TransactionRepository>();
        service.AddScoped<INotificationRepository,NotificationRepository>();
        
        //Transaction Authorize Service
        service.AddScoped<ITransactionAuthorizationIntegration, 
                          TransactionAuthorizationIntegration>();
        
        service.AddRefitClient<ITransactionAuthorizationRefit>()
            .ConfigureHttpClient(
                x =>
                {
                    x.BaseAddress = new Uri("https://run.mocky.io");
                });
        
        //Notification Authorize Service
        service.AddScoped<INotificationServiceIntegration, 
                          NotificationServiceIntegration>();
        
        service.AddRefitClient<INotificationServiceRefit>()
            .ConfigureHttpClient(
                x =>
                {
                    x.BaseAddress = new Uri("https://run.mocky.io");
                });
    }

    public static void UseInfrastructure(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices
                                    .GetRequiredService<IServiceScopeFactory>()
                                    .CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
        context.Database.Migrate();
        context.Database.EnsureCreated();
    }

}