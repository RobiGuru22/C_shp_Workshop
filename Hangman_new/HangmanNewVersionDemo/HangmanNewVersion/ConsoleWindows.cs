﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanNewVersion
{
    public class ConsoleWindows
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
            Console.WriteLine("Wrong input, please try again!");
            Console.Write("Input: ");
        }

        public static void DifficultyChooseWindow()
        {
            Console.Clear();
            Console.WriteLine("Choose diffculty:");
            Console.WriteLine("[1] Easy (1 - 5 word)");
            Console.WriteLine("[2] Medium (6 - 10 word)");
            Console.WriteLine("[3] Hard (10+ word)");
            Console.WriteLine("[0] Back to main menu");
        }

        public static void GameWindow(string guessableWord, int attemptsLeft, char[] foundCharacters, List<char> incorrectCharacters)
        {
            Console.Clear();
            Console.WriteLine("Guess the word\n");
            Console.WriteLine(foundCharacters + "\n");
            Console.Write("Wrong attempts: ");
            foreach(var c in incorrectCharacters)
            {
                Console.Write(c + " ");
            }
        }
    }
}
