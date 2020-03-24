using Photo_Album.Models;

namespace Photo_Album
{
    public interface IInputValidator
    {
        InputValResult IsInt(string input);
    }
    public class InputValidator : IInputValidator
    {
        public InputValResult IsInt(string input)
        {
            var result = new InputValResult();           
            if (result.IsValid = int.TryParse(input, out int num))
            {
                result.OutputNumber = num;
            }
            else
            {
                result.Error = Constants.ERROR_IS_NOT_NUMBER;
            }
            return result;
        }
    }
}
