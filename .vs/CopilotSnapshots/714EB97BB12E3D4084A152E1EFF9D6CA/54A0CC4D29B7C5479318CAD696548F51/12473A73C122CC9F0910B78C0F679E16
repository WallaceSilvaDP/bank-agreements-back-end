using BankAgreements.Infrastructure.Repositories.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BankAgreements.Infrastructure.Extensions
{
    public static class RepositoryCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ContractRepository>();

            return services;
        }
    }
}
