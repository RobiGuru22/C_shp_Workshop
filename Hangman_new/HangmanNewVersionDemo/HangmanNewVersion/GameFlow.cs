using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangmanNewVersion.Frontend;
using HangmanNewVersion.Backend;
using HangmanNewVersion.States;

namespace HangmanNewVersion
{
    public class GameFlow
    {

        public static void StartGame()
        {
            FrontendLogic.MainWindow();
            MainWindowFlow(BackendLogic.MainWindowLogic());
        }

        public static void MainWindowFlow(int mainWindowInputResult)
        {
            switch (mainWindowInputResult)
            {
                case -1:
                    FrontendLogic.IncorrectInputText();
                    if(BackendLogic.IncorrectMainWindowInputLogic() == -1)
                    {
                        FrontendHelper.WrongInputTextClear();
                        MainWindowFlow(-1);
                    }
                    else
                    {
                        if (BackendHelper.MainWindowCorrectInput() == 0)
                        {
                            BackendLogic.GameOver = true;
                        }
                        else
                        {
                            FrontendLogic.DifficultyChooseWindow();
                            DifficultyChooseWindowFlow(BackendLogic.DifficultyChooserWindowLogic());
                        }
                    }
                    break;
                case 0:
                    if(BackendHelper.MainWindowCorrectInput() == 0)
                    {
                        BackendLogic.GameOver = true;
                    }
                    else
                    {
                        FrontendLogic.DifficultyChooseWindow();
                        DifficultyChooseWindowFlow(BackendLogic.DifficultyChooserWindowLogic());
                    }
                    break;
                default:
                    FrontendHelper.WrongInputTextClear();
                    MainWindowFlow(-1);
                    break;
            }
        }

        public static void DifficultyChooseWindowFlow(int difficultyWindowInputResult)
        {
            switch(difficultyWindowInputResult)
            {
                case -1:
                    FrontendLogic.IncorrectInputText();
                    if(BackendLogic.IncorrectDifficultyChooseWindowInputLogic() == -1)
                    {
                        FrontendHelper.WrongInputTextClear();
                        DifficultyChooseWindowFlow(-1);
                    }
                    else
                    {
                        GameWindowFlow(BackendHelper.DifficultyChooserWindowCorrectInput());
                    }
                    break;
                case 0:
                    if(BackendHelper.DifficultyChooserWindowCorrectInput() == 0)
                    {
                        StartGame();
                    }
                    else
                    {
                        GameWindowFlow(BackendHelper.DifficultyChooserWindowCorrectInput());
                    }
                    
                    break;
                default:
                    FrontendHelper.WrongInputTextClear();
                    DifficultyChooseWindowFlow(-1);
                    break;
            }
        }

        public static void GameWindowFlow(int chosenDifficulty)
        {
            BackendHelper.CreateGameWindow(DifficultyState.GetDifficultyEnumByNumber(chosenDifficulty));
            FrontendLogic.GameWindow(
                BackendLogic.DisplayCharacters, 
                BackendLogic.AttemptsLeft, 
                BackendLogic.IncorrectlyGuessedCharacters, 
                BackendHelper.GetCurrentHangmanDrawingByAttemptsLeft()
                );
            switch(BackendLogic.GameWindowLogic())
            {
                case -1:
                    GameOverWindowFlow(-1);
                    return;
                case 0:
                    FrontendLogic.IncorrectGuessFormatText();
                    if(BackendLogic.IncorrectGuessFormatTextLogic() == -1)
                    {
                        FrontendHelper.WrongInputTextClear();
                        GameWindowFlow(chosenDifficulty);
                    }
                    else
                    {
                        BackendHelper.ImplementGuess();
                        GameWindowFlow(chosenDifficulty);
                    }
                    break;
                case 1:
                    BackendHelper.IncorrectGuessLogic();
                    GameWindowFlow(chosenDifficulty);
                    break;
                case 2:

                    break;
            }
        }

        public static void GameOverWindowFlow(int gameOverState)
        {
            if(gameOverState == -1)
            {
                FrontendLogic.DefeatWindow(BackendLogic.GuessableWord);
            }
            else
            {
                FrontendLogic.VictoryWindow(BackendLogic.IncorrectlyGuessedCharacters);
            }
            BackendLogic.GameOver = true;
        }
    }
}
