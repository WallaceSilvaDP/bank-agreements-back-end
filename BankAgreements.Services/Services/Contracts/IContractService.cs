using BankAgreements.Infrastructure.Repositories.Contracts;
using BankAgreements.Shared.DTOs.Contracts;

namespace BankAgreements.Services.Contracts;

public interface IContractService
{
    Task<List<ContractDTO>> GetAllAsync();

    Task<ContractDTO?> GetByIdAsync(Guid id);
}
