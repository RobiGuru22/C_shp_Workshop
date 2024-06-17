using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman_workshop
{
    public class Game
    {
        public static void Run(int difficulty, string path)
        {
            if(difficulty == -1)
            {
                return;
            }
            int attemptsLeft = 7;
            string word = Reader.GetWord(path, difficulty);
            char[] talalos = new char[word.Length];
            for(int i = 0; i < word.Length; i++)
            {
                talalos[i] = '_';
            }

            List<string> probak = new List<string>();

            int talalatok = 0;
            while (attemptsLeft > 0 && talalatok < word.Length)
            {
                Window.GameWindow(talalos, probak, attemptsLeft, talalatok);
                Console.Write("\nAttempt: ");
                string attempt = Console.ReadLine();
                attempt = attempt.Substring(0, 1);
                List<int> talalatIndexek = new List<int>();
                for(int i = 0; i < word.Length; i++)
                {
                    if (word[i] == Convert.ToChar(attempt))
                    {
                        talalatIndexek.Add(i);
                        talalatok++;
                    }
                }
                if(talalatIndexek.Count > 0)
                {
                    foreach (var item in talalatIndexek)
                    {
                        talalos[item] = Convert.ToChar(attempt);
                    }
                }
                else
                {
                    probak.Add(attempt);
                    attemptsLeft--;
                }
            }
            Window.GameWindow(talalos, probak, attemptsLeft, talalatok);
            Window.EndWindow(attemptsLeft);
        } 
    }
}
