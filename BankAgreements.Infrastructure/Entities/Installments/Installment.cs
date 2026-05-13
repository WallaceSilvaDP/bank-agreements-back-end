using BankAgreements.Infrastructure.Entities.Agreements;
using BankAgreements.Infrastructure.Entities.Contracts;

namespace BankAgreements.Infrastructure.Entities.Installments
{
	public class Installment
	{
		public Guid Id { get; set; }
		public required string Number { get; set; }
		public Guid? ContractId { get; set; }
		public Contract? Contract { get; set; }
		public Guid? AgreementId { get; set; }
		public Agreement? Agreement { get; set; }
		public required decimal Amount { get; set; }
		public required DateTime DueDate { get; set; }
		public required bool Paid { get; set; } = false;
	}
}
