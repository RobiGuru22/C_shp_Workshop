using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman_workshop
{
    public class Game
    {
        private static int Attempt(string attempt, string searchedWord, char[] inGameWord, List<string> triedCharList, int attemptsLeftCounter)
        {
            if (searchedWord.Contains(attempt))
            {
                int i = 0;
                while ((i = searchedWord.IndexOf(attempt, i)) != -1)
                {
                    inGameWord[i] = Convert.ToChar(attempt);
                    i++;
                }
            }
            else
            {
                triedCharList.Add(attempt);
                attemptsLeftCounter--;
            }
            return attemptsLeftCounter;
        }
        public static void Run(int difficulty, string path)
        {
            int attemptsLeftCounter = 7;
            string searchedWord = Reader.GetWord(path, difficulty);
            char[] inGameWord = new string('_', searchedWord.Length).ToCharArray();

            List<string> triedCharList = new List<string>();

            int successfulAttemptsCounter = 0;
            while (attemptsLeftCounter > 0 && successfulAttemptsCounter < searchedWord.Length)
            {
                string attempt = Window.GameWindow(inGameWord, triedCharList, attemptsLeftCounter);
                attemptsLeftCounter = Attempt(attempt, searchedWord, inGameWord, triedCharList, attemptsLeftCounter);
            }
            Window.GameWindow(inGameWord, triedCharList, attemptsLeftCounter);
            Window.EndWindow(attemptsLeftCounter);
        } 
    }
}
