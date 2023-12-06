using BancoDigitalDesafio.Services.Response;
using Refit;

namespace BancoDigitalDesafio.Services.Refit;

public interface INotificationServiceRefit
{
    [Get("/v3/54dc2cf1-3add-45b5-b5a9-6bf7e7f1f4a6")]
    Task<ApiResponse<NotificationService>> Notification();
}