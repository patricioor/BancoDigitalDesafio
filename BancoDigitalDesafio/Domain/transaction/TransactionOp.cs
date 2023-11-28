using BancoDigitalDesafio.Domain.user;

namespace BancoDigitalDesafio.Domain.Transaction;

public class TransactionOp
{
    public int Id { get; set; }
    public int Amount { get; set; }

    public int SenderId { get; set; }
    public User Sender { get; set; }

    public int ReceiverId { get; set; }
    public User Receiver { get; set; }
    public DateTime Timestamp { get; set; }
}