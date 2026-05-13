namespace BankAgreements.Shared.DTOs.Debtors;

public class DebtorDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Document { get; set; } = string.Empty;
}