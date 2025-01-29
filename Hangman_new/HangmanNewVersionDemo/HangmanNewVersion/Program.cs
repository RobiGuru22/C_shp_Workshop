
using HangmanNewVersion;
using HangmanNewVersion.Backend;

TextSource.SourcePath = Directory.GetCurrentDirectory() + "\\test_words.txt";

while(!BackendLogic.GameOver)
{
    GameFlow.StartGame();
}

