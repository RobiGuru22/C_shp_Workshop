using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangmanNewVersion.States;


namespace HangmanNewVersion.Backend
{
    public class TextSource
    {
        public static string SourcePath { get; set; }
        public static List<string> GetEveryWord()
        {
            return File.ReadAllLines(SourcePath).ToList();
        }

        public static string? GetGueassableWord(DifficultyEnum? difficulty)
        {
            Random rnd = new Random();
            List<string> everyWord = GetEveryWord();
            List<string> possibleWords = new List<string>();
            switch (difficulty)
            {
                case DifficultyEnum.EASY:
                    foreach (var word in everyWord)
                    {
                        if (word.Length > 0 && word.Length <= 5)
                        {
                            possibleWords.Add(word);
                        }
                    }
                    return possibleWords[rnd.Next(0, possibleWords.Count - 1)];
                case DifficultyEnum.MEDIUM:
                    foreach (var word in everyWord)
                    {
                        if (word.Length > 5 && word.Length <= 10)
                        {
                            possibleWords.Add(word);
                        }
                    }
                    return possibleWords[rnd.Next(0, possibleWords.Count - 1)];
                case DifficultyEnum.HARD:
                    foreach (var word in everyWord)
                    {
                        if (word.Length > 10)
                        {
                            possibleWords.Add(word);
                        }
                    }
                    return possibleWords[rnd.Next(0, possibleWords.Count - 1)];
                default:
                    return null;

            }
        }
    }
}
