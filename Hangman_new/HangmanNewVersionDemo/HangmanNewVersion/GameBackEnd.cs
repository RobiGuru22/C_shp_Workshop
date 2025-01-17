using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanNewVersion
{
    public class GameBackEnd
    {
        public static bool GameOver = false;
        public static int CurrentInput { get; set; }
        public static char CurrentGuess { get; set; }
        public static DifficultyEnum CurrentDifficulty { get; set; }

        public static string? GuessableWord {  get; set; }
        public static int AttemptsLeft = 7;
        public static List<char>? DisplayCharacters { get; set; }
        public static List<char>? IncorrectlyGuessedCharacters { get; set; }

        public static void MainWindowLogic()
        {
            List<int> correctInputs = new List<int> { 0, 1 };
            if(!UserInputChecker.IsInputCorrect(correctInputs)) 
            {
                GameFrontEnd.IncorrectInputText(ActiveWindowEnum.MAIN_WINDOW_ACTIVE);
            }

            GameBackendHelper.MainWindowCorrectInput();
        }

        public static void DifficultyChooserWindowLogic()
        {
            List<int> correctInputs = new List<int> { 0, 1, 2, 3 };
            if (!UserInputChecker.IsInputCorrect(correctInputs))
            {
                GameFrontEnd.IncorrectInputText(ActiveWindowEnum.DIFFICULTY_CHOOSER_WINDOW_ACTIVE);
            }

            GameBackendHelper.DifficultyChooserWindowCorrectInput();
        }

        public static void GameWindowLogic()
        {
            if(!UserInputChecker.IsGuessCorrect())
            {
                GameFrontEnd.IncorrectGuessText();
            }
            else
            {
                if(GuessableWord != null)
                {
                    for (int i = 0; i < GuessableWord.Length; i++)
                    {

                    }
                }
            }
        }

        public static void IncorrectInputTextLogic(ActiveWindowEnum activeWindowEnum)
        {
            string? inputString = Console.ReadLine();
            int inputNumber;
            switch(activeWindowEnum)
            {
                case ActiveWindowEnum.MAIN_WINDOW_ACTIVE:
                    if (!int.TryParse(inputString, out inputNumber) || (inputNumber != 0 && inputNumber != 1))
                    {
                        GameFrontEnd.WrongInputTextClear();
                        GameFrontEnd.IncorrectInputText(ActiveWindowEnum.MAIN_WINDOW_ACTIVE);
                    }
                    else 
                    {
                        CurrentInput = inputNumber;
                        GameBackendHelper.MainWindowCorrectInput();
                    }
                    break;
                case ActiveWindowEnum.DIFFICULTY_CHOOSER_WINDOW_ACTIVE:
                    if (!int.TryParse(inputString, out inputNumber) || (inputNumber != 0 && inputNumber != 1))
                    {
                        GameFrontEnd.WrongInputTextClear();
                        GameFrontEnd.IncorrectInputText(ActiveWindowEnum.DIFFICULTY_CHOOSER_WINDOW_ACTIVE);
                    }
                    else
                    {
                        CurrentInput = inputNumber;
                        GameBackendHelper.DifficultyChooserWindowCorrectInput();
                    }
                    break;
            }
        }
    }
}
