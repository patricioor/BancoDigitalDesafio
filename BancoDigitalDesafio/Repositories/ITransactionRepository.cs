using BancoDigitalDesafio.Domain.user;
using BancoDigitalDesafio.DTO;
using BancoDigitalDesafio.Services.Response;

namespace BancoDigitalDesafio.Repositories;

public interface ITransactionRepository
{
    public void ValidateTransaction(User sender, int amount);
    public Task<TransactionDto> CreateTransaction
        (TransactionDto transactionDto, 
        TransactionAuthorization transactionAuthorization,
        NotificationService notificationService);
}