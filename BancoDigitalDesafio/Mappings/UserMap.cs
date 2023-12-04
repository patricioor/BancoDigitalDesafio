using BancoDigitalDesafio.Domain.user;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BancoDigitalDesafio.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasColumnName("FirstName")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasColumnName("LastName")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(60);

        builder.Property(x => x.Balance)
            .HasColumnName("Balance")
            .HasColumnType("INT")
            .HasDefaultValue(0);

        builder.Property(x => x.Document)
            .IsRequired()
            .HasColumnName("Document")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(14);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);

        builder.Property(x => x.Password)
            .IsRequired()
            .HasColumnName("Password")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(256);

        builder.Property(x => x.UserType)
            .HasConversion
            (
                p => p.ToString(),
                p => (UserType)Enum.Parse(typeof(UserType), p)
            );
    }
}