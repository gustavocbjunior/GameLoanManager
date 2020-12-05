using System.Collections.Generic;
using GameLoanManager.Domain.Enums;

namespace GameLoanManager.Domain.Entities
{
    public class Game : Entity
    {
        public string Name { get; set; }
        public EGameType Type { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        
        // Game Owner
        public long IdUser { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<LoanedGame> LoanedGames { get; set; }
    }
}
