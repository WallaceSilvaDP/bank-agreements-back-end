using BankAgreements.Infrastructure.Entities.Debtors;

namespace BankAgreements.Infrastructure.Seed;

public static class DebtorSeed
{
	public static List<Debtor> GetDebtors()
	{
		return
		[
			new Debtor
			{
				Id = Guid.Parse("c7106e39-2d81-491d-95bf-13fbfbaa703f"),

				Name = "João da Silva",

				Documents = "123.456.789-00"
			}
		];
	}
}