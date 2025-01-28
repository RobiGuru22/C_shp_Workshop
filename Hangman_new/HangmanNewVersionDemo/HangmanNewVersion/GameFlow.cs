using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangmanNewVersion.Frontend;
using HangmanNewVersion.Backend;
using HangmanNewVersion.States;
using System.Windows.Markup;

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
                        if(BackendHelper.DifficultyChooserWindowCorrectInput() == 0)
                        {
                            StartGame();
                        }
                        else
                        {
                            BackendHelper.CreateGameWindow(DifficultyState.GetDifficultyEnumByNumber(BackendHelper.DifficultyChooserWindowCorrectInput()));
                            GameWindowFlow();
                        }
                        
                    }
                    break;
                case 0:
                    if(BackendHelper.DifficultyChooserWindowCorrectInput() == 0)
                    {
                        StartGame();
                    }
                    else
                    {
                        BackendHelper.CreateGameWindow(DifficultyState.GetDifficultyEnumByNumber(BackendHelper.DifficultyChooserWindowCorrectInput()));
                        GameWindowFlow();
                    }
                    
                    break;
                default:
                    FrontendHelper.WrongInputTextClear();
                    DifficultyChooseWindowFlow(-1);
                    break;
            }
        }

        public static void GameWindowFlow()
        {   
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
                    break;
                case 0:
                    GameOverWindowFlow(0);
                    break;
                case 1:
                    FrontendLogic.IncorrectGuessFormatText();
                    if(BackendLogic.IncorrectGuessFormatTextLogic() == -1)
                    {
                        FrontendHelper.WrongInputTextClear();
                        FrontendLogic.IncorrectGuessFormatText();
                        GameWindowFlow();
                    }
                    else
                    {
                        FrontendHelper.WrongInputTextClear();
                        BackendHelper.ImplementGuess();
                        GameWindowFlow();
                    }
                    break;
                case 2:
                    BackendHelper.IncorrectGuessLogic();
                    BackendHelper.ImplementGuess();
                    GameWindowFlow();
                    break;
                case 3:
                    BackendHelper.ImplementGuess();
                    GameWindowFlow();
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
