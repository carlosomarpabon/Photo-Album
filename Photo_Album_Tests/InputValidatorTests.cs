using Microsoft.VisualStudio.TestTools.UnitTesting;
using Photo_Album;

namespace Photo_Album_Tests
{
    [TestClass]
    public class InputValidatorTests
    {
        private InputValidator _inputValidator;
        private const string NOT_INT_INPUT = "NOTINT";
        private const int INT_INPUT = 123;
        [TestInitialize]
        public void Setup()
        {
            _inputValidator = new InputValidator();
        }

        [TestMethod]
        public void Returns_InputResultWithError_When_NotInt()
        {
            var input = NOT_INT_INPUT;
            var result = _inputValidator.IsInt(input);

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Error == Constants.ERROR_IS_NOT_NUMBER);
        }

        [TestMethod]
        public void Returns_InputResultWithIsValid_When_IsInt()
        {
            var input = INT_INPUT.ToString();
            var result = _inputValidator.IsInt(input);

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.Error);
            Assert.IsTrue(result.OutputNumber == INT_INPUT);
        }
    }
}
