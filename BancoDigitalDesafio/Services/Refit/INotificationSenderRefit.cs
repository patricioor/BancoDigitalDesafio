using BancoDigitalDesafio.Services.Response;
using BancoDigitalDesafio.Domain.user;
using Refit;

namespace BancoDigitalDesafio.Services.Refit;

public interface INotificationSenderRefit
{
    [Get("/v3/54dc2cf1-3add-45b5-b5a9-6bf7e7f1f4a6")]
    Task<ApiResponse<NotificationSender>> Notification(string email, string message);
}