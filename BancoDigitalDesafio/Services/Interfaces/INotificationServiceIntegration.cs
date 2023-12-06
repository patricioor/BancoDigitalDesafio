using BancoDigitalDesafio.Services.Response;

namespace BancoDigitalDesafio.Services.Interfaces;

public interface INotificationServiceIntegration
{
    Task<NotificationService> NotificationIntegration();
}