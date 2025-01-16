using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanNewVersion
{
    public class Difficulty
    {
        public static DifficultyEnum? GetDifficultyEnumByNumber(int difficultyNumber)
        {
            switch(difficultyNumber)
            {
                case 1:
                    return DifficultyEnum.EASY;
                case 2:
                    return DifficultyEnum.MEDIUM;
                case 3:
                    return DifficultyEnum.HARD;
                default:
                    return null;
            }
        }
    }

    public enum DifficultyEnum
    {
        EASY,
        MEDIUM,
        HARD
    }
}
