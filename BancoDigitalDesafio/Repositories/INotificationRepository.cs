using BancoDigitalDesafio.Domain.notification;
using BancoDigitalDesafio.Domain.user;
using BancoDigitalDesafio.Services.Response;

namespace BancoDigitalDesafio.Repositories;

public interface INotificationRepository
{
    public Notification SendNotification
        (
        User user, 
        string message, 
        NotificationService notificationService
        );
}