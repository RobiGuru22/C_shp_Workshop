using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanBackendLibrary
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
            MainBackendLogic.CurrentInput = inputNumber;
            return true;
        }

        public static bool IsGuessFormatCorrect()
        {
            string? inputString = Console.ReadLine();
            if (
                inputString == null ||
                int.TryParse(inputString, out int temp) ||
                (inputString.Length > 1 && !BackendHelperLogic.AllowedMultipleCharacters.Any(x => x == inputString))
                )
            {
                return false;
            }
            MainBackendLogic.CurrentGuess = inputString;
            return true;
        }

        public static bool IsGuessCorrect()
        {
            if (!MainBackendLogic.GuessableWord.Contains(MainBackendLogic.CurrentGuess))
            {
                return false;
            }
            return true;
        }
    }
}
