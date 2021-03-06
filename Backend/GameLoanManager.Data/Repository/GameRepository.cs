using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameLoanManager.Data.Repository
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        private DbSet<Game> _dataset;
        public GameRepository(Context context) : base(context)
        {
            _dataset = context.Set<Game>();
        }

        public async Task<Game> GetByIdWithRelationships(long id)
        {
            IQueryable<Game> q = _dataset.Include(u => u.User)
                                         .Where(g => g.Id == id);

            return await q.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Game>> GetAllWithRelationships()
        {
            IQueryable<Game> q = _dataset.Include(u => u.User);

            return await q.ToListAsync();
        }

        public async Task<Game> GetAvailable(long id)
        {
            IQueryable<Game> q = _dataset.Include(u => u.User)
                                         .Where(g => g.Id == id && g.Available);

            return await q.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Game>> GetAvailables()
        {
            IQueryable<Game> q = _dataset.Include(u => u.User)
                                         .Where(g => g.Available);

            return await q.ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetByIdUser(long idUser)
        {
            IQueryable<Game> q = _dataset.Where(g => g.IdUser == idUser);

            return await q.ToListAsync();
        }
    }
}