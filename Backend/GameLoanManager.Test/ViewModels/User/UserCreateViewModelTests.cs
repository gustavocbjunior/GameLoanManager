using System.Text;
using GameLoanManager.Domain.ValueObjects;
using GameLoanManager.Domain.ViewModels.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameLoanManager.Test.ViewModels.User
{
    [TestClass]
    public class UserCreateViewModelTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenLoginIsInvalid()
        {
            var user = new UserCreateViewModel (
                "gb",
                "12345678",
                "Gustavo",
                new Email("gustavobarbosaa@gmail.com"),
                "83999869309"
            );

            Assert.IsTrue(user.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenPasswordIsInvalid()
        {
            var user = new UserCreateViewModel (
                "gustavo",
                "1234",
                "Gustavo",
                new Email("gustavobarbosaa@gmail.com"),
                "83999869309"
            );

            Assert.IsTrue(user.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var user = new UserCreateViewModel (
                "gustavo",
                "12345678",
                "gb",
                new Email("gustavobarbosaa@gmail.com"),
                "83999869309"
            );

            Assert.IsTrue(user.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenPhoneIsInvalid()
        {
            var user = new UserCreateViewModel (
                "gustavo",
                "12345678",
                "Gustavo",
                new Email("gustavobarbosaa@gmail.com"),
                "999869309" //Sem DDD
            );

            Assert.IsTrue(user.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenUserCreateViewModelIsValid()
        {
            var user = new UserCreateViewModel (
                "gustavo",
                "12345678",
                "Gustavo",
                new Email("gustavobarbosaa@gmail.com"),
                "83999869309" 
            );

            StringBuilder error = new StringBuilder();
            foreach (var item in user.Notifications)
            {
                error.Append("\r\n\t").Append(item.Message);
            }

            Assert.IsTrue(user.Valid, error.ToString());
        }
    }
}