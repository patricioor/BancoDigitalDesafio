using BancoDigitalDesafio.Domain.Transaction;
using BancoDigitalDesafio.Domain.user;
using BancoDigitalDesafio.Mappings;
using Microsoft.EntityFrameworkCore;

namespace BancoDigitalDesafio.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> context): base(context){}

    public DbSet<User> Users { get; set; }
    public DbSet<TransactionOp> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new TransactionMap());
        
        modelBuilder.Entity<TransactionOp>()
            .HasOne(t => t.Sender)
            .WithMany(u => u.SentTransactions)
            .HasForeignKey(t => t.SenderId);

        modelBuilder.Entity<TransactionOp>()
            .HasOne(t => t.Receiver)
            .WithMany(u => u.ReceivedTransactions)
            .HasForeignKey(t => t.ReceiverId);
    }
}