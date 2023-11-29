using BancoDigitalDesafio.Domain.user;
using BancoDigitalDesafio.Services.Interfaces;

namespace BancoDigitalDesafio.Repositories;

public class NotificationRepository
{
    private readonly INotificationSenderIntegration _notificationSender;

    public NotificationRepository(INotificationSenderIntegration notificationSender)
        => _notificationSender = notificationSender;

    public void SendNotification(User user, string message)
    {
        var email = user.Email;
        var notificationResponse = _notificationSender.NotificationIntegration(email, message).ToString();

        if (!notificationResponse.Equals("false"))
        {
            Console.WriteLine("Erro ao enviar notificação");
            throw new Exception("Serviço de notificação está fora do ar");
        }
    }
}