using BancoDigitalDesafio.Services.Response;

namespace BancoDigitalDesafio.Services.Interfaces;

public interface ITransactionAuthorizationIntegration
{
    Task<TransactionAuthorization> GetTransactionAuthorization();
}