using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanNewVersion
{
    public class TextSource
    {
        public static List<string> GetEveryWord(string sourcePath)
        {
            return File.ReadAllLines(sourcePath).ToList();
        }
    }
}
