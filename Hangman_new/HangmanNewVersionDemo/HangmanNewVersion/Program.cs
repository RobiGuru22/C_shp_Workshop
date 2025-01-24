using HangmanBackendLibrary;
using HangmanFrontendLibrary;

TextSource.SourcePath = Directory.GetCurrentDirectory() + "\\words.txt";

while(!MainBackendLogic.GameOver)
{
    MainFrontendLogic.MainWindow();
}

