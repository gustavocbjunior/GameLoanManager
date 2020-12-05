using System.Collections.Generic;
using System.Threading.Tasks;
using GameLoanManager.Domain.Entities;

namespace GameLoanManager.Domain.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<Game> GetByIdWithRelationships(long id);
        Task<IEnumerable<Game>> GetAllWithRelationships();
        Task<Game> GetAvailable(long id);
        Task<IEnumerable<Game>> GetAvailables();
    }
}