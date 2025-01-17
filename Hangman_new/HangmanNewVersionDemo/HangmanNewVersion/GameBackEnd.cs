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
            if(!IsInputCorrect(correctInputs)) 
            {
                GameFrontEnd.IncorrectInputText(ActiveWindowEnum.MAIN_WINDOW_ACTIVE);
            }

            MainWindowCorrectInput();
        }

        public static void DifficultyChooserWindowLogic()
        {
            List<int> correctInputs = new List<int> { 0, 1, 2, 3 };
            if (!IsInputCorrect(correctInputs))
            {
                GameFrontEnd.IncorrectInputText(ActiveWindowEnum.DIFFICULTY_CHOOSER_WINDOW_ACTIVE);
            }

            DifficultyChooserWindowCorrectInput();
        }

        public static void GameWindowLogic()
        {
            Console.ReadLine();
        }
        
        public static bool IsInputCorrect(List<int> acceptableInputs)
        {
            string? inputString = Console.ReadLine();
            if (!int.TryParse(inputString, out int inputNumber) || !acceptableInputs.Any(x => x == inputNumber))
            {
                return false;
            }
            CurrentInput = inputNumber;
            return true;
        }

        public static bool IsGuessCorrect()
        {
            string? inputString = Console.ReadLine();
            if (int.TryParse(inputString, out int temp) || inputString == null || inputString.Length > 1)
            {
                return false;
            }
            CurrentGuess = char.Parse(inputString);
            return true;
        }
        public static void MainWindowCorrectInput()
        {
            switch (CurrentInput)
            {
                case 0:
                    GameOver = true;
                    break;
                case 1:
                    GameFrontEnd.DifficultyChooseWindow();
                    break;
            }
        }

        public static void CreateGameWindow(DifficultyEnum? difficulty)
        {
            if (difficulty != null)
            {
                GuessableWord = TextSource.GetGueassableWord(difficulty);
                DisplayCharacters = new List<char>();
                IncorrectlyGuessedCharacters = new List<char>();
                if (GuessableWord != null && DisplayCharacters != null && IncorrectlyGuessedCharacters != null)
                {
                    foreach (var word in GuessableWord)
                    {
                        DisplayCharacters.Add('_');
                    }
                    GameFrontEnd.GameWindow(GuessableWord, AttemptsLeft, DisplayCharacters, IncorrectlyGuessedCharacters);
                }
            }
        }

        public static void DifficultyChooserWindowCorrectInput()
        {
            switch (CurrentInput)
            {
                case 0:
                    GameFrontEnd.MainWindow();
                    break;
                case 1:
                    CreateGameWindow(Difficulty.GetDifficultyEnumByNumber(1));
                    break;
                case 2:
                    CreateGameWindow(Difficulty.GetDifficultyEnumByNumber(2));
                    break;
                case 3:
                    CreateGameWindow(Difficulty.GetDifficultyEnumByNumber(3));
                    break;
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
                        MainWindowCorrectInput();
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
                        DifficultyChooserWindowCorrectInput();
                    }
                    break;
            }
        }
    }
}
