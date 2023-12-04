using AutoMapper;
using BancoDigitalDesafio.Data;
using BancoDigitalDesafio.Domain.Transaction;
using BancoDigitalDesafio.Domain.user;
using BancoDigitalDesafio.DTO;
using BancoDigitalDesafio.Services.Interfaces;
using BancoDigitalDesafio.Services.Response;
using Microsoft.EntityFrameworkCore;

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
    
    public async Task<TransactionOp> CreateTransaction(TransactionDto transactionDto,TransactionAuthorization transactionAuthorization)
    {
        var senderFound = await _context
            .Users
            .FirstOrDefaultAsync(x => x.Id == transactionDto.SenderId) ?? throw new NullReferenceException("Sender not found");
        
        var receiverFound = await _context
            .Users
            .FirstOrDefaultAsync(x => x.Id == transactionDto.ReceiverId) ?? throw new NullReferenceException("Receiver not found");;
        
        ValidateTransaction(senderFound, transactionDto.Amount);
        
        var responseMock = transactionAuthorization;
        if (!responseMock.Message.Equals("Autorizado"))
            throw new Exception("Transação não autorizada");

        var newTransaction = new TransactionOp
        {
            Amount = transactionDto.Amount,
            Sender = senderFound,
            SenderId = senderFound.Id,
            Receiver = receiverFound,
            ReceiverId = receiverFound.Id,
            Timestamp = DateTime.Now
        };
        
        senderFound.Balance -= transactionDto.Amount;
        receiverFound.Balance += transactionDto.Amount;
        
        senderFound.TransactionsAsSender.Add(newTransaction);
        receiverFound.TransactionsAsReceiver.Add(newTransaction);
        
        await _context.Transactions.AddAsync(newTransaction);
        await _context.SaveChangesAsync();

        _notificationRepository.SendNotification(senderFound, "mandou fon");
        _notificationRepository.SendNotification(receiverFound,"recebeu fonfon");

        return newTransaction;
    }

    public async Task<string> AuthorizeTransaction()
    {
        var result = await _transactionAuthorization.GetTransactionAuthorization() ??
                     throw new NullReferenceException("Erro na requisição");
        return result.Message;
    }

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