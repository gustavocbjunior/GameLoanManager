using GameLoanManager.Domain.Interfaces;
using GameLoanManager.Service;
using Microsoft.Extensions.DependencyInjection;

namespace GameLoanManager.Helpers.DependencyInjection
{
    public class ServicesConfiguration
    {
        public static void ConfigureDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IGameService, GameService>();
            serviceCollection.AddTransient<ILoanedGameService, LoanedGameService>();
            serviceCollection.AddTransient<ITokenService, TokenService>();
        }
    }
}