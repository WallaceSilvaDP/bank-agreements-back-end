namespace BankAgreements.Services.Agreements.Calculators;

public class InstallmentCalculator
{
    /// <summary>
    /// Calcula o valor de cada parcela com juros usando o sistema francês (Price)
    /// </summary>
    /// <param name="totalAmount">Valor total a parcelar</param>
    /// <param name="numberOfInstallments">Número de parcelas</param>
    /// <param name="annualInterestRate">Taxa de juros anual (ex: 0.10 para 10%)</param>
    /// <returns>Valor de cada parcela com juros inclusos</returns>
    public static decimal CalculateInstallmentAmount(
        decimal totalAmount,
        int numberOfInstallments,
        decimal annualInterestRate)
    {
        if (numberOfInstallments <= 0)
            throw new ArgumentException("Número de parcelas deve ser maior que zero.", nameof(numberOfInstallments));

        // Converter taxa anual para mensal
        var monthlyRate = annualInterestRate / 12;

        // Se não há juros, divide igualmente
        if (monthlyRate == 0)
            return decimal.Round(totalAmount / numberOfInstallments, 2);

        // Fórmula de cálculo da parcela (Price/SAC adaptado)
        // P = V * [i * (1 + i)^n] / [(1 + i)^n - 1]
        var numerator = monthlyRate * (decimal)Math.Pow((double)(1 + monthlyRate), numberOfInstallments);
        var denominator = (decimal)Math.Pow((double)(1 + monthlyRate), numberOfInstallments) - 1;

        var installmentAmount = totalAmount * (numerator / denominator);

        return decimal.Round(installmentAmount, 2);
    }

    /// <summary>
    /// Calcula a lista de parcelas com datas espaçadas mensalmente
    /// </summary>
    /// <param name="startDate">Data de início do parcelamento</param>
    /// <param name="numberOfInstallments">Número de parcelas</param>
    /// <param name="installmentAmount">Valor de cada parcela</param>
    /// <returns>Lista de tuplas (numero, data, valor)</returns>
    public static List<(int Number, DateTime DueDate, decimal Amount)> CalculateInstallmentDates(
        DateTime startDate,
        int numberOfInstallments,
        decimal installmentAmount)
    {
        var installments = new List<(int, DateTime, decimal)>();

        for (int i = 1; i <= numberOfInstallments; i++)
        {
            var dueDate = startDate.AddMonths(i);
            installments.Add((i, dueDate, installmentAmount));
        }

        return installments;
    }

    /// <summary>
    /// Ajusta a última parcela para garantir que o total seja exato
    /// </summary>
    public static void AdjustLastInstallment(
        List<(int Number, DateTime DueDate, decimal Amount)> installments,
        decimal totalAmount)
    {
        if (installments.Count == 0)
            return;

        // Calcular o total até a penúltima parcela
        var sumExceptLast = installments
            .SkipLast(1)
            .Sum(x => x.Amount);

        // Ajustar a última parcela
        var lastIndex = installments.Count - 1;
        var adjustedLastAmount = totalAmount - sumExceptLast;

        installments[lastIndex] = (
            installments[lastIndex].Number,
            installments[lastIndex].DueDate,
            decimal.Round(adjustedLastAmount, 2)
        );
    }
}
