using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman_workshop
{
    public class Window
    {
        public static int MainWindow()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Ultimate, TopG, SuperPowerGivin Hangman Game!\n");
            Console.WriteLine("Please choose an option:\n\n");
            Console.WriteLine("[1]Start game");
            Console.WriteLine("[0]Quit\n");
            Console.Write("Input: ");
            int input;
            try
            {
                input = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                input = -1;
            }
            switch (input) 
            {
                case 1:
                    return DifficultyWindow();
                case 0:
                    return -1;
                case -1:
                    Console.WriteLine("\nInvalid input, please try again...");
                    Thread.Sleep(2000);
                    MainWindow();
                    return 0;
                default:
                    Console.WriteLine("\nInvalid input, please try again...");
                    Thread.Sleep(2000);
                    MainWindow();
                    return 0;
            }
        }
        public static int DifficultyWindow()
        {
            Console.Clear();
            Console.WriteLine("Please choose a difficulty:\n");
            Console.WriteLine("[1]Easy - 1-5 characters");
            Console.WriteLine("[2]Medium - 5-10 characters");
            Console.WriteLine("[3]Hard - more than 10 characters\n");
            Console.WriteLine("[0]Back to main menu\n");
            Console.Write("Input: ");

            int input;

            try
            {
                input = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                input = -1;
            }

            switch (input)
            {
                case 1:
                    return input;
                case 2:
                    return input;
                case 3:
                    return input;
                case 0:
                    MainWindow();
                    return 0;
                case -1:
                    Console.WriteLine("\nInvalid input, please try again...");
                    Thread.Sleep(2000);
                    DifficultyWindow();
                    return 0;
                default:
                    Console.WriteLine("\nInvalid input, please try again...");
                    Thread.Sleep(2000);
                    DifficultyWindow();
                    return 0;
            }
        }
        public static void GameWindow(char[] talalos, List<string> probak, int attemptsLeft, int talalatok)
        {
            Console.Clear();
            Console.WriteLine("What could the word be?\n\n");
            Console.WriteLine(new string(talalos) + "\n");
            Console.Write("Previous attempts: ");
            foreach (var item in probak)
            {
                Console.Write(item + ' ');
            }
            Console.WriteLine($"\nAttempts left: {attemptsLeft}");
        }
        public static void EndWindow(int attemptsLeft)
        {
            if (attemptsLeft > 0) //win
            {
                Console.WriteLine("\nSuccess!!");
                Console.WriteLine("\nWhy not another one:");
                Console.WriteLine("[1]Why not");
                Console.WriteLine("[0]Hell nah");
                Console.Write("\nInput: ");

                int input;
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    input = -1;
                }
                switch (input)
                {
                    case 1:
                        DifficultyWindow();
                        break;
                    case 0:
                        MainWindow();
                        break;
                    case -1:
                        Console.WriteLine("\nInvalid input, please try again...");
                        Thread.Sleep(2000);
                        Console.Clear();
                        EndWindow(attemptsLeft);
                        break;
                    default:
                        Console.WriteLine("\nInvalid input, please try again...");
                        Thread.Sleep(2000);
                        Console.Clear();
                        EndWindow(attemptsLeft);
                        break;
                }
            }
            else //lose
            {
                Console.WriteLine("\nOopsie, you have run out of attempts :c");
                Console.WriteLine("\nHow about another one:");
                Console.WriteLine("[1]Why not");
                Console.WriteLine("[0]Hell nah");

                Console.Write("\nInput: ");

                int input;
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    input = -1;
                }
                switch (input)
                {
                    case 1:
                        DifficultyWindow();
                        break;
                    case 0:
                        MainWindow();
                        break;
                    case -1:
                        Console.WriteLine("\nInvalid input, please try again...");
                        Thread.Sleep(2000);
                        Console.Clear();
                        EndWindow(attemptsLeft);
                        break;
                    default:
                        Console.WriteLine("\nInvalid input, please try again...");
                        Thread.Sleep(2000);
                        Console.Clear();
                        EndWindow(attemptsLeft);
                        break;
                }
            }
        }
    }
}
