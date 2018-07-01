using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Snake_Remastered.Models
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public static class DirectionExtension
    {
        public static int ToInt(this Direction dir)
        {
            switch (dir)
            {
                case Direction.North:
                    return 0;
                case Direction.East:
                    return 1;
                case Direction.South:
                    return 2;
                case Direction.West:
                    return 3;
                default:
                    throw new Exception("Something went wrong.");
            }
        }
    }
}
