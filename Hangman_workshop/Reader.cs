using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman_workshop
{
    public class Reader
    {
        enum Level
        {
            Easy,
            Medium,
            Hard
        }
        private static Level GetDifficulty(int difficultyNumber)
        {
            if (difficultyNumber == 1) 
            {
                return Level.Easy;
            }
            else if (difficultyNumber == 2) 
            {
                return Level.Medium;
            }
            else 
            {
                return Level.Hard; 
            }
        }
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

            Level enumDifficulty = GetDifficulty(difficulty);

            switch(enumDifficulty)
            {
                case Level.Easy:
                    return EasyDifficulty(words);
                case Level.Medium:
                    return MediumDifficulty(words);
                case Level.Hard:
                    return HardDifficulty(words);
                default:
                    return null;
            }
        }
    }
}
