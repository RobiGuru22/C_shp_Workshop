using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanNewVersion
{
    public class GameOverWindow
    {
        public static GameOverEnum GetEnumByInt(int gameOverInt)
        {
            switch(gameOverInt)
            {
                case 0:
                    return GameOverEnum.WIN;
                case -1:
                    return GameOverEnum.LOSE;
                default:
                    return GameOverEnum.DEFAULT;
            }
        }
    }

    public enum GameOverEnum
    {
        DEFAULT,
        WIN,
        LOSE
    }
}
