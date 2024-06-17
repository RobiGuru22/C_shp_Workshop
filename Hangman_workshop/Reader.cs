using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman_workshop
{
    public class Reader
    {
        private static string EasyDifficulty(List<string> words)
        {
            Random rnd = new Random();
            List<string> rightWords = words.Where(x => x.Length <= 5).ToList();
            return rightWords[rnd.Next(0, rightWords.Count())];
        }
        private static string MediumDifficulty(List<string> words)
        {
            Random rnd = new Random();
            List<string> rightWords = words.Where(x => x.Length > 5 && x.Length <= 10).ToList();
            return rightWords[rnd.Next(0, rightWords.Count())];
        }
        private static string HardDifficulty(List<string> words)
        {
            Random rnd = new Random();
            List<string> rightWords = words.Where(x => x.Length > 10).ToList();
            return rightWords[rnd.Next(0, rightWords.Count())];
        }
        public static string GetWord(string path, int difficulty)
        {
            List<string> words = File.ReadAllLines(path).ToList();
            
            if(difficulty == 1)
            {
                return EasyDifficulty(words);
            }
            else if (difficulty == 2)
            {
                return MediumDifficulty(words);
            }
            else
            {
                return HardDifficulty(words);
            }
        }
    }
}
