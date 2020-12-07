using GameLoanManager.Domain.Enums;
using GameLoanManager.Domain.ViewModels.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameLoanManager.Test.ViewModels.Game
{
    [TestClass]
    public class GameCreateViewModelTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var game = new GameCreateViewModel (
                "Wa",
                EGameType.Tabuleiro,
                "Descrição do jogo",
                1
            );

            Assert.IsTrue(game.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenDescriptionIsInvalid()
        {
            var game = new GameCreateViewModel (
                "War",
                EGameType.Tabuleiro,
                "O melhor jogo de estratégia de todos os tempos! Com War, uma batalha nunca é igual a outra, e cada jogador precisa usar toda sua habilidade militar para conquistar territórios e continentes e derrotar seus adversários.",
                1
            );

            Assert.IsTrue(game.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenGameCreateViewModelIsValid()
        {
            var game = new GameCreateViewModel (
                "War",
                EGameType.Tabuleiro,
                "O melhor jogo de estratégia de todos os tempos!",
                1
            );
            Assert.IsTrue(game.Valid);
        }
    }
}