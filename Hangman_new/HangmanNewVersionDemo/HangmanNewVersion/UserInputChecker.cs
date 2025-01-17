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
            List<string> allowedMultipleCharacters = new List<string>
            {
                "cs",
                "dz",
                "dzs",
                "gy",
                "ly",
                "sz",
                "ty"
            };
            if (
                int.TryParse(inputString, out int temp) ||
                inputString == null ||
                (!allowedMultipleCharacters.Any(x => x == inputString) && inputString.Length > 1)
                )
            {
                return false;
            }
            GameBackEnd.CurrentGuess = char.Parse(inputString);
            return true;
        }
    }
}
