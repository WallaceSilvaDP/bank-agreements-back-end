using BankAgreements.Infrastructure.Entities.Debtors;
using BankAgreements.Infrastructure.Entities.Installments;
using BankAgreements.Infrastructure.Entities.Institutions;

namespace BankAgreements.Infrastructure.Entities.Contracts
{
	public class Contract
	{
		public Guid Id { get; set; }
		public required string ContractNumber { get; set; }
		public required decimal Amount { get; set; }
		public required Guid DebtorId { get; set; }
		public Debtor? Debtor { get; set; }
		public required Guid InstitutionId { get; set; }
		public Institution? Institution { get; set; }
		public required List<Installment> Installments { get; set; }
	}
}
