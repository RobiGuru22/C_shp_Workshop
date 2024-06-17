using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman_workshop;
    public class Window
    {
        private static void InvalidInputText()
        {
            Console.WriteLine("\nInvalid input, please try again...");
            Thread.Sleep(2000);
        }
        private static void EndingScene(int attemptsLeftCounter)
        {
            Console.WriteLine("\nHow about another one:");
            Console.WriteLine("[1]Why not");
            Console.WriteLine("[0]Hell nah");

            Console.Write("\nInput: ");

            if (int.TryParse(Console.ReadLine(), out int input))
            {
                if (input == 1)
                {
                    DifficultyWindow();
                }
                else if (input == 0)
                {
                    MainWindow();
                }
                else
                {
                    InvalidInputText();
                    Console.Clear();
                    EndWindow(attemptsLeftCounter);
                }
            }
            else
            {
                InvalidInputText();
                Console.Clear();
                EndWindow(attemptsLeftCounter); ;
            }
        }
        
        private static void VictoryText()
        {
            Console.WriteLine("\nSuccess!!");
        }
        private static void LoseText()
        {
            Console.WriteLine("\nOopsie, you have run out of attempts :c");
        }
        public static int MainWindow()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Ultimate, TopG, SuperPowerGivin Hangman Game!\n");
            Console.WriteLine("Please choose an option:\n\n");
            Console.WriteLine("[1]Start game");
            Console.WriteLine("[0]Quit\n");
            Console.Write("Input: ");
            if (int.TryParse(Console.ReadLine(), out int input))
            {
                if (input == 1)
                {
                    DifficultyWindow();
                    return 0;
                }
                else if (input == 0)
                {
                    return -1;
                }
                else
                {
                    InvalidInputText();
                    MainWindow();
                    return 0;
                }
            }
            else
            {
                InvalidInputText();
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

            if(int.TryParse(Console.ReadLine(), out int input))
            {
                if(1 <= input && input <= 3)
                {
                    return input;
                }
                else if(input == 0)
                {
                    MainWindow();
                    return 0;
                }
                else
                {
                    InvalidInputText();
                    DifficultyWindow();
                    return 0;
                }
            }
            else
            {
                InvalidInputText();
                DifficultyWindow();
                return 0;
            }
        }
        public static string GameWindow(char[] inGameWord, List<string> triedCharList, int attemptsLeftCounter)
        {
            Console.Clear();
            Console.WriteLine("What could the word be?\n\n");
            Console.WriteLine(new string(inGameWord) + "\n");
            Console.Write("Previous attempts: ");
            foreach (var item in triedCharList)
            {
                Console.Write(item + ' ');
            }
            Console.WriteLine($"\nAttempts left: {attemptsLeftCounter}");
            Console.Write("\nAttempt: ");
            return Console.ReadLine().Substring(0, 1);
        }
        public static void EndWindow(int attemptsLeftCounter)
        {
            if (attemptsLeftCounter > 0)
            {
                VictoryText();
            }
            else 
            {
                LoseText();
            }
            EndingScene(attemptsLeftCounter);
        }
    }
