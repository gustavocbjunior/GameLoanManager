using System.Collections.Generic;
using System.Threading.Tasks;
using GameLoanManager.Domain.Entities;

namespace GameLoanManager.Domain.Interfaces
{
    public interface ILoanedGameRepository : IRepository<LoanedGame>
    {
        Task<LoanedGame> GetByIdWithRelationshipsAsync(long id);
        Task<IEnumerable<LoanedGame>> GetAllWithRelationshipsAsync();
        Task<IEnumerable<LoanedGame>> GetReturnedWithRelationshipsAsync();
        Task<IEnumerable<LoanedGame>> GetByUserAsync(long idUser);
        Task<IEnumerable<LoanedGame>> GetByGameAsync(long idGame);
    }
}