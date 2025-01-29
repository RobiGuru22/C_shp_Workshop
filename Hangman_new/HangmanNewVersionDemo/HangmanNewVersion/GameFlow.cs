using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using HangmanNewVersion.Backend;
using HangmanNewVersion.Frontend;

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

        public static void MainWindowFlow(States.MainWindowLogicStateEnum mainWindowInputResult)
        {
            switch (mainWindowInputResult)
            {
                case States.MainWindowLogicStateEnum.INCORRECT_INPUT:
                    FrontendLogic.IncorrectInputText();
                    if(BackendLogic.IncorrectMainWindowInputLogic() == States.IncorrectInputMainWindowLogicStateEnum.INCORRECT_INPUT)
                    {
                        FrontendHelper.WrongInputTextClear();
                        MainWindowFlow(States.MainWindowLogicStateEnum.INCORRECT_INPUT);
                    }
                    else
                    {
                        if (BackendHelper.MainWindowCorrectInput() == States.CorrectInputMainWindowLogicStateEnum.GAMEOVER)
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
                case States.MainWindowLogicStateEnum.CORRECT_INPUT:
                    if(BackendHelper.MainWindowCorrectInput() == States.CorrectInputMainWindowLogicStateEnum.GAMEOVER)
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
                    MainWindowFlow(States.MainWindowLogicStateEnum.INCORRECT_INPUT);
                    break;
            }
        }

        public static void DifficultyChooseWindowFlow(States.DifficultyChooserWindowLogicStateEnum difficultyWindowInputResult)
        {
            switch(difficultyWindowInputResult)
            {
                case States.DifficultyChooserWindowLogicStateEnum.INCORRECT_DIFFICULTY:
                    FrontendLogic.IncorrectInputText();
                    if(BackendLogic.IncorrectDifficultyChooseWindowInputLogic() == States.IncorrectDifficultyChooseWindowInputLogicStateEnum.INCORRECT_DIFFICULTY_INPUT)
                    {
                        FrontendHelper.WrongInputTextClear();
                        DifficultyChooseWindowFlow(States.DifficultyChooserWindowLogicStateEnum.INCORRECT_DIFFICULTY);
                    }
                    else
                    {
                        if(BackendHelper.DifficultyChooserWindowCorrectInput() == States.DifficultyEnum.DEFAULT)
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
                case States.DifficultyChooserWindowLogicStateEnum.CORRECT_DIFFICULTY:
                    if(BackendHelper.DifficultyChooserWindowCorrectInput() == States.DifficultyEnum.DEFAULT)
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
                    DifficultyChooseWindowFlow(States.DifficultyChooserWindowLogicStateEnum.INCORRECT_DIFFICULTY);
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
                case States.GameWindowLogicStateEnum.GAMEOVER:
                    GameOverWindowFlow(BackendHelper.GameOverCheck());
                    break;
                case States.GameWindowLogicStateEnum.INCORRECT_GUESS_FORMAT:
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

                    if (BackendLogic.IncorrectGuessFormatTextLogic() == States.IncorrectGuessFormatLogicStateEnum.INCORRECT_GUESS)
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
                case States.GameWindowLogicStateEnum.IMPLEMENTABLE_GUESS:
                    AlreadyInWrongGuessFormatState = false;
                    BackendHelper.ImplementGuess();
                    GameWindowFlow();
                    break;
            }
        }

        public static void GameOverWindowFlow(States.GameOverEnum gameOverState)
        {
            switch(gameOverState)
            {
                case States.GameOverEnum.WIN:
                    FrontendLogic.VictoryWindow(BackendLogic.IncorrectlyGuessedCharacters);
                    break;
                case States.GameOverEnum.LOSE:
                    FrontendLogic.DefeatWindow(BackendLogic.GuessableWord);
                    break;
            }
            BackendLogic.GameOver = true;
        }
    }
}
