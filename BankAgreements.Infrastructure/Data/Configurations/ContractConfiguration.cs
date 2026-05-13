using BankAgreements.Infrastructure.Entities.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Paschoalotto.Infrastructure.Data.Configurations;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
	public void Configure(EntityTypeBuilder<Contract> builder)
	{
		builder.ToTable("contracts");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.ContractNumber)
			.HasMaxLength(50)
			.IsRequired();
	}
}