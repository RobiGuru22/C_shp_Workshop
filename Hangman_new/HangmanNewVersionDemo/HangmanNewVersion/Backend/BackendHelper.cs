using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangmanNewVersion.States;

namespace HangmanNewVersion.Backend
{
    public class BackendHelper
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
                "ny",
                "sz",
                "ty",
                "zs"
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
            string guessableWord = BackendLogic.GuessableWord;
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
                dzsFound = false;
            }

            return allowedMultipleCharactersInWord;
        }

        public static CorrectInputMainWindowLogicStateEnum MainWindowCorrectInput()
        {
            switch (BackendLogic.CurrentInput)
            {
                case 0:
                    return CorrectInputMainWindowLogicStateEnum.GAMEOVER;
                case 1:
                    return CorrectInputMainWindowLogicStateEnum.CONTINUE;
                    //MainFrontendLogic.DifficultyChooseWindow();
                default:
                    return CorrectInputMainWindowLogicStateEnum.GAMEOVER;
            }
        }

        public static DifficultyEnum? DifficultyChooserWindowCorrectInput()
        {
            if(BackendLogic.CurrentInput > -1)
            {
                return DifficultyState.GetDifficultyEnumByNumber(BackendLogic.CurrentInput);
            }
            return null;
            //switch (BackendLogic.CurrentInput)
            //{
            //    case 0:
            //        //MainFrontendLogic.MainWindow();

            //        break;
            //    case 1:
            //        CreateGameWindow(DifficultyState.GetDifficultyEnumByNumber(1));
            //        break;
            //    case 2:
            //        CreateGameWindow(DifficultyState.GetDifficultyEnumByNumber(2));
            //        break;
            //    case 3:
            //        CreateGameWindow(DifficultyState.GetDifficultyEnumByNumber(3));
            //        break;
            //}
        }

        public static void CreateGameWindow(DifficultyEnum? difficulty)
        {
            if (difficulty != null)
            {
                BackendLogic.GuessableWord = TextSource.GetGueassableWord(difficulty);
                BackendLogic.ActualCharacters = new List<char>();
                BackendLogic.DisplayCharacters = new List<char>();
                BackendLogic.IncorrectlyGuessedCharacters = new List<string>();
                if (BackendLogic.GuessableWord != null && BackendLogic.DisplayCharacters != null && BackendLogic.IncorrectlyGuessedCharacters != null)
                {
                    foreach (var c in BackendLogic.GuessableWord)
                    {
                        if (AllowedSpecialCharacters.Any(x => x == c))
                        {
                            BackendLogic.ActualCharacters.Add(c);
                        }
                        else
                        {
                            BackendLogic.ActualCharacters.Add('_');
                        }

                    }

                    BackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords = GetAllowedMultipleCharactersFromGuessableWord();

                    ActualListToDisplayListConversion(BackendLogic.ActualCharacters);

                    //MainFrontendLogic.GameWindow();
                }
            }
        }

        public static void ActualListToDisplayListConversion(List<char> actualList)
        {
            List<char> newDipslayList = new List<char>();
            if (AllowedMultipleCharactersDetected(BackendLogic.GuessableWord))
            {
                int indexSkip = 0;
                bool multipleCharactersDetectedAtIndex = false;

                for (int i = 0; i < BackendLogic.GuessableWord.Length; i++)
                {
                    for (int j = 0; j < BackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords.Count; j++)
                    {
                        if ((i + BackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords[j].Length) < BackendLogic.GuessableWord.Length)
                        {
                            for (int k = 0; k < BackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords[j].Length; k++)
                            {

                                if (
                                    ((i + k) < BackendLogic.GuessableWord.Length) &&
                                    (BackendLogic.GuessableWord[i + k] == BackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords[j][k])
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
                            indexSkip = BackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords[j].Length;
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
            BackendLogic.DisplayCharacters = newDipslayList;
        }

        public static void ImplementGuess()
        {
            if (BackendLogic.CurrentGuess.Length == 1)
            {
                bool guessCharIsInMultiWord = false;
                bool singleCharSuccessfullyAddedToActualList = false;
                foreach (var c in BackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords)
                {
                    if (c.Contains(BackendLogic.CurrentGuess))
                    {
                        guessCharIsInMultiWord = true;
                        break;
                    }
                }
                if (guessCharIsInMultiWord)
                {
                    for (int i = 0; i < BackendLogic.GuessableWord.Length; i++)
                    {
                        bool cicleIsInMultiWord = false;
                        foreach (var c in BackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords)
                        {
                            int indexSkip = 0;
                            for (int j = 0; j < c.Length; j++)
                            {
                                if (((i + j) < BackendLogic.GuessableWord.Length) && BackendLogic.GuessableWord[i + j] == c[j])
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
                        if (!cicleIsInMultiWord && BackendLogic.GuessableWord[i] == char.Parse(BackendLogic.CurrentGuess))
                        {
                            BackendLogic.ActualCharacters[i] = char.Parse(BackendLogic.CurrentGuess);
                            singleCharSuccessfullyAddedToActualList = true;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < BackendLogic.GuessableWord.Length; i++)
                    {
                        if (BackendLogic.GuessableWord[i] == char.Parse(BackendLogic.CurrentGuess))
                        {
                            BackendLogic.ActualCharacters[i] = char.Parse(BackendLogic.CurrentGuess);
                            singleCharSuccessfullyAddedToActualList = true;
                        }
                    }
                }
                if (!singleCharSuccessfullyAddedToActualList && !BackendLogic.IncorrectlyGuessedCharacters.Contains(BackendLogic.CurrentGuess))
                {
                    BackendLogic.IncorrectlyGuessedCharacters.Add(BackendLogic.CurrentGuess);
                    BackendLogic.AttemptsLeft--;
                }

            }
            else
            {
                bool wordFoundAtLeastOnce = false;
                for (int i = 0; i < BackendLogic.GuessableWord.Length; i++)
                {
                    bool cicleIsInMultiWord = false;
                    bool multiWordIsGuessedWord = false;
                    foreach (var c in BackendLogic.CurrentAllowedMultipleCharacterWordInGueassableWords)
                    {
                        int indexSkip = 0;
                        for (int j = 0; j < c.Length; j++)
                        {
                            if (((i + j) < BackendLogic.GuessableWord.Length) && BackendLogic.GuessableWord[i + j] == c[j])
                            {
                                cicleIsInMultiWord = true;
                                indexSkip++;
                                if (!(BackendLogic.CurrentGuess.Length < c.Length) && BackendLogic.CurrentGuess[j] == c[j])
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
                                    BackendLogic.ActualCharacters[i + j] = c[j];
                                }
                                wordFoundAtLeastOnce = true;
                            }
                            i += indexSkip - 1;
                            break;
                        }
                    }
                }
                if (!wordFoundAtLeastOnce && !BackendLogic.IncorrectlyGuessedCharacters.Contains(BackendLogic.CurrentGuess))
                {
                    BackendLogic.IncorrectlyGuessedCharacters.Add(BackendLogic.CurrentGuess);
                    BackendLogic.AttemptsLeft--;
                }
            }

            ActualListToDisplayListConversion(BackendLogic.ActualCharacters);
            //MainFrontendLogic.GameWindow();
        }

        public static void IncorrectGuessLogic()
        {
            if (!BackendLogic.IncorrectlyGuessedCharacters.Contains(BackendLogic.CurrentGuess))
            {
                BackendLogic.IncorrectlyGuessedCharacters.Add(BackendLogic.CurrentGuess);
                BackendLogic.AttemptsLeft--;
            }
            //MainFrontendLogic.GameWindow();

        }

        public static GameOverEnum GameOverCheck()
        {
            
            if (!BackendLogic.ActualCharacters.Contains('_'))
            {
                return GameOverEnum.WIN;
            }
            else if (BackendLogic.AttemptsLeft == 0)
            {
                return GameOverEnum.LOSE;
            }
            return GameOverEnum.DEFAULT;

        }
        public static string? GetCurrentHangmanDrawingByAttemptsLeft()
        {
            switch (BackendLogic.AttemptsLeft)
            {
                case 7:
                    return BackendLogic.hangmanPics[0];
                case 6:
                    return BackendLogic.hangmanPics[1];
                case 5:
                    return BackendLogic.hangmanPics[2];
                case 4:
                    return BackendLogic.hangmanPics[3];
                case 3:
                    return BackendLogic.hangmanPics[4];
                case 2:
                    return BackendLogic.hangmanPics[5];
                case 1:
                    return BackendLogic.hangmanPics[6];
                case 0:
                    return BackendLogic.hangmanPics[7];
                default:
                    return null;
            }
        }
    }
}
