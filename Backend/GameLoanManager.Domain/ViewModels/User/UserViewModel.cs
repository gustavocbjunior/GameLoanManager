using System.Collections.Generic;
using GameLoanManager.Domain.ViewModels.Game;

namespace GameLoanManager.Domain.ViewModels.User
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IEnumerable<GameViewModel> Games { get; set; }
    }
}