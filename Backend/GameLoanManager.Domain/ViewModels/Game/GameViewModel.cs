using GameLoanManager.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GameLoanManager.Domain.ViewModels.Game
{
    public class GameViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EGameType Type { get; set; }
        
        public string Description { get; set; }
        public long IdOwner { get; set; }
        public string Owner { get; set; }
        public bool Available { get; set; }
    }
}