using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameLoanManager.Test.Entities
{
    [TestClass]
    public class LoanedGameTests
    {
        private LoanedGame _loanedGame;
        public LoanedGameTests()
        {
            _loanedGame = new LoanedGame{
                Id = 1,
                IdGame = 1,
                Game = new Game {
                    Id = 1,
                    Name = "Far Cry 5",
                    Type = EGameType.DVD,
                    Available = true
                },
                IdUser = 1
            };
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenGameAvailable()
        {
            Assert.IsTrue(_loanedGame.IsValid());
        }
    }
}