using BankAgreements.Infrastructure.Repositories.Contracts;
using BankAgreements.Shared.DTOs.Contracts;

namespace BankAgreements.Services.Contracts;

public class ContractService : IContractService
{
    private readonly IContractRepository _repository;

    public ContractService(
        IContractRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ContractDTO>> GetAllAsync()
    {
        var contracts = await _repository.GetAllAsync();

        return contracts.Select(contract =>
        {
            return new ContractDTO
            {
                Id = contract.Id,
                ContractNumber = contract.ContractNumber,
                DebtorName = contract.Debtor.Name,
                InstitutionName = contract.Institution.Name,
                ContractAmount = contract.Amount
            };
        }).ToList();
    }

    public async Task<ContractDTO?> GetByIdAsync(Guid id)
    {
        var contract = await _repository.GetByIdAsync(id);

        if (contract is null)
            return null;

        var openInstallments =
            contract.Installments
                .Where(x => !x.Paid)
                .ToList();

        var overdueInstallments =
            openInstallments
                .Where(x => x.DueDate < DateTime.UtcNow)
                .ToList();

        var debtAmount =
            overdueInstallments.Sum(x => x.Amount * 1.02m)
            +
            openInstallments
                .Where(x => x.DueDate >= DateTime.UtcNow)
                .Sum(x => x.Amount);

        return new ContractDTO
        {
            Id = contract.Id,
            ContractNumber = contract.ContractNumber,

            DebtorName = contract.Debtor.Name,

            InstitutionName = contract.Institution.Name,

            ContractAmount = contract.Amount,

            DebtAmount = decimal.Round(debtAmount, 2),

            TotalInstallments = contract.Installments.Count,

            OpenInstallments = openInstallments.Count,

            OverdueInstallments = overdueInstallments.Count
        };
    }
}