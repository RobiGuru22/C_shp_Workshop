using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanNewVersion
{
    public class GameBackendHelper
    {
        public static List<char> AllowedSpecialCharacters = new List<char>
                {
                    '-',
                    '\'',
                    '/',
                    '*',
                    '\\',
                    ',',
                    '.',
                    '='
                };

        public static List<string> AllowedMultipleCharacters = new List<string>
            {
                "cs",
                "dz",
                "dzs",
                "gy",
                "ly",
                "sz",
                "ty"
            };

        public static bool AllowedMultipleCharactersDetected(string guessableWord)
        {
            foreach (var w in AllowedMultipleCharacters)
            {
                if(guessableWord.Contains(w))
                {
                    return true;
                }
            }
            return false;
        }

        public static List<string> GetAllowedMultipleCharactersFromGuessableWord(string guessableWord)
        {
            List<string> allowedMultipleCharactersInWord = new List<string>();
            bool dzsFound = false;
            foreach (var w in AllowedMultipleCharacters)
            {
                if (guessableWord.Contains(w) && w != "dz" && w != "dzs")
                {
                    allowedMultipleCharactersInWord.Add(w);
                }
                else if (w == "dz")
                {
                    for (int i = 0; i < guessableWord.Length; i++)
                    {
                        if (i + 2 < guessableWord.Length && guessableWord[i] == 'd' && guessableWord[i + 1] == 'z' && guessableWord[i + 2] == 's')
                        {
                            allowedMultipleCharactersInWord.Add("dzs");
                            dzsFound = true;
                            break;
                        }
                    }
                    if(!dzsFound)
                    {
                        allowedMultipleCharactersInWord.Add("dz");
                    }
                }
            }

            return allowedMultipleCharactersInWord;
        }

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
                GameBackEnd.GuessableWord = TextSource.GetGueassableWord(difficulty);
                GameBackEnd.ActualCharacters = new List<char>();
                GameBackEnd.DisplayCharacters = new List<char>();
                GameBackEnd.IncorrectlyGuessedCharacters = new List<string>();
                if (
                    GameBackEnd.GuessableWord != null &&
                    GameBackEnd.DisplayCharacters != null &&
                    GameBackEnd.IncorrectlyGuessedCharacters != null
                    )
                {
                    /*if(AllowedMultipleCharactersDetected(GameBackEnd.GuessableWord))
                    {
                        GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords = GetAllowedMultipleCharactersFromGuessableWord(GameBackEnd.GuessableWord);
                        int indexSkip = 0;
                        bool multipleCharactersDetectedAtIndex = false;
                        for(int i = 0; i < GameBackEnd.GuessableWord.Length; i++)
                        {
                            if (AllowedSpecialCharacters.Any(x => x == GameBackEnd.GuessableWord[i]))
                            {
                                GameBackEnd.DisplayCharacters[i] = GameBackEnd.GuessableWord[i];
                                if(i + 1 != GameBackEnd.GuessableWord.Length)
                                {
                                    GameBackEnd.DisplayCharacters.Add(' ');
                                }
                            }
                            else
                            {
                                for (int j = 0; j < GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords.Count; j++)
                                {
                                    if ((i + GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords[j].Length) < GameBackEnd.GuessableWord.Length)
                                    {
                                        for (int k = 0; k < GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords[j].Length; k++)
                                        {

                                            if (GameBackEnd.GuessableWord[i + k] == GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords[j][k])
                                            {
                                                multipleCharactersDetectedAtIndex = true;
                                            }
                                            else
                                            {
                                                multipleCharactersDetectedAtIndex = false;
                                            }
                                        }
                                    }
                                    if (multipleCharactersDetectedAtIndex)
                                    {
                                        indexSkip = GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords[j].Length;
                                        break;
                                    }
                                }
                                if (indexSkip > 0)
                                {
                                    int temp = 0;
                                    while(temp < indexSkip)
                                    {
                                        GameBackEnd.DisplayCharacters.Add('_');
                                        temp++;
                                    }
                                    if (i + 1 != GameBackEnd.GuessableWord.Length)
                                    {
                                        GameBackEnd.DisplayCharacters.Add(' ');
                                    }
                                    i += indexSkip - 1;
                                }
                                else
                                {
                                    GameBackEnd.DisplayCharacters.Add('_');
                                    if (i + 1 != GameBackEnd.GuessableWord.Length)
                                    {
                                        GameBackEnd.DisplayCharacters.Add(' ');
                                    }
                                }
                                multipleCharactersDetectedAtIndex = false;
                                indexSkip = 0;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < GameBackEnd.GuessableWord.Length; i++)
                        {
                            if (AllowedSpecialCharacters.Any(x => x == GameBackEnd.GuessableWord[i]))
                            {
                                GameBackEnd.DisplayCharacters.Add(GameBackEnd.GuessableWord[i]);
                            }
                            else
                            {
                                GameBackEnd.DisplayCharacters.Add('_');
                            }
                            if (i + 1 != GameBackEnd.GuessableWord.Length)
                            {
                                GameBackEnd.DisplayCharacters.Add(' ');
                            }
                        }
                    }
                    foreach(var c in GameBackEnd.DisplayCharacters)
                    {
                        if(c != ' ')
                        {
                            GameBackEnd.ActualCharacters.Add(c);
                        }
                    }*/

                    foreach(var c in GameBackEnd.GuessableWord)
                    {
                        GameBackEnd.ActualCharacters.Add('_');
                    }

                    ActualListToDisplayListConersion(GameBackEnd.ActualCharacters);

                    GameFrontEnd.GameWindow(
                        GameBackEnd.GuessableWord, 
                        GameBackEnd.AttemptsLeft, 
                        GameBackEnd.DisplayCharacters, 
                        GameBackEnd.IncorrectlyGuessedCharacters
                        );
                }
            }
        }

        public static void ActualListToDisplayListConersion(List<char> actualList)
        {
            List<char> newDipslayList = new List<char>();
            if(AllowedMultipleCharactersDetected(GameBackEnd.GuessableWord))
            {
                int indexSkip = 0;
                bool multipleCharactersDetectedAtIndex = false;
                for (int i = 0; i < GameBackEnd.GuessableWord.Length; i++)
                {
                    if (AllowedSpecialCharacters.Any(x => x == GameBackEnd.GuessableWord[i]))
                    {
                        GameBackEnd.DisplayCharacters[i] = actualList[i];
                        if (i + 1 != actualList.Count)
                        {
                            newDipslayList.Add(' ');
                        }
                    }
                    else
                    {
                        for (int j = 0; j < GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords.Count; j++)
                        {
                            if ((i + GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords[j].Length) < GameBackEnd.GuessableWord.Length)
                            {
                                for (int k = 0; k < GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords[j].Length; k++)
                                {

                                    if (GameBackEnd.GuessableWord[i + k] == GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords[j][k])
                                    {
                                        multipleCharactersDetectedAtIndex = true;
                                    }
                                    else
                                    {
                                        multipleCharactersDetectedAtIndex = false;
                                    }
                                }
                            }
                            if (multipleCharactersDetectedAtIndex)
                            {
                                indexSkip = GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords[j].Length;
                                break;
                            }
                        }
                        if (indexSkip > 0)
                        {
                            int temp = 0;
                            while (temp < indexSkip)
                            {
                                newDipslayList[i + temp] = actualList[i + temp];
                                temp++;
                            }
                            if (i + 1 != actualList.Count)
                            {
                                newDipslayList.Add(' ');
                            }
                            i += indexSkip - 1;
                        }
                        else
                        {
                            newDipslayList[i] = actualList[i];
                            if (i + 1 != actualList.Count)
                            {
                                newDipslayList.Add(' ');
                            }
                        }
                        multipleCharactersDetectedAtIndex = false;
                        indexSkip = 0;
                    }
                }
            }
            else
            {
                for (int i = 0; i < actualList.Count; i++)
                {
                    newDipslayList.Add(actualList[i]);
                    if(i + 1 != actualList.Count)
                    {
                        newDipslayList.Add(' ');
                    }
                }
            }
            GameBackEnd.DisplayCharacters = newDipslayList;
        }

        public static void ImplementGuess()
        {
            if (GameBackEnd.GuessableWord != null)
            {
                if (GameBackEnd.GuessableWord.Contains(GameBackEnd.CurrentGuess))
                {
                    int indexSkip = 0;
                    bool multipleCharactersDetectedAtIndex = false;
                    for (int i = 0; i < GameBackEnd.GuessableWord.Length; i++)
                    {
                        for (int j = 0; j < GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords.Count; j++)
                        {
                            if (i + GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords[j].Length < GameBackEnd.GuessableWord.Length)
                            {
                                for (int k = 0; k < GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords[j].Length; k++)
                                {
                                    if (GameBackEnd.GuessableWord[i + k] == GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords[j][k])
                                    {
                                        multipleCharactersDetectedAtIndex = true;
                                    }
                                    else
                                    {
                                        multipleCharactersDetectedAtIndex = false;
                                    }
                                }
                                if (multipleCharactersDetectedAtIndex)
                                {
                                    indexSkip = GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords[j].Length;
                                    break;
                                }
                            }
                        }
                        if (indexSkip > 0)
                        {
                            for (int j = 0; j < GameBackEnd.DisplayCharacters.Count; j++)
                            {
                                int temp = 0;
                                bool rightPlaceInDipslacCharacters = false;
                                while (temp < indexSkip)
                                {
                                    if (GameBackEnd.DisplayCharacters[j + temp] != ' ')
                                    {
                                        rightPlaceInDipslacCharacters = true;
                                    }
                                    else
                                    {
                                        rightPlaceInDipslacCharacters = false;
                                    }
                                    temp++;
                                }
                                temp = 0;
                                if (rightPlaceInDipslacCharacters)
                                {
                                    while (temp < indexSkip)
                                    {
                                        GameBackEnd.DisplayCharacters[j + temp] = GameBackEnd.GuessableWord[i + temp];
                                        temp++;
                                    }
                                }
                            }
                            i += indexSkip - 1;
                        }
                        else
                        {
                            for(int j = 0; j < GameBackEnd.DisplayCharacters.Count; j++)
                            {
                                if(j == 0 && GameBackEnd.GuessableWord[i] == char.Parse(GameBackEnd.CurrentGuess))
                                {
                                    GameBackEnd.DisplayCharacters[j] = char.Parse(GameBackEnd.CurrentGuess);
                                } 
                                else if (GameBackEnd.DisplayCharacters[j] != ' ' && j != 0 && GameBackEnd.GuessableWord[i] == char.Parse(GameBackEnd.CurrentGuess))
                                {

                                }
                            }
                        }
                    }
                }
                else
                {
                    GameBackEnd.IncorrectlyGuessedCharacters.Add(GameBackEnd.CurrentGuess);
                }
            }
        }
    }
}
