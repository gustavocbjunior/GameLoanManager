using System.Collections.Generic;
using System.Threading.Tasks;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.ViewModels;
using GameLoanManager.Domain.ViewModels.LoanedGame;

namespace GameLoanManager.Domain.Interfaces
{
    public interface ILoanedGameService
    {
        Task<LoanedGameViewModel> GetByIdWithRelationships(long id);
        Task<IEnumerable<LoanedGameViewModel>> GetAllWithRelationships();
        Task<IEnumerable<LoanedGameViewModel>> GetReturnedWithRelationships();
        Task<IEnumerable<LoanedGameViewModel>> GetByUser(long idUser);
        Task<IEnumerable<LoanedGameViewModel>> GetByGame(long idGame);
        Task<LoanedGameViewModel> Get(long id);
        Task<IEnumerable<LoanedGameViewModel>> GetAll();
        Task<ResultViewModel> Post(LoanedGameCreateViewModel loanedGame);
        Task<ResultViewModel> Put(LoanedGameUpdateViewModel loanedGame);
        Task<ResultViewModel> Delete(long id);
    }
}