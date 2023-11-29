using BancoDigitalDesafio.DTO;

namespace BancoDigitalDesafio.Services.Response;

public record NotificationSender(string email, string message) : NotificationDto(email, message);