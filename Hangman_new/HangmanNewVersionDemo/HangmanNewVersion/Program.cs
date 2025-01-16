
using HangmanNewVersion;

string sourcePath = Directory.GetCurrentDirectory() + "\\words.txt";

foreach(var word in TextSource.GetEveryWord(sourcePath))
{
    Console.WriteLine(word);
}
