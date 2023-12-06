using BancoDigitalDesafio.Domain.notification;
using BancoDigitalDesafio.Domain.user;
using BancoDigitalDesafio.Services.Response;

namespace BancoDigitalDesafio.Repositories;

public class NotificationRepository : INotificationRepository
{
    public Notification SendNotification(User user, string message, NotificationService notificationService)
    {
        var notification = new Notification();
        notification.Email = user.Email;
        notification.Message = message;
        NotificationStatus(notificationService);
        return notification;
    }
    
    private void NotificationStatus(NotificationService notificationService)
    {
        var notificationResponse = notificationService;
        if (!(bool) notificationResponse.Message)
        {
            Console.WriteLine("Erro ao enviar notificação");
            throw new Exception("Serviço de notificação está fora do ar");
        }
    }
}