using System;
using Flunt.Notifications;
using Flunt.Validations;
using GameLoanManager.Domain.ValueObjects;

namespace GameLoanManager.Domain.ViewModels.User
{
    public class UserUpdateViewModel : Notifiable, IValidatable
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Email Email { get; set; }
        public string Phone { get; set; }

        public DateTime UpdateAt { get { return DateTime.UtcNow; } }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsGreaterThan(Id, 0, "User ID", "Para alteração, o ID do usuário é obrigatório")
                    .HasMaxLen(Login, 40, "Login", "O login deve conter até 40 caracteres")
                    .HasMinLen(Login, 3, "Login", "O login deve conter pelo menos 3 caracteres")
                    .HasMinLen(Password, 8, "Password", "O Password deve conter pelo menos 8 caracteres")
                    .HasMinLen(Name, 3, "Password", "O Nome deve conter pelo menos 3 caracteres")
                    .HasLen(Phone, 11, "Phone", "O número de telefone deve conter 11 dígitos")
            );
            AddNotifications(Email.Notifications);
        }
    }
}