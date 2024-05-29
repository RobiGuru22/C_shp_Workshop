using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman_workshop
{
    public class Reader
    {
        public static string GetWord(string path, int difficulty)
        {
            List<string> words = File.ReadAllLines(path).ToList();
            Random rnd = new Random();
            if(difficulty == 1)
            {
                List<string> rightWords = words.Where(x => x.Length <= 5).ToList();
                return rightWords[rnd.Next(0, rightWords.Count())];
            }
            else if (difficulty == 2)
            {
                List<string> rightWords = words.Where(x => x.Length > 5 && x.Length <= 10).ToList();
                return rightWords[rnd.Next(0, rightWords.Count())];
            }
            else
            {
                List<string> rightWords = words.Where(x => x.Length > 10).ToList();
                return rightWords[rnd.Next(0, rightWords.Count())];
            }
        }
    }
}
