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

        public static MainWindowLogicStateEnum MainWindowLogic()
        {
            List<int> correctInputs = new List<int> { 0, 1 };
            if (!UserInputChecker.IsInputCorrect(correctInputs))
            {
                return MainWindowLogicStateEnum.INCORRECT_INPUT;
            }

            return MainWindowLogicStateEnum.CORRECT_INPUT;
        }

        public static DifficultyChooserWindowLogicStateEnum DifficultyChooserWindowLogic()
        {
            List<int> correctInputs = new List<int> { 0, 1, 2, 3 };
            if (!UserInputChecker.IsInputCorrect(correctInputs))
            {
                return DifficultyChooserWindowLogicStateEnum.INCORRECT_DIFFICULTY;
            }
            return DifficultyChooserWindowLogicStateEnum.CORRECT_DIFFICULTY;
        }

        public static GameWindowLogicStateEnum GameWindowLogic()
        {
            if (BackendHelper.GameOverCheck() == GameOverEnum.DEFAULT)
            {
                if (!UserInputChecker.IsGuessFormatCorrect())
                {
                    return GameWindowLogicStateEnum.INCORRECT_GUESS_FORMAT;
                }
                else
                {
                    return GameWindowLogicStateEnum.IMPLEMENTABLE_GUESS;
                }
            }
            else
            {
                return GameWindowLogicStateEnum.GAMEOVER;
            }
        }

        public static IncorrectInputMainWindowLogicStateEnum IncorrectMainWindowInputLogic()
        {
            string? inputString = Console.ReadLine();
            int inputNumber;
            if (!int.TryParse(inputString, out inputNumber) || (inputNumber != 0 && inputNumber != 1))
            {
                return IncorrectInputMainWindowLogicStateEnum.INCORRECT_INPUT;
            }
            else
            {
                CurrentInput = inputNumber;
                return IncorrectInputMainWindowLogicStateEnum.CORRECT_INPUT;
            }
        }

        public static IncorrectDifficultyChooseWindowInputLogicStateEnum IncorrectDifficultyChooseWindowInputLogic()
        {
            string? inputString = Console.ReadLine();
            int inputNumber;

            if (!int.TryParse(inputString, out inputNumber) || (inputNumber != 0 && inputNumber != 1))
            {
                return IncorrectDifficultyChooseWindowInputLogicStateEnum.INCORRECT_DIFFICULTY_INPUT;
            }
            else
            {
                CurrentInput = inputNumber;
                return IncorrectDifficultyChooseWindowInputLogicStateEnum.CORRECT_DIFFICULTY_INPUT;
            }
        }

        public static IncorrectGuessFormatLogicStateEnum IncorrectGuessFormatTextLogic()
        {
            string? inputGuess = Console.ReadLine();

            if (
                inputGuess == null ||
                GuessableWord == null ||
                int.TryParse(inputGuess, out int temp) ||
                (inputGuess.Length > 1 && !BackendHelper.AllowedMultipleCharacters.Any(x => x == inputGuess))
            )
            {
                return IncorrectGuessFormatLogicStateEnum.INCORRECT_GUESS;
            }
            else
            {
                CurrentGuess = inputGuess;
                return IncorrectGuessFormatLogicStateEnum.CORRECT_GUESS;
            }
        }
    }
}
