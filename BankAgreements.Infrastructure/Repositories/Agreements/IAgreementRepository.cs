using BankAgreements.Infrastructure.Entities.Agreements;

namespace BankAgreements.Infrastructure.Repositories.Agreements;

public interface IAgreementRepository
{
    Task<List<Agreement>> GetAllAsync();

    Task<Agreement?> GetByIdAsync(Guid id);

    Task<Agreement> CreateAsync(Agreement agreement);

    Task<Agreement?> GetByIdWithContractAsync(Guid contractId);
}
