using Flunt.Notifications;
using Flunt.Validations;

namespace GameLoanManager.Domain.ViewModels.LoanedGame
{
    public class LoanedGameCreateViewModel : Notifiable, IValidatable
    {
        public long IdUser { get; set; }
        public long IdGame { get; set; }
        public bool Returned { get { return false; } }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsGreaterThan(IdUser, 0, "IdUser", "O ID do usuário é obrigatório")
                    .IsGreaterThan(IdGame, 0, "IdGame", "O ID do jogo é obrigatório")
            );
        }
    }
}