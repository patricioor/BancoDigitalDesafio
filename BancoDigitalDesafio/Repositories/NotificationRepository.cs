using BancoDigitalDesafio.Domain.notification;
using BancoDigitalDesafio.Domain.user;
using BancoDigitalDesafio.Services.Response;

namespace BancoDigitalDesafio.Repositories;

public class NotificationRepository : INotificationRepository
{
    public Notification SendNotification(User user, string message, NotificationService notificationService)
    {
        var notification = new Notification
        {
            Email = user.Email,
            Message = message
        };
        NotificationStatus(notificationService);
        Console.WriteLine($"Transaction confirmation was sent to email '{notification.Email}'");
        return notification;
    }
    
    private void NotificationStatus(NotificationService notificationService)
    {
        var notificationResponse = notificationService;
        if (!(bool) notificationResponse.Message)
        {
            Console.WriteLine("Error sending notification");
            throw new Exception("Notification service is down");
        }
    }
}