using System.Threading.Tasks;
using GameLoanManager.Domain.Entities;

namespace GameLoanManager.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindForAuthentication(string login, string password);
        Task<User> GetMyGames(long id);
    }
}