using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangmanNewVersion.States;

namespace HangmanNewVersion.Backend
{
    public class BackendLogic
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

        public static int MainWindowLogic()
        {
            List<int> correctInputs = new List<int> { 0, 1 };
            if (!UserInputChecker.IsInputCorrect(correctInputs))
            {
                //MainFrontendLogic.IncorrectInputText(ActiveWindowEnum.MAIN_WINDOW_ACTIVE);
                return -1;
            }

            //BackendHelper.MainWindowCorrectInput();
            return 0;
        }

        public static int DifficultyChooserWindowLogic()
        {
            List<int> correctInputs = new List<int> { 0, 1, 2, 3 };
            if (!UserInputChecker.IsInputCorrect(correctInputs))
            {
                //MainFrontendLogic.IncorrectInputText(ActiveWindowEnum.DIFFICULTY_CHOOSER_WINDOW_ACTIVE);
                return -1;
            }
            //BackendHelper.DifficultyChooserWindowCorrectInput();
            return 0;
        }

        public static int GameWindowLogic()
        {
            if (BackendHelper.GameOverCheck() > 0)
            {
                if (!UserInputChecker.IsGuessFormatCorrect())
                {
                    return 1;
                    //MainFrontendLogic.IncorrectGuessFormatText();
                }
                else if (!UserInputChecker.IsGuessCorrect())
                {
                    return 2;
                    //BackendHelper.IncorrectGuessLogic();
                }
                else
                {
                    return 3;
                    //BackendHelper.ImplementGuess();
                }
            }
            else
            {
                //MainFrontendLogic.GameOverWindow(GameOverWindowState.GetEnumByInt(BackendHelper.GameOverCheck()));
                //GameOver = true;
                return BackendHelper.GameOverCheck();
            }
        }

        public static int IncorrectMainWindowInputLogic()
        {
            string? inputString = Console.ReadLine();
            int inputNumber;
            if (!int.TryParse(inputString, out inputNumber) || (inputNumber != 0 && inputNumber != 1))
            {
                //MainFrontendLogic.WrongInputTextClear();
                //MainFrontendLogic.IncorrectInputText(ActiveWindowEnum.MAIN_WINDOW_ACTIVE);
                return -1;
            }
            else
            {
                CurrentInput = inputNumber;
                //BackendHelper.MainWindowCorrectInput();
                return 0;
            }
        }

        public static int IncorrectDifficultyChooseWindowInputLogic()
        {
            string? inputString = Console.ReadLine();
            int inputNumber;

            if (!int.TryParse(inputString, out inputNumber) || (inputNumber != 0 && inputNumber != 1))
            {
                //MainFrontendLogic.WrongInputTextClear();
                //MainFrontendLogic.IncorrectInputText(ActiveWindowEnum.DIFFICULTY_CHOOSER_WINDOW_ACTIVE);
                return -1;
            }
            else
            {
                CurrentInput = inputNumber;
                //BackendHelper.DifficultyChooserWindowCorrectInput();
                return 0;
            }
        }

        //public static void IncorrectInputTextLogic(ActiveWindowEnum activeWindowEnum)
        //{
        //    string? inputString = Console.ReadLine();
        //    int inputNumber;
        //    switch (activeWindowEnum)
        //    {
        //        case ActiveWindowEnum.MAIN_WINDOW_ACTIVE:
        //            if (!int.TryParse(inputString, out inputNumber) || (inputNumber != 0 && inputNumber != 1))
        //            {
        //                //MainFrontendLogic.WrongInputTextClear();
        //                //MainFrontendLogic.IncorrectInputText(ActiveWindowEnum.MAIN_WINDOW_ACTIVE);
        //            }
        //            else
        //            {
        //                CurrentInput = inputNumber;
        //                BackendHelper.MainWindowCorrectInput();
        //            }
        //            break;
        //        case ActiveWindowEnum.DIFFICULTY_CHOOSER_WINDOW_ACTIVE:
        //            if (!int.TryParse(inputString, out inputNumber) || (inputNumber != 0 && inputNumber != 1))
        //            {
        //                //MainFrontendLogic.WrongInputTextClear();
        //                //MainFrontendLogic.IncorrectInputText(ActiveWindowEnum.DIFFICULTY_CHOOSER_WINDOW_ACTIVE);
        //            }
        //            else
        //            {
        //                CurrentInput = inputNumber;
        //                BackendHelper.DifficultyChooserWindowCorrectInput();
        //            }
        //            break;
        //    }
        //}

        public static int IncorrectGuessFormatTextLogic()
        {
            string? inputGuess = Console.ReadLine();

            if (
                inputGuess == null ||
                GuessableWord == null ||
                int.TryParse(inputGuess, out int temp) ||
                (inputGuess.Length > 1 && !BackendHelper.AllowedMultipleCharacters.Any(x => x == inputGuess))
            )
            {
                //MainFrontendLogic.WrongGuessFormatTextClear();
                //MainFrontendLogic.IncorrectGuessFormatText();
                return -1;
            }
            else
            {
                CurrentGuess = inputGuess;
                //BackendHelper.ImplementGuess();
                //MainFrontendLogic.GameWindow();
                return 0;
            }
        }
    }
}
