using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanNewVersion
{
    public class UserInputChecker
    {
        
        public static bool IsInputCorrect(List<int> acceptableInputs)
        {
            string? inputString = Console.ReadLine();
            if (!int.TryParse(inputString, out int inputNumber) || !acceptableInputs.Any(x => x == inputNumber))
            {
                return false;
            }
            GameBackEnd.CurrentInput = inputNumber;
            return true;
        }

        public static bool IsGuessCorrect()
        {
            string? inputString = Console.ReadLine();
            if (
                inputString == null ||
                int.TryParse(inputString, out int temp) ||
                (inputString.Length > 1 && !GameBackendHelper.AllowedMultipleCharacters.Any(x => x == inputString))
                )
            {
                return false;
            }
            GameBackEnd.CurrentGuess = inputString;
            return true;
        }
    }
}
