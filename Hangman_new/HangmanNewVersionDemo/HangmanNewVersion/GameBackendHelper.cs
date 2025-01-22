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
                if (guessableWord.Contains(w))
                {
                    return true;
                }
            }
            return false;
        }

        public static List<string> GetAllowedMultipleCharactersFromGuessableWord()
        {
            string guessableWord = GameBackEnd.GuessableWord;
            List<string> allowedMultipleCharactersInWord = new List<string>();
            bool dzsFound = false;
            foreach (var w in AllowedMultipleCharacters)
            {
                if (guessableWord.Contains(w) && w != "dz" && w != "dzs")
                {
                    allowedMultipleCharactersInWord.Add(w);
                }
                else if (guessableWord.Contains("dz"))
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
                    if (!dzsFound)
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
                if (GameBackEnd.GuessableWord != null && GameBackEnd.DisplayCharacters != null && GameBackEnd.IncorrectlyGuessedCharacters != null)
                {
                    foreach (var c in GameBackEnd.GuessableWord)
                    {
                        if (AllowedSpecialCharacters.Any(x => x == c))
                        {
                            GameBackEnd.ActualCharacters.Add(c);
                        }
                        else
                        {
                            GameBackEnd.ActualCharacters.Add('_');
                        }

                    }

                    GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords = GetAllowedMultipleCharactersFromGuessableWord();

                    ActualListToDisplayListConversion(GameBackEnd.ActualCharacters);

                    GameFrontEnd.GameWindow();
                }
            }
        }

        public static void ActualListToDisplayListConversion(List<char> actualList)
        {
            List<char> newDipslayList = new List<char>();
            if (AllowedMultipleCharactersDetected(GameBackEnd.GuessableWord))
            {
                int indexSkip = 0;
                bool multipleCharactersDetectedAtIndex = false;

                for (int i = 0; i < GameBackEnd.GuessableWord.Length; i++)
                {
                    for (int j = 0; j < GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords.Count; j++)
                    {
                        if ((i + GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords[j].Length) < GameBackEnd.GuessableWord.Length)
                        {
                            for (int k = 0; k < GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords[j].Length; k++)
                            {
                                //
                                if (
                                    ((i + k) < GameBackEnd.GuessableWord.Length) &&
                                    (GameBackEnd.GuessableWord[i + k] == GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords[j][k])
                                    )
                                {
                                    multipleCharactersDetectedAtIndex = true;
                                }
                                else
                                {
                                    multipleCharactersDetectedAtIndex = false;
                                    break;
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
                            newDipslayList.Add(actualList[i + temp]);
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
                        newDipslayList.Add(actualList[i]);
                        if (i + 1 != actualList.Count)
                        {
                            newDipslayList.Add(' ');
                        }
                    }
                    multipleCharactersDetectedAtIndex = false;
                    indexSkip = 0;
                }
            }
            else
            {
                for (int i = 0; i < actualList.Count; i++)
                {
                    newDipslayList.Add(actualList[i]);
                    if (i + 1 != actualList.Count)
                    {
                        newDipslayList.Add(' ');
                    }
                }
            }
            GameBackEnd.DisplayCharacters = newDipslayList;
        }

        public static void ImplementGuess()
        {
            if (GameBackEnd.CurrentGuess.Length == 1)
            {
                bool guessCharIsInMultiWord = false;
                bool singleCharSuccessfullyAddedToActualList = false;
                foreach (var c in GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords)
                {
                    if (c.Contains(GameBackEnd.CurrentGuess))
                    {
                        guessCharIsInMultiWord = true;
                        break;
                    }
                }
                if (guessCharIsInMultiWord)
                {
                    for (int i = 0; i < GameBackEnd.GuessableWord.Length; i++)
                    {
                        bool cicleIsInMultiWord = false;
                        foreach (var c in GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords)
                        {
                            int indexSkip = 0;
                            for (int j = 0; j < c.Length; j++)
                            {
                                if (((i + j) < GameBackEnd.GuessableWord.Length) && GameBackEnd.GuessableWord[i + j] == c[j])
                                {
                                    cicleIsInMultiWord = true;
                                    indexSkip++;
                                }
                                else
                                {
                                    cicleIsInMultiWord = false;
                                    break;
                                }
                            }
                            if (cicleIsInMultiWord)
                            {
                                i += indexSkip - 1;
                                break;
                            }
                        }
                        if (!cicleIsInMultiWord && GameBackEnd.GuessableWord[i] == char.Parse(GameBackEnd.CurrentGuess))
                        {
                            GameBackEnd.ActualCharacters[i] = char.Parse(GameBackEnd.CurrentGuess);
                            singleCharSuccessfullyAddedToActualList = true;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < GameBackEnd.GuessableWord.Length; i++)
                    {
                        if (GameBackEnd.GuessableWord[i] == char.Parse(GameBackEnd.CurrentGuess))
                        {
                            GameBackEnd.ActualCharacters[i] = char.Parse(GameBackEnd.CurrentGuess);
                            singleCharSuccessfullyAddedToActualList = true;
                        }
                    }
                }
                if (!singleCharSuccessfullyAddedToActualList && !GameBackEnd.IncorrectlyGuessedCharacters.Contains(GameBackEnd.CurrentGuess))
                {
                    GameBackEnd.IncorrectlyGuessedCharacters.Add(GameBackEnd.CurrentGuess);
                    GameBackEnd.AttemptsLeft--;
                }

            }
            else
            {
                for (int i = 0; i < GameBackEnd.GuessableWord.Length; i++)
                {
                    bool cicleIsInMultiWord = false;
                    bool multiWordIsGuessedWord = false;
                    foreach (var c in GameBackEnd.CurrentAllowedMultipleCharacterWordInGueassableWords)
                    {
                        int indexSkip = 0;
                        for (int j = 0; j < c.Length; j++)
                        {
                            if (((i + j) < GameBackEnd.GuessableWord.Length) && GameBackEnd.GuessableWord[i + j] == c[j])
                            {
                                cicleIsInMultiWord = true;
                                indexSkip++;
                                if (!(GameBackEnd.CurrentGuess.Length < c.Length) && GameBackEnd.CurrentGuess[j] == c[j])
                                {
                                    multiWordIsGuessedWord = true;
                                }
                                else
                                {
                                    multiWordIsGuessedWord = false;
                                }
                            }
                            else
                            {
                                cicleIsInMultiWord = false;
                                multiWordIsGuessedWord = false;
                            }
                        }
                        if (cicleIsInMultiWord)
                        {
                            if (multiWordIsGuessedWord)
                            {
                                for (int j = 0; j < c.Length; j++)
                                {
                                    GameBackEnd.ActualCharacters[i + j] = c[j];
                                }
                            }
                            i += indexSkip - 1;
                            break;
                        }
                    }
                }
            }

            ActualListToDisplayListConversion(GameBackEnd.ActualCharacters);
            GameFrontEnd.GameWindow();
        }

        public static void IncorrectGuessLogic()
        {
            if (!GameBackEnd.IncorrectlyGuessedCharacters.Contains(GameBackEnd.CurrentGuess))
            {
                GameBackEnd.IncorrectlyGuessedCharacters.Add(GameBackEnd.CurrentGuess);
                GameBackEnd.AttemptsLeft--;
            }
            GameFrontEnd.GameWindow();

        }

        public static int GameOverCheck()
        {
            if (!GameBackEnd.ActualCharacters.Contains('_'))
            {
                return 0;
            }
            else if (GameBackEnd.AttemptsLeft == 0)
            {
                return -1;
            }
            return 1;

        }
        public static string? GetCurrentHangmanDrawingByAttemptsLeft()
        {
            switch (GameBackEnd.AttemptsLeft)
            {
                case 7:
                    return GameBackEnd.hangmanPics[0];
                case 6:
                    return GameBackEnd.hangmanPics[1];
                case 5:
                    return GameBackEnd.hangmanPics[2];
                case 4:
                    return GameBackEnd.hangmanPics[3];
                case 3:
                    return GameBackEnd.hangmanPics[4];
                case 2:
                    return GameBackEnd.hangmanPics[5];
                case 1:
                    return GameBackEnd.hangmanPics[6];
                case 0:
                    return GameBackEnd.hangmanPics[7];
                default:
                    return null;
            }
        }
    }
}
