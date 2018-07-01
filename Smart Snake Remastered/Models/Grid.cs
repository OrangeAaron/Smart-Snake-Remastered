using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Smart_Snake_Remastered.Models
{
    public class Grid
    {
        public GridBox[,] BoxMatrix;
        public UInt64 Lifetime;
        public Random GridSeed;

        public Grid() : this(10) {}

        public Grid(int sizeXbyX)
        {
            Lifetime = 0;
            BoxMatrix = new GridBox[sizeXbyX, sizeXbyX];
            GridSeed = new Random(DateTime.Now.Millisecond + DateTime.Now.Day + DateTime.Now.Year);
        }
        
        public bool IsAvailable(Point location)
        {
            if (BoxMatrix[location.X, location.Y].HasObject = false && WithinBounds(location)) return true;
            return false;
        }

        public Boolean WithinBounds(Point location)
        {
            var maxXCoordinate = this.BoxMatrix.GetLength(0);
            var maxYCoordinate = this.BoxMatrix.GetLength(1);
            var minXCoordinate = 0;
            var minYCoordinate = 0;

            if (location.X >= minXCoordinate && location.X <= maxXCoordinate)
            {
                if (location.Y >= minYCoordinate && location.Y <= maxYCoordinate) return true;
            }
            return false;
        }
        

        public class GridBox
        {
            public object ContainedObject
            {
                get { return this; }
                set
                {
                    ContainedObject = value;
                    if (value == null) HasObject = false;
                    else HasObject = true;
                }
            }
            public bool HasObject = false;

        }
    }
}
