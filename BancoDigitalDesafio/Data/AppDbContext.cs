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

        modelBuilder.Entity<User>()
            .HasMany(x => x.TransactionsAsSender)
            .WithOne(x => x.Sender)
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasMany(x => x.TransactionsAsReceiver)
            .WithOne(x => x.Receiver)
            .HasForeignKey(x => x.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}