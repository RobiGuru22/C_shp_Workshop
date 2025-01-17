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

        public static void IncorrectGuessText()
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
            GameBackEnd.DifficultyChooserWindowLogic();
        }

        public static void GameWindow(string guessableWord, int attemptsLeft, char[] displayCharacters, List<char> incorrectCharacters)
        {
            Console.Clear();
            Console.WriteLine("Guess the word\n");
            Console.WriteLine(displayCharacters + "\n");
            Console.Write("Wrong attempts: ");
            for(int i = 0; i < incorrectCharacters.Count; i++)
            {
                if(i == incorrectCharacters.Count - 1)
                {
                    Console.Write(incorrectCharacters[i]);
                }
                else
                {
                    Console.Write(incorrectCharacters[i]+ ", ");
                }
            }
            Console.Write("\n\nGuess: ");
        }
    }
}
