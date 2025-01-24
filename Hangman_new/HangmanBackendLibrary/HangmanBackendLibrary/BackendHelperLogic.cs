using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangmanFrontendLibrary;

namespace HangmanBackendLibrary
{
    public class BackendHelperLogic
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
            string guessableWord = MainBackendLogic.GuessableWord;
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
            switch (MainBackendLogic.CurrentInput)
            {
                case 0:
                    MainBackendLogic.GameOver = true;
                    break;
                case 1:
                    MainFrontendLogic.DifficultyChooseWindow();
                    break;
            }
        }

        public static void DifficultyChooserWindowCorrectInput()
        {
            switch (MainBackendLogic.CurrentInput)
            {
                case 0:
                    MainFrontendLogic.MainWindow();
                    break;
                case 1:
                    CreateGameWindow(DifficultyState.GetDifficultyEnumByNumber(1));
                    break;
                case 2:
                    CreateGameWindow(DifficultyState.GetDifficultyEnumByNumber(2));
                    break;
                case 3:
                    CreateGameWindow(DifficultyState.GetDifficultyEnumByNumber(3));
                    break;
            }
        }

        public static void CreateGameWindow(DifficultyEnum? difficulty)
        {
            if (difficulty != null)
            {
                MainBackendLogic.GuessableWord = TextSource.GetGueassableWord(difficulty);
                MainBackendLogic.ActualCharacters = new List<char>();
                MainBackendLogic.DisplayCharacters = new List<char>();
                MainBackendLogic.IncorrectlyGuessedCharacters = new List<string>();
                if (MainBackendLogic.GuessableWord != null && MainBackendLogic.DisplayCharacters != null && MainBackendLogic.IncorrectlyGuessedCharacters != null)
                {
                    foreach (var c in MainBackendLogic.GuessableWord)
                    {
                        if (AllowedSpecialCharacters.Any(x => x == c))
                        {
                            MainBackendLogic.ActualCharacters.Add(c);
                        }
                        else
                        {
                            MainBackendLogic.ActualCharacters.Add('_');
                        }

                    }

                    MainBackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords = GetAllowedMultipleCharactersFromGuessableWord();

                    ActualListToDisplayListConversion(MainBackendLogic.ActualCharacters);

                    MainFrontendLogic.GameWindow();
                }
            }
        }

        public static void ActualListToDisplayListConversion(List<char> actualList)
        {
            List<char> newDipslayList = new List<char>();
            if (AllowedMultipleCharactersDetected(MainBackendLogic.GuessableWord))
            {
                int indexSkip = 0;
                bool multipleCharactersDetectedAtIndex = false;

                for (int i = 0; i < MainBackendLogic.GuessableWord.Length; i++)
                {
                    for (int j = 0; j < MainBackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords.Count; j++)
                    {
                        if ((i + MainBackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords[j].Length) < MainBackendLogic.GuessableWord.Length)
                        {
                            for (int k = 0; k < MainBackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords[j].Length; k++)
                            {
                                
                                if (
                                    ((i + k) < MainBackendLogic.GuessableWord.Length) &&
                                    (MainBackendLogic.GuessableWord[i + k] == MainBackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords[j][k])
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
                            indexSkip = MainBackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords[j].Length;
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
            MainBackendLogic.DisplayCharacters = newDipslayList;
        }

        public static void ImplementGuess()
        {
            if (MainBackendLogic.CurrentGuess.Length == 1)
            {
                bool guessCharIsInMultiWord = false;
                bool singleCharSuccessfullyAddedToActualList = false;
                foreach (var c in MainBackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords)
                {
                    if (c.Contains(MainBackendLogic.CurrentGuess))
                    {
                        guessCharIsInMultiWord = true;
                        break;
                    }
                }
                if (guessCharIsInMultiWord)
                {
                    for (int i = 0; i < MainBackendLogic.GuessableWord.Length; i++)
                    {
                        bool cicleIsInMultiWord = false;
                        foreach (var c in MainBackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords)
                        {
                            int indexSkip = 0;
                            for (int j = 0; j < c.Length; j++)
                            {
                                if (((i + j) < MainBackendLogic.GuessableWord.Length) && MainBackendLogic.GuessableWord[i + j] == c[j])
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
                        if (!cicleIsInMultiWord && MainBackendLogic.GuessableWord[i] == char.Parse(MainBackendLogic.CurrentGuess))
                        {
                            MainBackendLogic.ActualCharacters[i] = char.Parse(MainBackendLogic.CurrentGuess);
                            singleCharSuccessfullyAddedToActualList = true;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < MainBackendLogic.GuessableWord.Length; i++)
                    {
                        if (MainBackendLogic.GuessableWord[i] == char.Parse(MainBackendLogic.CurrentGuess))
                        {
                            MainBackendLogic.ActualCharacters[i] = char.Parse(MainBackendLogic.CurrentGuess);
                            singleCharSuccessfullyAddedToActualList = true;
                        }
                    }
                }
                if (!singleCharSuccessfullyAddedToActualList && !MainBackendLogic.IncorrectlyGuessedCharacters.Contains(MainBackendLogic.CurrentGuess))
                {
                    MainBackendLogic.IncorrectlyGuessedCharacters.Add(MainBackendLogic.CurrentGuess);
                    MainBackendLogic.AttemptsLeft--;
                }

            }
            else
            {
                for (int i = 0; i < MainBackendLogic.GuessableWord.Length; i++)
                {
                    bool cicleIsInMultiWord = false;
                    bool multiWordIsGuessedWord = false;
                    foreach (var c in MainBackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords)
                    {
                        int indexSkip = 0;
                        for (int j = 0; j < c.Length; j++)
                        {
                            if (((i + j) < MainBackendLogic.GuessableWord.Length) && MainBackendLogic.GuessableWord[i + j] == c[j])
                            {
                                cicleIsInMultiWord = true;
                                indexSkip++;
                                if (!(MainBackendLogic.CurrentGuess.Length < c.Length) && MainBackendLogic.CurrentGuess[j] == c[j])
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
                                    MainBackendLogic.ActualCharacters[i + j] = c[j];
                                }
                            }
                            i += indexSkip - 1;
                            break;
                        }
                    }
                }
            }

            ActualListToDisplayListConversion(MainBackendLogic.ActualCharacters);
            MainFrontendLogic.GameWindow();
        }

        public static void IncorrectGuessLogic()
        {
            if (!MainBackendLogic.IncorrectlyGuessedCharacters.Contains(MainBackendLogic.CurrentGuess))
            {
                MainBackendLogic.IncorrectlyGuessedCharacters.Add(MainBackendLogic.CurrentGuess);
                MainBackendLogic.AttemptsLeft--;
            }
            MainFrontendLogic.GameWindow();

        }

        public static int GameOverCheck()
        {
            if (!MainBackendLogic.ActualCharacters.Contains('_'))
            {
                return 0;
            }
            else if (MainBackendLogic.AttemptsLeft == 0)
            {
                return -1;
            }
            return 1;

        }
        public static string? GetCurrentHangmanDrawingByAttemptsLeft()
        {
            switch (MainBackendLogic.AttemptsLeft)
            {
                case 7:
                    return MainBackendLogic.hangmanPics[0];
                case 6:
                    return MainBackendLogic.hangmanPics[1];
                case 5:
                    return MainBackendLogic.hangmanPics[2];
                case 4:
                    return MainBackendLogic.hangmanPics[3];
                case 3:
                    return MainBackendLogic.hangmanPics[4];
                case 2:
                    return MainBackendLogic.hangmanPics[5];
                case 1:
                    return MainBackendLogic.hangmanPics[6];
                case 0:
                    return MainBackendLogic.hangmanPics[7];
                default:
                    return null;
            }
        }
    }
}
