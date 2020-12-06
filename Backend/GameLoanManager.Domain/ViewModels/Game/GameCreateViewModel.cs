using Flunt.Notifications;
using Flunt.Validations;
using GameLoanManager.Domain.Enums;

namespace GameLoanManager.Domain.ViewModels.Game
{
    public class GameCreateViewModel : Notifiable
    {
        public GameCreateViewModel(string name, EGameType type, string description, long idOwner)
        {
            this.Name = name;
            this.Type = type;
            this.Description = description;
            this.IdOwner = idOwner;
            this.Available = true;

            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMaxLen(Name, 60, "Name", "O nome deve conter até 60 caracteres")
                    .HasMinLen(Name, 3, "Name", "O nome deve conter pelo menos 3 caracteres")
                    .HasMaxLen(Description, 200, "Description", "A descriçao deve conter até 200 caracteres")
                    .IsGreaterThan(IdOwner, 0, "IdOwner", "O ID do usuário a qual o jogo pertence é obrigatório")
            );
        }
        public string Name { get; private set; }
        public EGameType Type { get; private set; }
        public string Description { get; private set; }
        public long IdOwner { get; private set; }
        public bool Available { get; private set; }
    }
}