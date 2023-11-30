using BancoDigitalDesafio.Domain.Transaction;
using BancoDigitalDesafio.Domain.user;
using BancoDigitalDesafio.DTO;

namespace BancoDigitalDesafio.Repositories;

public interface ITransactionRepository
{
    public TransactionOp FindTransactionOpWithId(int id);
    public void ValidateTransaction(User sender, int amount);

    public Task<TransactionOp> CreateTransaction(TransactionDto transactionDto);
}