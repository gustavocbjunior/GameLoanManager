using GameLoanManager.Domain.ViewModels.LoanedGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameLoanManager.Test.ViewModels.LoanedGame
{
    [TestClass]
    public class LoanedGameCreateViewModelTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenIdUserIsInvalid()
        {
            var loanedGame = new LoanedGameCreateViewModel (0,1);

            Assert.IsTrue(loanedGame.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenIdGameIsInvalid()
        {
            var loanedGame = new LoanedGameCreateViewModel (1,0);

            Assert.IsTrue(loanedGame.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenLoanedGameCreateViewModelIsValid()
        {
            var loanedGame = new LoanedGameCreateViewModel (1,1);

            Assert.IsTrue(loanedGame.Valid);
        }
    }
}