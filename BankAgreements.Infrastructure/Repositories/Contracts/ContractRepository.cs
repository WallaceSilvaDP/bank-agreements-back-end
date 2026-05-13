using Microsoft.EntityFrameworkCore;
using BankAgreements.Infrastructure.Data;
using BankAgreements.Infrastructure.Entities.Contracts;

namespace BankAgreements.Infrastructure.Repositories.Contracts;

public class ContractRepository : IContractRepository
{
    private readonly AppDbContext _context;

    public ContractRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Contract>> GetAllAsync()
    {
        return await _context.Contracts
            .Include(x => x.Debtor)
            .Include(x => x.Institution)
            .Include(x => x.Installments)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Contract?> GetByIdAsync(Guid id)
    {
        return await _context.Contracts
            .Include(x => x.Debtor)
            .Include(x => x.Institution)
            .Include(x => x.Installments)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}