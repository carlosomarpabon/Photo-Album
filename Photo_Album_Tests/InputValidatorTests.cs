using Microsoft.VisualStudio.TestTools.UnitTesting;
using Photo_Album;
using System;
using System.Collections.Generic;
using System.Text;

namespace Photo_Album_Tests
{
    [TestClass]
    public class InputValidatorTests
    {
        private InputValidator _inputValidator;

        [TestInitialize]
        public void Setup()
        {
            _inputValidator = new InputValidator();
        }

        [TestMethod]
        public void Returns_InputResultWithError_When_NotInt()
        {
            var input = "NOTINT";
            var result = _inputValidator.IsInt(input);

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Error == Constants.ERROR_IS_NOT_NUMBER);
        }

        [TestMethod]
        public void Returns_InputResultWithIsValid_When_IsInt()
        {
            var input = "123";
            var result = _inputValidator.IsInt(input);

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.Error);
            Assert.IsTrue(result.OutputNumber == 123);
        }
    }
}
