namespace BankAgreements.Shared.DTOs.Contracts;

public class ContractDTO
{
    public Guid Id { get; set; }

    public string ContractNumber { get; set; } = string.Empty;

    public string DebtorName { get; set; } = string.Empty;

    public string InstitutionName { get; set; } = string.Empty;

    public decimal ContractAmount { get; set; }

    public decimal DebtAmount { get; set; }

    public int TotalInstallments { get; set; }

    public int OpenInstallments { get; set; }

    public int OverdueInstallments { get; set; }

    public List<InstallmentDTO> Installments { get; set; } = [];
}