
using HangmanNewVersion;

TextSource.SourcePath = Directory.GetCurrentDirectory() + "\\words.txt";

while(!GameBackEnd.GameOver)
{
    GameFrontEnd.MainWindow();
}

