using BancoDigitalDesafio.Services.Response;

namespace BancoDigitalDesafio.Services.Interfaces;

public interface INotificationSenderIntegration
{
    Task<NotificationSender> NotificationIntegration(string email, string message);
}