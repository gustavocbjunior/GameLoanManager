using System.Linq;
using System.Threading.Tasks;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameLoanManager.Data.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private DbSet<User> _dbset;

        public UserRepository(Context context) : base(context)
        {
            _dbset = context.Set<User>();
        }

        public async Task<User> FindForAuthentication(string login, string password)
        {
            return await _dbset.FirstOrDefaultAsync(u => u.Login.Equals(login) && u.Password.Equals(password));
        }

        public async Task<User> GetMyGames(long id)
        {
            return await _dbset.Include(g => g.MyGames).FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}