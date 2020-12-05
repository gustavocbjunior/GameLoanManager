using Flunt.Notifications;
using Flunt.Validations;

namespace GameLoanManager.Domain.ViewModels.LoanedGame
{
    public class LoanedGameCreateViewModel : Notifiable
    {
        public LoanedGameCreateViewModel(long idUser, long idGame)
        {
            this.IdUser = idUser;
            this.IdGame = idGame;

            AddNotifications(
                new Contract()
                    .Requires()
                    .IsGreaterThan(IdUser, 0, "IdUser", "O ID do usuário é obrigatório")
                    .IsGreaterThan(IdGame, 0, "IdGame", "O ID do jogo é obrigatório")
            );
        }
        public long IdUser { get; private set; }
        public long IdGame { get; private set; }
        public bool Returned { get { return false; } }
    }
}