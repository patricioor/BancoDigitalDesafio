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

    public TransactionController(ITransactionRepository transactionRepository, ITransactionAuthorizationIntegration transactionAuthorization)
    {
        _transactionRepository = transactionRepository;
        _transactionAuthorization = transactionAuthorization;
    }

    [HttpGet]
    public async Task<ActionResult> Transaction(TransactionDto transactionDto)
    {
        var transactionAuth = await _transactionAuthorization.GetTransactionAuthorization();
        var transaction = await _transactionRepository.CreateTransaction(transactionDto, transactionAuth)
                            ?? throw new NullReferenceException("Transaction fail");
        return Ok(transaction);
    }
}