using System;
using Flunt.Notifications;
using Flunt.Validations;

namespace GameLoanManager.Domain.ValueObjects
{
    public class Email : Notifiable
    {
        public Email(string address)
        {
            Address = address;

            AddNotifications(new Contract()
                .Requires()
                .IsEmail(Address, "E-mail Address", "E-mail inválido")
            );
        }

        public string Address { get; private set; }
    }
}
