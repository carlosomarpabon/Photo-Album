using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
    public class InputValResult
    {
        public bool IsValid { get; set; }
        public string Error { get; set; }
        public int OutputNumber { get; set; }
    }
}
