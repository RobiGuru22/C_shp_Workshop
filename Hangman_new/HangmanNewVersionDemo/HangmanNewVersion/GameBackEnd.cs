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
        public static string? CurrentGuess { get; set; }
        public static DifficultyEnum CurrentDifficulty { get; set; }

        public static string? GuessableWord {  get; set; }
        public static int AttemptsLeft = 7;
        public static List<char>? DisplayCharacters { get; set; }
        public static List<char>? ActualCharacters { get; set; }
        public static List<string>? IncorrectlyGuessedCharacters { get; set; }

        public static List<string> CurrentAllowedMultipleCharacterWordInGueassableWords = new List<string>();

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
            if(GameBackendHelper.GameOverCheck() > 0)
            {
                if (!UserInputChecker.IsGuessFormatCorrect())
                {
                    GameFrontEnd.IncorrectGuessFormatText();
                }
                else if (!UserInputChecker.IsGuessCorrect())
                {
                    GameBackendHelper.IncorrectGuessLogic();
                }
                else
                {
                    GameBackendHelper.ImplementGuess();
                }
            }
            else
            {
                GameFrontEnd.GameOverWindow(GameOverWindow.GetEnumByInt(GameBackendHelper.GameOverCheck()));
                GameOver = true;
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

        public static void IncorrectGuessFormatTextLogic()
        {
            string? inputGuess = Console.ReadLine();

            if (
                inputGuess == null || 
                GuessableWord == null ||
                int.TryParse(inputGuess, out int temp) || 
                (inputGuess.Length > 1 && !GameBackendHelper.AllowedMultipleCharacters.Any(x => x == inputGuess))
            )
            {
                GameFrontEnd.WrongGuessFormatTextClear();
                GameFrontEnd.IncorrectGuessFormatText();
            }
            else
            {
                CurrentGuess = inputGuess;
                GameBackendHelper.ImplementGuess();
                GameFrontEnd.GameWindow();
            }
        }
    }
}
