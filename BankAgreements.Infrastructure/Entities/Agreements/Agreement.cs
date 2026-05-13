using BankAgreements.Infrastructure.Entities.Contracts;
using BankAgreements.Infrastructure.Entities.Installments;

namespace BankAgreements.Infrastructure.Entities.Agreements
{
	public class Agreement
	{
		public Guid Id { get; set; }
		public int Number { get; set; }
		public Guid ContractId { get; set; }
		public required Contract Contract { get; set; }
		public required DateTime DueDate { get; set; }
		public required decimal Amount { get; set; }
		public required List<Installment> Installments { get; set; }

	}
}
