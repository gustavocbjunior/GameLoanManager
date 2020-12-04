using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameLoanManager.Data.Repository
{
    public class LoanedGameRepository : RepositoryBase<LoanedGame>, ILoanedGameRepository
    {
        private DbSet<LoanedGame> _dataset;
        public LoanedGameRepository(Context context) : base(context)
        {
            _dataset = context.Set<LoanedGame>();
        }

        public async Task<LoanedGame> GetByIdWithRelationshipsAsync(long id)
        {
            IQueryable<LoanedGame> q = _dataset.Include(u => u.User)
                                               .Include(g => g.Game)
                                               .Where(l => l.Id == id);

            return await q.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<LoanedGame>> GetAllWithRelationshipsAsync()
        {
            IQueryable<LoanedGame> q = _dataset.Include(p => p.User)
                                               .Include(g => g.Game);

            return await q.ToListAsync();
        }

        public async Task<IEnumerable<LoanedGame>> GetReturnedWithRelationshipsAsync()
        {
            IQueryable<LoanedGame> q = _dataset.Include(u => u.User)
                                               .Include(g => g.Game)
                                               .Where(l => l.Returned == true);

            return await q.ToListAsync();
        }

        public async Task<IEnumerable<LoanedGame>> GetByUserAsync(long idUser)
        {
            IQueryable<LoanedGame> q = _dataset.Include(u => u.User)
                                               .Include(g => g.Game)
                                               .Where(l => l.IdUser == idUser);

            return await q.ToListAsync();
        }

        public async Task<IEnumerable<LoanedGame>> GetByGameAsync(long idGame)
        {
            IQueryable<LoanedGame> q = _dataset.Include(u => u.User)
                                               .Include(g => g.Game)
                                               .Where(l => l.IdGame == idGame);

            return await q.ToListAsync();
        }
    }
}