using BancoDigitalDesafio.Services.Refit;
using BancoDigitalDesafio.Services.Response;

namespace BancoDigitalDesafio.Services.Interfaces;

public class NotificationServiceIntegration : INotificationServiceIntegration
{
    private readonly INotificationServiceRefit _notificationService;

    public NotificationServiceIntegration(INotificationServiceRefit notificationService)
        => _notificationService = notificationService;
    
    public async Task<NotificationService> NotificationIntegration()
    {
        var responseData = await _notificationService.Notification();
        if (responseData != null && responseData.IsSuccessStatusCode)
            return responseData.Content;
        return null;
    }
}