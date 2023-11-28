using BancoDigitalDesafio.Domain.Transaction;

namespace BancoDigitalDesafio.Repositories;

public interface ITransactionRepository
{
    public TransactionOp FindTransactionOpWithId(int id);
}