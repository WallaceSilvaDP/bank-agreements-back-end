using BankAgreements.Infrastructure.Entities.Contracts;

namespace BankAgreements.Infrastructure.Repositories.Contracts;

public interface IContractRepository
{
    Task<List<Contract>> GetAllAsync();

    Task<Contract?> GetByIdAsync(Guid id);
}