using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangmanFrontendLibrary;

namespace HangmanBackendLibrary
{
    public class MainBackendLogic
    {
        public static bool GameOver = false;
        public static int CurrentInput { get; set; }
        public static string? CurrentGuess { get; set; }
        public static DifficultyEnum CurrentDifficulty { get; set; }

        public static string? GuessableWord { get; set; }
        public static int AttemptsLeft = 7;
        public static List<char>? DisplayCharacters { get; set; }
        public static List<char>? ActualCharacters { get; set; }
        public static List<string>? IncorrectlyGuessedCharacters { get; set; }

        public static List<string> CurrentAllowedMultipleCharacterWordInGueassableWords = new List<string>();

        public static string[] hangmanPics =
        {
            "\n  +---+\n      |\n      |\n      |\n      |\n      |\n=========",
            "\n  +---+\n  |   |\n      |\n      |\n      |\n      |\n=========",
            "\n  +---+\n  |   |\n  O   |\n      |\n      |\n      |\n=========",
            "\n  +---+\n  |   |\n  O   |\n  |   |\n      |\n      |\n=========",
            "\n  +---+\n  |   |\n  O   |\n /|   |\n      |\n      |\n=========",
            "\n  +---+\n  |   |\n  O   |\n /|\\  |\n      |\n      |\n=========",
            "\n  +---+\n  |   |\n  O   |\n /|\\  |\n /    |\n      |\n=========",
            "\n  +---+\n  |   |\n  O   |\n /|\\  |\n / \\  |\n      |\n========="
        };

        public static void MainWindowLogic()
        {
            List<int> correctInputs = new List<int> { 0, 1 };
            if (!UserInputChecker.IsInputCorrect(correctInputs))
            {
                MainFrontendLogic.IncorrectInputText(ActiveWindowEnum.MAIN_WINDOW_ACTIVE);
            }

            BackendHelperLogic.MainWindowCorrectInput();
        }

        public static void DifficultyChooserWindowLogic()
        {
            List<int> correctInputs = new List<int> { 0, 1, 2, 3 };
            if (!UserInputChecker.IsInputCorrect(correctInputs))
            {
                MainFrontendLogic.IncorrectInputText(ActiveWindowEnum.DIFFICULTY_CHOOSER_WINDOW_ACTIVE);
            }

            BackendHelperLogic.DifficultyChooserWindowCorrectInput();
        }

        public static void GameWindowLogic()
        {
            if (BackendHelperLogic.GameOverCheck() > 0)
            {
                if (!UserInputChecker.IsGuessFormatCorrect())
                {
                    MainFrontendLogic.IncorrectGuessFormatText();
                }
                else if (!UserInputChecker.IsGuessCorrect())
                {
                    BackendHelperLogic.IncorrectGuessLogic();
                }
                else
                {
                    BackendHelperLogic.ImplementGuess();
                }
            }
            else
            {
                MainFrontendLogic.GameOverWindow(GameOverWindowState.GetEnumByInt(BackendHelperLogic.GameOverCheck()));
                GameOver = true;
            }
        }

        public static void IncorrectInputTextLogic(ActiveWindowEnum activeWindowEnum)
        {
            string? inputString = Console.ReadLine();
            int inputNumber;
            switch (activeWindowEnum)
            {
                case ActiveWindowEnum.MAIN_WINDOW_ACTIVE:
                    if (!int.TryParse(inputString, out inputNumber) || (inputNumber != 0 && inputNumber != 1))
                    {
                        MainFrontendLogic.WrongInputTextClear();
                        MainFrontendLogic.IncorrectInputText(ActiveWindowEnum.MAIN_WINDOW_ACTIVE);
                    }
                    else
                    {
                        CurrentInput = inputNumber;
                        BackendHelperLogic.MainWindowCorrectInput();
                    }
                    break;
                case ActiveWindowEnum.DIFFICULTY_CHOOSER_WINDOW_ACTIVE:
                    if (!int.TryParse(inputString, out inputNumber) || (inputNumber != 0 && inputNumber != 1))
                    {
                        MainFrontendLogic.WrongInputTextClear();
                        MainFrontendLogic.IncorrectInputText(ActiveWindowEnum.DIFFICULTY_CHOOSER_WINDOW_ACTIVE);
                    }
                    else
                    {
                        CurrentInput = inputNumber;
                        BackendHelperLogic.DifficultyChooserWindowCorrectInput();
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
                (inputGuess.Length > 1 && !BackendHelperLogic.AllowedMultipleCharacters.Any(x => x == inputGuess))
            )
            {
                MainFrontendLogic.WrongGuessFormatTextClear();
                MainFrontendLogic.IncorrectGuessFormatText();
            }
            else
            {
                CurrentGuess = inputGuess;
                BackendHelperLogic.ImplementGuess();
                MainFrontendLogic.GameWindow();
            }
        }
    }
}
