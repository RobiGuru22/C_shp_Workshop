
using HangmanNewVersion;
using HangmanNewVersion.Backend;

TextSource.SourcePath = Directory.GetCurrentDirectory() + "\\words.txt";

while(!BackendLogic.GameOver)
{
    GameFlow.StartGame();
}

