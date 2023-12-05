using BancoDigitalDesafio.DTO;

namespace BancoDigitalDesafio.Services.Interfaces;

public interface INotificationSenderIntegration
{
    Task<NotificationDto> NotificationIntegration(string email, string message);
}