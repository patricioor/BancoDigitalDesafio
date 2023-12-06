using BancoDigitalDesafio.DTO;
using BancoDigitalDesafio.Repositories;
using BancoDigitalDesafio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BancoDigitalDesafio.Controllers;

[Route("api/[controller]")]
public class TransactionController : Controller
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ITransactionAuthorizationIntegration _transactionAuthorization;
    private readonly INotificationServiceIntegration _notificationService;

    public TransactionController(ITransactionRepository transactionRepository, 
        ITransactionAuthorizationIntegration transactionAuthorization, 
        INotificationServiceIntegration notificationService)
    {
        _transactionRepository = transactionRepository;
        _transactionAuthorization = transactionAuthorization;
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<ActionResult> Transaction(TransactionDto transactionDto)
    {
        var notificationServ = await _notificationService.NotificationIntegration();
        var transactionAuth = await _transactionAuthorization.GetTransactionAuthorization();
        var transaction = await _transactionRepository.CreateTransaction(transactionDto, transactionAuth, notificationServ)
                            ?? throw new NullReferenceException("Transaction fail");
        return Ok(transaction);
    }
}