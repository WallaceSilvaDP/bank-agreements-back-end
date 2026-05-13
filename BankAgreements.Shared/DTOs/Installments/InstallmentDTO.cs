namespace BankAgreements.Shared.DTOs.Contracts;

public class InstallmentDTO
{
    public Guid Id { get; set; }

    public string Number { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public DateTime DueDate { get; set; }

    public bool Paid { get; set; }

    public bool Overdue =>
        !Paid && DueDate < DateTime.UtcNow;
}