using BancoDigitalDesafio.Services.Refit;
using BancoDigitalDesafio.Services.Response;

namespace BancoDigitalDesafio.Services.Interfaces;

public class TransactionAuthorizationIntegration : ITransactionAuthorizationIntegration
{
    private readonly ITransactionAuthorizationRefit _transactionAuthorization;

    public TransactionAuthorizationIntegration(ITransactionAuthorizationRefit transactionAuthorization)
        => _transactionAuthorization = transactionAuthorization;


    public async Task<TransactionAuthorization> GetTransactionAuthorization()
    {
        var responseData = await _transactionAuthorization.TransactionAuto();
        if (responseData != null && responseData.IsSuccessStatusCode)
            return responseData.Content;
        return null;
    }


}

