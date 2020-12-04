using Flunt.Notifications;
using Flunt.Validations;
using GameLoanManager.Domain.Enums;

namespace GameLoanManager.Domain.ViewModels.Game
{
    public class GameCreateViewModel :  Notifiable, IValidatable
    {
        public string Name { get; set; }
        public EGameType Type { get; set; }
        public string Description { get; set; }
        public long IdOwner { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMaxLen(Name, 60, "Name", "O nome deve conter até 60 caracteres")
                    .HasMinLen(Name, 3, "Name", "O nome deve conter pelo menos 3 caracteres")
                    .HasMaxLen(Description, 60, "Description", "A descriçao deve conter até 200 caracteres")
                    .IsGreaterThan(IdOwner, 0, "IdOwner", "O ID do usuário a qual o jogo pertence é obrigatório")
            );
        }
    }
}