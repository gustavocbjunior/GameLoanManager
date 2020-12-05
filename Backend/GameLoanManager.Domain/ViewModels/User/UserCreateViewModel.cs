using Flunt.Notifications;
using Flunt.Validations;
using GameLoanManager.Domain.ValueObjects;

namespace GameLoanManager.Domain.ViewModels.User
{
    public class UserCreateViewModel : Notifiable
    {
        public UserCreateViewModel(string login, string password, string name, Email email, string phone)
        {
            this.Login = login;
            this.Password = password;
            this.Name = name;
            this.Email = email;
            this.Phone = phone;

            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMaxLen(Login, 40, "Login", "O login deve conter até 40 caracteres")
                    .HasMinLen(Login, 3, "Login", "O login deve conter pelo menos 3 caracteres")
                    .HasMinLen(Password, 8, "Password", "O Password deve conter pelo menos 8 caracteres")
                    .HasMinLen(Name, 3, "Password", "O Nome deve conter pelo menos 3 caracteres")
                    .HasLen(Phone, 11, "Phone", "O número de telefone deve conter 11 dígitos")
            );
            AddNotifications(Email.Notifications);
        }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public string Phone { get; private set; }

    }
}