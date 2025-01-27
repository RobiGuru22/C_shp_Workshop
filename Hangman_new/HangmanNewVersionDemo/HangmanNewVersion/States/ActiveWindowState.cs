using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanNewVersion.States
{
    public class ActiveWindowState
    {
        public static ActiveWindowEnum? GetAcativeWindowEnumByNumber(int windowNumber)
        {
            switch (windowNumber)
            {
                case 1:
                    return ActiveWindowEnum.MAIN_WINDOW_ACTIVE;
                case 2:
                    return ActiveWindowEnum.DIFFICULTY_CHOOSER_WINDOW_ACTIVE;
                case 3:
                    return ActiveWindowEnum.GAME_WINDOW_ACTIVE;
                default:
                    return null;
            }
        }
    }
    public enum ActiveWindowEnum
    {
        MAIN_WINDOW_ACTIVE,
        DIFFICULTY_CHOOSER_WINDOW_ACTIVE,
        GAME_WINDOW_ACTIVE
    }
}
