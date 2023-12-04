using BancoDigitalDesafio.Domain.Transaction;
using BancoDigitalDesafio.Domain.user;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BancoDigitalDesafio.Mappings;

public class TransactionMap : IEntityTypeConfiguration<TransactionOp>
{
    public void Configure(EntityTypeBuilder<TransactionOp> builder)
    {
        builder.ToTable("Transactions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnName("Amount")
            .HasColumnType("INT")
            .HasDefaultValue(0.01);

        builder.Property(x => x.Timestamp)
            .HasColumnName("Timestamp")
            .HasColumnType("DATETIME")
            .HasDefaultValue(DateTime.Now);
    }
}