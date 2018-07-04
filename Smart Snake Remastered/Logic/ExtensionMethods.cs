using Smart_Snake_Remastered.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ExtensionMethods
{
    public static Direction ToDirection(this int i)
    {
        switch (i)
        {
            case 0:
                return Direction.North;
            case 1:
                return Direction.East;
            case 2:
                return Direction.South;
            case 3:
                return Direction.West;
            default:
                throw new Exception("Something went wrong.");
        }
    }
    public static bool HasObject(this Color pixel)
    {
        if (pixel.ToArgb() == (Color.White.ToArgb()))
            return false;
        else
            return true;
    }

}
