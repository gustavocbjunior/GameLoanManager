using System.Collections.Generic;
using System.Threading.Tasks;
using GameLoanManager.Domain.ViewModels;
using GameLoanManager.Domain.ViewModels.User;

namespace GameLoanManager.Domain.Interfaces
{
    public interface IUserService
    {
        Task<ResultViewModel> Authentication(LoginViewModel login);
        Task<UserViewModel> Get(long id);
        Task<UserViewModel> GetMyGames(long id);
        Task<IEnumerable<UserViewModel>> GetAll();
        Task<ResultViewModel> Post(UserCreateViewModel user);
        Task<ResultViewModel> Put(UserUpdateViewModel user);
        Task<ResultViewModel> Delete(long id);
    }
}
