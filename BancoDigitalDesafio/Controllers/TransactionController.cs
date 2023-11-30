using BancoDigitalDesafio.DTO;
using BancoDigitalDesafio.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BancoDigitalDesafio.Controllers;

[Route("api/[controller]")]
public class TransactionController : Controller
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionController(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    [HttpPost]
    public IActionResult Transaction(TransactionDto transactionDto)
    {
        var transaction = _transactionRepository.CreateTransaction(transactionDto)
                            ?? throw new NullReferenceException("Transaction fail");
        return Ok(transaction);

    }
}