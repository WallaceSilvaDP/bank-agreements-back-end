using BankAgreements.Infrastructure.Entities.Institutions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankAgreements.Infrastructure.Data.Configurations;

public class InstitutionConfiguration : IEntityTypeConfiguration<Institution>
{
    public void Configure(EntityTypeBuilder<Institution> builder)
    {
        builder.ToTable("institutions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Cnpj)
            .HasMaxLength(18)
            .IsRequired();

        builder.Property(x => x.MaxInstallments)
            .HasDefaultValue(12)
            .IsRequired();

        builder.Property(x => x.AnnualInterestRate)
            .HasColumnType("numeric(5,4)")
            .HasDefaultValue(0.10m)
            .IsRequired();
    }
}
