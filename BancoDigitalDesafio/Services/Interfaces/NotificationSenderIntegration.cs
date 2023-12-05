using BancoDigitalDesafio.DTO;
using BancoDigitalDesafio.Services.Refit;
using BancoDigitalDesafio.Services.Response;

namespace BancoDigitalDesafio.Services.Interfaces;

public class NotificationSenderIntegration : INotificationSenderIntegration
{
    private readonly INotificationSenderRefit _notificationSender;

    public NotificationSenderIntegration(INotificationSenderRefit notificationSender)
        => _notificationSender = notificationSender;
    
    public async Task<> NotificationIntegration(string email, string message)
    {
        var responseData = await _notificationSender.Notification(email, message);
        if (responseData != null && responseData.IsSuccessStatusCode)
            return responseData.Content;
        return null;
    }
}