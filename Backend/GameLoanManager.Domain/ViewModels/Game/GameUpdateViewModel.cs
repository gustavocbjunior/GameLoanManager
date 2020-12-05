using System;
using Flunt.Notifications;
using Flunt.Validations;
using GameLoanManager.Domain.Enums;

namespace GameLoanManager.Domain.ViewModels.Game
{
    public class GameUpdateViewModel : Notifiable
    {
        public GameUpdateViewModel(long id, string name, EGameType type, string description, long idOwner, bool avaliable)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Description = description;
            this.IdOwner = idOwner;
            this.Available = avaliable;
            this.UpdateAt = DateTime.UtcNow;

            AddNotifications(
                new Contract()
                    .Requires()
                    .IsGreaterThan(Id, 0, "Game ID", "Para alteração, o ID do jogo é obrigatório")
                    .HasMaxLen(Name, 60, "Name", "O nome deve conter até 60 caracteres")
                    .HasMinLen(Name, 3, "Name", "O nome deve conter pelo menos 3 caracteres")
                    .HasMaxLen(Description, 60, "Description", "A descriçao deve conter até 200 caracteres")
                    .IsGreaterThan(IdOwner, 0, "IdOwner", "O ID do usuário a qual o jogo pertence é obrigatório")
            );
        }
        public long Id { get; private set; }
        public string Name { get; private set; }
        public EGameType Type { get; private set; }
        public string Description { get; private set; }
        public long IdOwner { get; private set; }
        public bool Available { get; private set; }
        public DateTime UpdateAt { get; private set; }

    }
}