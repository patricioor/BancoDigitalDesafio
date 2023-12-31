using BancoDigitalDesafio.Domain.Transaction;

namespace BancoDigitalDesafio.Domain.user;

public class User
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Document { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public int Balance { get; set; } = 0;
    public UserType UserType { get; set; }
    
    public IList<TransactionOp> SentTransactions { get; set; } = new List<TransactionOp>();
    public IList<TransactionOp> ReceivedTransactions { get; set; } = new List<TransactionOp>();
}