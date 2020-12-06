using GameLoanManager.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameLoanManager.Test.ValueObjects
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenEmailIsInvalid()
        {
            var email = new Email("gustavo@gmail");
            Assert.IsTrue(email.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenEmailIsValid()
        {
            var email = new Email("gustavo@gmail.com");
            Assert.IsTrue(email.Valid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("@gmail.com")]
        [DataRow("gustavo@gmail")]
        [DataRow("gustavo@gmail.com")]
        public void ShouldReturnSuccessWhenEmailIsValid(string value)
        {
            var email = new Email(value);
            Assert.IsTrue(email.Valid);
        }
    }
}