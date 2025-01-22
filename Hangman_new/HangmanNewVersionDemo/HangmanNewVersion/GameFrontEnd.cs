using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanNewVersion
{
    public class GameFrontEnd
    {
        public static void MainWindow()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the game");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("[1] Play");
            Console.WriteLine("[0] Quit");
            Console.Write("Input: ");
            GameBackEnd.MainWindowLogic();
        }

        public static void IncorrectInputText(ActiveWindowEnum activeWindowEnum)
        {
            Console.WriteLine("\nWrong input, please try again!");
            Console.Write("Input: ");
            GameBackEnd.IncorrectInputTextLogic(activeWindowEnum);
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
            GameBackEnd.IncorrectGuessFormatTextLogic();
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
            GameBackEnd.DifficultyChooserWindowLogic();
        }

        public static void GameWindow()
        {
            Console.Clear();
            Console.WriteLine("Guess the word\n");
            Console.WriteLine(string.Join("", GameBackEnd.DisplayCharacters) + "\n");
            //Console.WriteLine(GameBackEnd.GuessableWord);
            Console.WriteLine($"Attempts left: {GameBackEnd.AttemptsLeft}");
            Console.Write("Wrong attempts: ");
            for (int i = 0; i < GameBackEnd.IncorrectlyGuessedCharacters.Count; i++)
            {
                if (i == GameBackEnd.IncorrectlyGuessedCharacters.Count - 1)
                {
                    Console.Write(GameBackEnd.IncorrectlyGuessedCharacters[i]);
                }
                else
                {
                    Console.Write(GameBackEnd.IncorrectlyGuessedCharacters[i] + ", ");
                }
            }
            Console.WriteLine(GameBackendHelper.GetCurrentHangmanDrawingByAttemptsLeft());
            Console.Write("\n\nGuess: ");
            GameBackEnd.GameWindowLogic();
        }

        public static void GameOverWindow(GameOverEnum gameOverWindow)
        {
            switch(gameOverWindow)
            {
                case GameOverEnum.WIN:
                    Console.WriteLine("\n\nYou've found the word!");
                    Console.WriteLine($"\nNumber of guesses used to find the word: {GameBackEnd.IncorrectlyGuessedCharacters.Count}\n");
                    break;
                case GameOverEnum.LOSE:
                    Console.WriteLine("\n\nYou've been hanged :(");
                    Console.WriteLine($"\nThe word you had to guess: {GameBackEnd.GuessableWord}\n");
                    break;
            }
        }
    }
}
