using System;
using Flunt.Notifications;
using Flunt.Validations;

namespace GameLoanManager.Domain.ViewModels.LoanedGame
{
    public class LoanedGameUpdateViewModel : Notifiable, IValidatable
    {
        public long Id { get; set; }
        public long IdUser { get; set; }
        public long IdGame { get; set; }
        public bool Returned { get; set; }

        public DateTime UpdateAt { get { return DateTime.UtcNow; } }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsGreaterThan(Id, 0, "Id Loaned Game", "O ID do empréstimo do jogo é obrigatório")
                    .IsGreaterThan(IdUser, 0, "IdUser", "O ID do usuário é obrigatório")
                    .IsGreaterThan(IdGame, 0, "IdGame", "O ID do jogo é obrigatório")
            );
        }
    }
}