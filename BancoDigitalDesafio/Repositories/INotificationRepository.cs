using BancoDigitalDesafio.Domain.user;

namespace BancoDigitalDesafio.Repositories;

public interface INotificationRepository
{
    public void SendNotification(User user, string message);
}