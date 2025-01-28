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
        public static bool AlreadyInWrongGuessFormatState = false;
        public static void StartGame()
        {
            FrontendLogic.MainWindow();
            MainWindowFlow(BackendLogic.MainWindowLogic());
        }

        public static void MainWindowFlow(MainWindowLogicStateEnum mainWindowInputResult)
        {
            switch (mainWindowInputResult)
            {
                case MainWindowLogicStateEnum.INCORRECT_INPUT:
                    FrontendLogic.IncorrectInputText();
                    if(BackendLogic.IncorrectMainWindowInputLogic() == IncorrectInputMainWindowLogicStateEnum.INCORRECT_INPUT)
                    {
                        FrontendHelper.WrongInputTextClear();
                        MainWindowFlow(MainWindowLogicStateEnum.INCORRECT_INPUT);
                    }
                    else
                    {
                        if (BackendHelper.MainWindowCorrectInput() == CorrectInputMainWindowLogicStateEnum.GAMEOVER)
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
                case MainWindowLogicStateEnum.CORRECT_INPUT:
                    if(BackendHelper.MainWindowCorrectInput() == CorrectInputMainWindowLogicStateEnum.GAMEOVER)
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
                    MainWindowFlow(MainWindowLogicStateEnum.INCORRECT_INPUT);
                    break;
            }
        }

        public static void DifficultyChooseWindowFlow(DifficultyChooserWindowLogicStateEnum difficultyWindowInputResult)
        {
            switch(difficultyWindowInputResult)
            {
                case DifficultyChooserWindowLogicStateEnum.INCORRECT_DIFFICULTY:
                    FrontendLogic.IncorrectInputText();
                    if(BackendLogic.IncorrectDifficultyChooseWindowInputLogic() == IncorrectDifficultyChooseWindowInputLogicStateEnum.INCORRECT_DIFFICULTY_INPUT)
                    {
                        FrontendHelper.WrongInputTextClear();
                        DifficultyChooseWindowFlow(DifficultyChooserWindowLogicStateEnum.INCORRECT_DIFFICULTY);
                    }
                    else
                    {
                        if(BackendHelper.DifficultyChooserWindowCorrectInput() == DifficultyEnum.DEFAULT)
                        {
                            StartGame();
                        }
                        else
                        {
                            BackendHelper.CreateGameWindow(BackendHelper.DifficultyChooserWindowCorrectInput());
                            GameWindowFlow();
                        }
                        
                    }
                    break;
                case DifficultyChooserWindowLogicStateEnum.CORRECT_DIFFICULTY:
                    if(BackendHelper.DifficultyChooserWindowCorrectInput() == DifficultyEnum.DEFAULT)
                    {
                        StartGame();
                    }
                    else
                    {
                        BackendHelper.CreateGameWindow(BackendHelper.DifficultyChooserWindowCorrectInput());
                        GameWindowFlow();
                    }
                    
                    break;
                default:
                    FrontendHelper.WrongInputTextClear();
                    DifficultyChooseWindowFlow(DifficultyChooserWindowLogicStateEnum.INCORRECT_DIFFICULTY);
                    break;
            }
        }

        public static void GameWindowFlow()
        {
            if(!AlreadyInWrongGuessFormatState)
            {
                FrontendLogic.GameWindow(
                BackendLogic.DisplayCharacters,
                BackendLogic.AttemptsLeft,
                BackendLogic.IncorrectlyGuessedCharacters,
                BackendHelper.GetCurrentHangmanDrawingByAttemptsLeft()
                );
            }
            switch (BackendLogic.GameWindowLogic())
            {
                case GameWindowLogicStateEnum.GAMEOVER:
                    GameOverWindowFlow(BackendHelper.GameOverCheck());
                    break;
                case GameWindowLogicStateEnum.INCORRECT_GUESS_FORMAT:
                    if(!AlreadyInWrongGuessFormatState)
                    {
                        FrontendLogic.IncorrectGuessFormatText();
                    }
                    else
                    {
                        FrontendHelper.WrongInputTextClear();
                        FrontendLogic.IncorrectGuessFormatText();
                    }
                    
                    AlreadyInWrongGuessFormatState = true;

                    if (BackendLogic.IncorrectGuessFormatTextLogic() == IncorrectGuessFormatLogicStateEnum.INCORRECT_GUESS)
                    {
                        FrontendHelper.WrongInputTextClear();
                        FrontendLogic.IncorrectGuessFormatText();
                        GameWindowFlow();
                    }
                    else
                    {
                        AlreadyInWrongGuessFormatState = false;
                        BackendHelper.ImplementGuess();
                        GameWindowFlow();
                    }
                    break;
                case GameWindowLogicStateEnum.INCORRECT_GUESS:
                    AlreadyInWrongGuessFormatState = false;
                    BackendHelper.IncorrectGuessLogic();
                    BackendHelper.ImplementGuess();
                    GameWindowFlow();
                    break;
                case GameWindowLogicStateEnum.CORRECT_GUESS:
                    AlreadyInWrongGuessFormatState = false;
                    BackendHelper.ImplementGuess();
                    GameWindowFlow();
                    break;
            }
        }

        public static void GameOverWindowFlow(GameOverEnum gameOverState)
        {
            switch(gameOverState)
            {
                case GameOverEnum.WIN:
                    FrontendLogic.VictoryWindow(BackendLogic.IncorrectlyGuessedCharacters);
                    break;
                case GameOverEnum.LOSE:
                    FrontendLogic.DefeatWindow(BackendLogic.GuessableWord);
                    break;
            }
            BackendLogic.GameOver = true;
        }
    }
}
