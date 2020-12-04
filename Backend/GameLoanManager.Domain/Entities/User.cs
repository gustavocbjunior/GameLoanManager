using System;
using System.Collections.Generic;

namespace GameLoanManager.Domain.Entities
{
    public class User : Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Game> MyGames { get; set; }
        public virtual ICollection<LoanedGame> LoanedGames { get; set; }
    }
}
