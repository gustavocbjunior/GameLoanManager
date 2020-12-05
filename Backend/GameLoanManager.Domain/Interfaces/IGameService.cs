using System.Collections.Generic;
using System.Threading.Tasks;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.ViewModels;
using GameLoanManager.Domain.ViewModels.Game;

namespace GameLoanManager.Domain.Interfaces
{
    public interface IGameService
    {
        Task<GameViewModel> GetByIdWithRelationships(long id);
        Task<IEnumerable<GameViewModel>> GetAllWithRelationships();
        Task<GameViewModel> GetAvailable(long id);
        Task<IEnumerable<GameViewModel>> GetAvailables();
        Task<GameViewModel> Get(long id);
        Task<IEnumerable<GameViewModel>> GetAll();
        Task<ResultViewModel> Post(GameCreateViewModel user);
        Task<ResultViewModel> Put(GameUpdateViewModel user);
        Task<ResultViewModel> Delete(long id);
    }
}