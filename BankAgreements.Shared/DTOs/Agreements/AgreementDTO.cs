namespace BankAgreements.Shared.DTOs.Agreements;

public class AgreementDTO
{
    public Guid Id { get; set; }

    public int Number { get; set; }

    public Guid ContractId { get; set; }

    public string ContractNumber { get; set; } = string.Empty;

    public DateTime DueDate { get; set; }

    public decimal Amount { get; set; }

    public int TotalInstallments { get; set; }
}
