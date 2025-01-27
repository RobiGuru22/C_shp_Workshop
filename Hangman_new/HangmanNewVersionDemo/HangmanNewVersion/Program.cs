
using HangmanNewVersion;

string txtSourcePath = Directory.GetCurrentDirectory() + "\\words.txt";

GameFlow.txtSourcePath = txtSourcePath;

while(!GameFlow.gameOver)
{
    GameFlow.StartGame();
}

