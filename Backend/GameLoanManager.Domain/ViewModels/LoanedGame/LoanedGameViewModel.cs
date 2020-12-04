
using GameLoanManager.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GameLoanManager.Domain.ViewModels.LoanedGame
{
    public class LoanedGameViewModel
    {
        public long Id { get; set; }
        public long IdUser { get; set; }
        public string User { get; set; }

        public long IdGame { get; set; }
        public string Game { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EGameType? GameType { get; set; }

        public bool Returned { get; set; }
    }
}