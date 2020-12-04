using GameLoanManager.Data.Repository;
using GameLoanManager.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GameLoanManager.Helpers.DependencyInjection
{
    public class RepositoriesConfiguration
    {
        public static void ConfigureDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<ILoanedGameRepository, LoanedGameRepository>();
            serviceCollection.AddScoped<IGameRepository, GameRepository>();
        }
        
    }
}