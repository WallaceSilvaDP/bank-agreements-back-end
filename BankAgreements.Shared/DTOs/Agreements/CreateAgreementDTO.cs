namespace BankAgreements.Shared.DTOs.Agreements;

public class CreateAgreementDTO
{
    public Guid ContractId { get; set; }

    //public int NumberOfInstallments { get; set; }

    public DateTime FirstDueDate { get; set; } = GetDefaultFirstDueDate();

    private static DateTime GetDefaultFirstDueDate()
    {
        var today = DateTime.UtcNow;
        var nextMonth = today.AddMonths(1);
        return new DateTime(nextMonth.Year, nextMonth.Month, 12, 0, 0, 0, DateTimeKind.Utc);
    }
}
