using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanNewVersion.Frontend
{
    public class FrontendLogic
    {
        public static void MainWindow()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the game");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("[1] Play");
            Console.WriteLine("[0] Quit");
            Console.Write("Input: ");
        }

        public static void IncorrectInputText()
        {
            Console.WriteLine("\nWrong input, please try again!");
            Console.Write("Input: ");
        }

        public static void IncorrectGuessFormatText()
        {
            Console.WriteLine("\nWrong guess format, please try again!");
            Console.Write("Guess: ");
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
        }

        public static void GameWindow(
            List<char> displayCharacters,
            int attemptsLeft,
            List<string> incorrectlyGuessedCharacters,
            string drawableHangman
            )
        {
            Console.Clear();
            Console.WriteLine("Guess the word\n");
            Console.WriteLine(string.Join("", displayCharacters) + "\n");
            Console.WriteLine($"Attempts left: {attemptsLeft}");
            Console.Write("Wrong attempts: ");
            for (int i = 0; i < incorrectlyGuessedCharacters.Count; i++)
            {
                if (i == incorrectlyGuessedCharacters.Count - 1)
                {
                    Console.Write(incorrectlyGuessedCharacters[i]);
                }
                else
                {
                    Console.Write(incorrectlyGuessedCharacters[i] + ", ");
                }
            }
            Console.WriteLine(drawableHangman);
            Console.Write("\n\nGuess: ");
        }

        public static void VictoryWindow(List<string> incorrectlyGuessedCharacters)
        {
            Console.WriteLine("\n\nYou've found the word!");
            Console.WriteLine($"\nNumber of guesses used to find the word: {incorrectlyGuessedCharacters.Count}\n");
        }

        public static void DefeatWindow(string guessableWord)
        {
            Console.WriteLine("\n\nYou've been hanged :(");
            Console.WriteLine($"\nThe word you had to guess: {guessableWord}\n");
        }
    }
}
