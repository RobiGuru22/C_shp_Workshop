using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanNewVersion
{
    public class GameBackendHelper
    {
        public static void MainWindowCorrectInput()
        {
            switch (GameBackEnd.CurrentInput)
            {
                case 0:
                    GameBackEnd.GameOver = true;
                    break;
                case 1:
                    GameFrontEnd.DifficultyChooseWindow();
                    break;
            }
        }

        public static void DifficultyChooserWindowCorrectInput()
        {
            switch (GameBackEnd.CurrentInput)
            {
                case 0:
                    GameFrontEnd.MainWindow();
                    break;
                case 1:
                    CreateGameWindow(Difficulty.GetDifficultyEnumByNumber(1));
                    break;
                case 2:
                    CreateGameWindow(Difficulty.GetDifficultyEnumByNumber(2));
                    break;
                case 3:
                    CreateGameWindow(Difficulty.GetDifficultyEnumByNumber(3));
                    break;
            }
        }

        public static void CreateGameWindow(DifficultyEnum? difficulty)
        {
            if (difficulty != null)
            {
                List<char> allowedSpecialCharacters = new List<char>
                {
                    '-',
                    '\'',
                    '/',
                    '*',
                    '\\',
                    ',',
                    '.'
                };
                GameBackEnd.GuessableWord = TextSource.GetGueassableWord(difficulty);
                GameBackEnd.DisplayCharacters = new List<char>();
                GameBackEnd.IncorrectlyGuessedCharacters = new List<char>();
                if (
                    GameBackEnd.GuessableWord != null &&
                    GameBackEnd.DisplayCharacters != null &&
                    GameBackEnd.IncorrectlyGuessedCharacters != null
                    )
                {
                    foreach (var word in GameBackEnd.GuessableWord)
                    {
                        if(allowedSpecialCharacters.Any(x => x == word))
                        {
                            GameBackEnd.DisplayCharacters.Add(word);
                        }
                        else
                        {
                            GameBackEnd.DisplayCharacters.Add('_');
                        }
                    }
                    GameFrontEnd.GameWindow(
                        GameBackEnd.GuessableWord, 
                        GameBackEnd.AttemptsLeft, 
                        GameBackEnd.DisplayCharacters, 
                        GameBackEnd.IncorrectlyGuessedCharacters
                        );
                }
            }
        }
    }
}
