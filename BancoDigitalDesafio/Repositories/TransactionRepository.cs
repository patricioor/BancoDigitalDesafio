using AutoMapper;
using BancoDigitalDesafio.Data;
using BancoDigitalDesafio.Domain.Transaction;
using BancoDigitalDesafio.Domain.user;
using BancoDigitalDesafio.DTO;
using BancoDigitalDesafio.Services.Response;
using Microsoft.EntityFrameworkCore;

namespace BancoDigitalDesafio.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;
    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;

    public TransactionRepository(AppDbContext context, INotificationRepository notificationRepository, IMapper mapper)
    {
        _context = context;
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<TransactionDto> CreateTransaction(TransactionDto transactionDto,
        TransactionAuthorization transactionAuthorization,
        NotificationService notification)
    {
        var transaction = await newTransaction(transactionDto, transactionAuthorization);

        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();

        _notificationRepository.SendNotification(transaction.Sender, "mandou fon", notification);
        _notificationRepository.SendNotification(transaction.Receiver, "recebeu fonfon", notification);

        
        return _mapper.Map<TransactionDto>(transaction);
    }

    private async Task<TransactionOp> newTransaction(TransactionDto transactionDto,
        TransactionAuthorization transactionAuthorization)
    {
        var senderFound = await _context
                              .Users
                              .FirstOrDefaultAsync(x => x.Id == transactionDto.SenderId)
                          ?? throw new NullReferenceException("Sender not found");

        var receiverFound = await _context
                                .Users
                                .FirstOrDefaultAsync(x => x.Id == transactionDto.ReceiverId)
                            ?? throw new NullReferenceException("Receiver not found");
        ;

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

        return newTransaction;
    }

    public void ValidateTransaction(User sender, int amount)
    {
        if (sender.UserType == UserType.MERCHANT)
            throw new Exception("Usuário do tipo lojista não está autorizado a realizar transferências");

        if (sender.Balance.CompareTo(amount) < 0)
            throw new Exception("Usuário não tem saldo suficiente para executar a ação");
    }
}
