using System;

namespace GameLoanManager.Domain.ViewModels
{
    public class TokenViewModel
    {
        public string User { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpirationDate { get; set; }

    }
}