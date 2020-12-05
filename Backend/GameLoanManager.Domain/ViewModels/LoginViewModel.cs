using Flunt.Notifications;
using Flunt.Validations;

namespace GameLoanManager.Domain.ViewModels
{
    public class LoginViewModel : Notifiable
    {
        public LoginViewModel(string login, string password)
        {
            this.Login = login;
            this.Password = password;

            AddNotifications(
                new Contract()
                    .Requires()
                    .IsNotNullOrWhiteSpace(Login, "Login", "O login é obrigatório")
                    .HasMaxLen(Login, 40, "Login", "O login deve conter até 40 caracteres")
                    .HasMinLen(Login, 3, "Login", "O login deve conter pelo menos 3 caracteres")
                    .HasMinLen(Password, 8, "Password", "O password deve conter pelo menos 8 caracteres")
                    .IsNotNullOrWhiteSpace(Password, "Password", "O password é obrigatório")
            );
        }
        public string Login { get; private set; }
        public string Password { get; private set; }
    }
}