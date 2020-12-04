using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.ViewModels;

namespace GameLoanManager.Domain.Interfaces
{
    public interface ITokenService
    {
        TokenViewModel GenerateToken(User user);
    }
}