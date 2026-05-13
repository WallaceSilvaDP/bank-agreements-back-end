using BankAgreements.Infrastructure.Entities.Agreements;
using BankAgreements.Infrastructure.Entities.Installments;
using BankAgreements.Infrastructure.Entities.Institutions;
using BankAgreements.Infrastructure.Entities.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BankAgreements.Infrastructure.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(
		DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}

	public DbSet<Institution> Institutions => Set<Institution>();

	public DbSet<Contract> Contracts => Set<Contract>();

	public DbSet<Installment> Installments => Set<Installment>();

	public DbSet<Agreement> Agreements => Set<Agreement>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(
			typeof(AppDbContext).Assembly);
	}
}