using AutoMapper;
using BancoDigitalDesafio.Data;
using BancoDigitalDesafio.Domain.Transaction;
using BancoDigitalDesafio.Domain.user;
using BancoDigitalDesafio.DTO;
using BancoDigitalDesafio.Services.Interfaces;

namespace BancoDigitalDesafio.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;
    private readonly ITransactionAuthorizationIntegration _transactionAuthorization;
    private readonly INotificationRepository _notificationRepository;
    

    public TransactionRepository(AppDbContext context, ITransactionAuthorizationIntegration transactionAuthorization, INotificationRepository notificationRepository)
    {
        _context = context;
        _transactionAuthorization = transactionAuthorization;
        _notificationRepository = notificationRepository;
    }
    
    public async Task<TransactionOp> CreateTransaction(TransactionDto transactionDto)
    {
        var sender = _context.Users.Find(transactionDto.SenderId) ?? throw new NullReferenceException("Sender not found");
        var receiver = _context.Users.Find(transactionDto.ReceiverId) ?? throw new NullReferenceException("Receiver not found");;
        ValidateTransaction(sender, transactionDto.Amount);
        
        var responseMock = await _transactionAuthorization.GetTransactionAuthorization();
        if (!responseMock.Equals("Autorizado"))
            throw new Exception("Transação não autorizada");

        var newTransaction = new TransactionOp();
        newTransaction.Amount = transactionDto.Amount;
        newTransaction.Sender = sender;
        newTransaction.Receiver = receiver;
        newTransaction.Timestamp = DateTime.Now;

        sender.Balance =- transactionDto.Amount;
        receiver.Balance =+ transactionDto.Amount;
        
        sender.Transactions.Add(newTransaction);
        receiver.Transactions.Add(newTransaction);

        _context.Transactions.Add(newTransaction);
        _context.Users.Update(sender);
        _context.Users.Update(receiver);
        _context.SaveChanges();

        _notificationRepository.SendNotification(sender, "mandou fon");
        _notificationRepository.SendNotification(receiver,"recebeu fonfon");

        return newTransaction;
    }

    // public async Task<string> AuthorizeTransaction()
    // {
    //     var result = await _transactionAuthorization.GetTransactionAuthorization() ??
    //                  throw new NullReferenceException("Erro na requisição");
    //     return result.message;
    // }

    public TransactionOp FindTransactionOpWithId(int id)
    {
        return null;
    }
    
    public void ValidateTransaction(User sender, int amount)
    {
        if (sender.UserType == UserType.MERCHANT)
            throw new Exception("Usuário do tipo lojista não está autorizado a realizar transação");

        if (sender.Balance.CompareTo(amount) < 0)
            throw new Exception("Usuário não tem saldo suficiente para executar a ação");
    }
}