namespace BankAgreements.Shared.DTOs.Institutions;

public class InstitutionDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Cnpj { get; set; } = string.Empty;

    public int MaxInstallments { get; set; }

    public decimal AnnualInterestRate { get; set; }
}
