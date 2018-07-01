using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Smart_Snake_Remastered.Models;

namespace Smart_Snake_Remastered.Logic
{
    public static class Business
    {
        public static Point MoveDirection(Point location, Direction direction, Grid currentGrid)
        {
            Point newLocation = new Point(location.X, location.Y);
            switch (direction)
            {
                case Direction.North:
                    newLocation.Y = location.Y + 1;
                    break;
                case Direction.East:
                    newLocation.X = location.X + 1;
                    break;
                case Direction.South:
                    newLocation.Y = location.Y - 1;
                    break;
                case Direction.West:
                    newLocation.X = location.X - 1;
                    break;
                default:
                    throw new Exception("Unsupported direction");
            }
            if (currentGrid.IsAvailable(newLocation))
            {
                return newLocation;
            }
            else
            {
                //go to other side of screen
            }
        }
    }
}
