using BancoDigitalDesafio.Services.Response;
using Refit;

namespace BancoDigitalDesafio.Services.Refit;

public interface ITransactionAuthorizationRefit
{
    [Get("/v3/5794d450-d2e2-4412-8131-73d0293ac1cc")]
    Task<ApiResponse<TransactionAuthorization>> TransactionAuto();
}