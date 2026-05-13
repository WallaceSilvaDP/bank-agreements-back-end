namespace BankAgreements.Infrastructure.Entities.Debtors
{
	public class Debtor
	{
		public Guid Id { get; set; }
		public required string Name { get; set; }
		public required string Documents { get; set; }
	}
}
