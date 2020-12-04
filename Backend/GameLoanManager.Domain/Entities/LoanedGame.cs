using System;

namespace GameLoanManager.Domain.Entities
{
    public class LoanedGame : Entity
    {
        public long IdUser { get; set; }
        public virtual User User { get; set; }

        public long IdGame { get; set; }
        public virtual Game Game { get; set; }

        public bool Returned { get; set; }
    }
}
