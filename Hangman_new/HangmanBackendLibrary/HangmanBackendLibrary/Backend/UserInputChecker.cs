using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanNewVersion.Backend
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
            BackendLogic.CurrentInput = inputNumber;
            return true;
        }

        public static bool IsGuessFormatCorrect()
        {
            string? inputString = Console.ReadLine();
            if (
                inputString == null ||
                int.TryParse(inputString, out int temp) ||
                (inputString.Length > 1 && !BackendHelper.AllowedMultipleCharacters.Any(x => x == inputString))
                )
            {
                return false;
            }
            BackendLogic.CurrentGuess = inputString;
            return true;
        }

        public static bool IsGuessCorrect()
        {
            if (!BackendLogic.GuessableWord.Contains(BackendLogic.CurrentGuess))
            {
                return false;
            }
            return true;
        }
    }
}
