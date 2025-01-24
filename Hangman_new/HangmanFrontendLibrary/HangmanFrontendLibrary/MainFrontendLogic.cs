using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangmanBackendLibrary;

namespace HangmanFrontendLibrary
{
    public class MainFrontendLogic
    {
        public static void MainWindow()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the game");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("[1] Play");
            Console.WriteLine("[0] Quit");
            Console.Write("Input: ");
            MainBackendLogic.MainWindowLogic();
        }

        public static void IncorrectInputText(ActiveWindowEnum activeWindowEnum)
        {
            Console.WriteLine("\nWrong input, please try again!");
            Console.Write("Input: ");
            MainBackendLogic.IncorrectInputTextLogic(activeWindowEnum);
        }

        public static void WrongInputTextClear()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 2);
        }

        public static void IncorrectGuessFormatText()
        {
            Console.WriteLine("\nWrong guess format, please try again!");
            Console.Write("Guess: ");
            MainBackendLogic.IncorrectGuessFormatTextLogic();
        }

        public static void WrongGuessFormatTextClear()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 2);
        }

        public static void DifficultyChooseWindow()
        {
            Console.Clear();
            Console.WriteLine("Choose diffculty:");
            Console.WriteLine("[1] Easy (1 - 5 word)");
            Console.WriteLine("[2] Medium (6 - 10 word)");
            Console.WriteLine("[3] Hard (10+ word)");
            Console.WriteLine("[0] Back to main menu");
            Console.Write("Input: ");
            MainBackendLogic.DifficultyChooserWindowLogic();
        }

        public static void GameWindow()
        {
            Console.Clear();
            Console.WriteLine("Guess the word\n");
            Console.WriteLine(string.Join("", MainBackendLogic.DisplayCharacters) + "\n");
            //Console.WriteLine(MainBackendLogic.GuessableWord);
            Console.WriteLine($"Attempts left: {MainBackendLogic.AttemptsLeft}");
            Console.Write("Wrong attempts: ");
            for (int i = 0; i < MainBackendLogic.IncorrectlyGuessedCharacters.Count; i++)
            {
                if (i == MainBackendLogic.IncorrectlyGuessedCharacters.Count - 1)
                {
                    Console.Write(MainBackendLogic.IncorrectlyGuessedCharacters[i]);
                }
                else
                {
                    Console.Write(MainBackendLogic.IncorrectlyGuessedCharacters[i] + ", ");
                }
            }
            Console.WriteLine(BackendHelperLogic.GetCurrentHangmanDrawingByAttemptsLeft());
            Console.Write("\n\nGuess: ");
            MainBackendLogic.GameWindowLogic();
        }

        public static void GameOverWindow(GameOverEnum gameOverWindow)
        {
            switch (gameOverWindow)
            {
                case GameOverEnum.WIN:
                    Console.WriteLine("\n\nYou've found the word!");
                    Console.WriteLine($"\nNumber of guesses used to find the word: {MainBackendLogic.IncorrectlyGuessedCharacters.Count}\n");
                    break;
                case GameOverEnum.LOSE:
                    Console.WriteLine("\n\nYou've been hanged :(");
                    Console.WriteLine($"\nThe word you had to guess: {MainBackendLogic.GuessableWord}\n");
                    break;
            }
        }
    }
}
