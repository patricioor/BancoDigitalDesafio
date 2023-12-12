using AutoMapper;
using BancoDigitalDesafio.Data;
using BancoDigitalDesafio.Data.CustomException;
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
        var sender = await _context
                              .Users
                              .FirstOrDefaultAsync(x => x.Id == transactionDto.SenderId)
                          ?? throw new HttpException(StatusCodes.Status400BadRequest,"Sender not found");

        var receiver = await _context
                                .Users
                                .FirstOrDefaultAsync(x => x.Id == transactionDto.ReceiverId)
                            ?? throw new HttpException(StatusCodes.Status400BadRequest,"Receiver not found");
        
        var transaction = await newTransaction(transactionDto, transactionAuthorization, sender, receiver);
        
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();

        _notificationRepository.SendNotification(sender, "mandou fon", notification);
        _notificationRepository.SendNotification(receiver, "recebeu fonfon", notification);
        
        return _mapper.Map<TransactionDto>(transaction);
    }

    private async Task<TransactionOp> newTransaction(TransactionDto transactionDto,
        TransactionAuthorization transactionAuthorization,
        User sender,
        User receiver)
    {
        ValidateTransaction(sender, transactionDto.Amount);

        var responseMock = transactionAuthorization;
        if (!responseMock.Message.Equals("Autorizado"))
            throw new Exception("Transação não autorizada");

        var newTransaction = new TransactionOp
        {
            Amount = transactionDto.Amount,
            SenderId = sender.Id,
            ReceiverId = receiver.Id,
            Timestamp = DateTime.Now
        };

        sender.Balance -= transactionDto.Amount;
        receiver.Balance += transactionDto.Amount;

        sender.Transactions.Add(newTransaction);
        receiver.Transactions.Add(newTransaction);

        return newTransaction;
    }

    public void ValidateTransaction(User sender, int amount)
    {
        if (sender.UserType == UserType.MERCHANT)
            throw new Exception("Merchant type user is not authorized to make transfers");

        if (sender.Balance.CompareTo(amount) < 0)
            throw new Exception("User does not have enough balance to perform the action");
    }
}
