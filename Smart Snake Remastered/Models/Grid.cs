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
            for (int x = 0; x < sizeXbyX; x++)
            {
                for (int y = 0; y < sizeXbyX; y++)
                {
                    BoxMatrix[x, y] = new GridBox();
                }
            }
            GridSeed = new Random(DateTime.Now.Millisecond + DateTime.Now.Day + DateTime.Now.Year);
        }
        
        public bool IsAvailable(Point location)
        {
            if (BoxMatrix[location.X, location.Y].HasObject = false && WithinBounds(location)) return true;
            return false;
        }
        

        public Boolean WithinBounds(Point location)
        {
            var maxXCoordinate = this.BoxMatrix.GetBorderIndex(0);
            var maxYCoordinate = this.BoxMatrix.GetBorderIndex(1);
            var minXCoordinate = 0;
            var minYCoordinate = 0;

            if (location.X >= minXCoordinate && location.X <= maxXCoordinate)
            {
                if (location.Y >= minYCoordinate && location.Y <= maxYCoordinate) return true;
            }
            return false;
        }

        public Point SendToOtherSideOfScreen(Point initialLocation)
        {
            Point newLocation = new Point();

            if (initialLocation.X > this.BoxMatrix.GetBorderIndex(0))  newLocation.X = 0;
            else if (initialLocation.X < 0)  newLocation.X = this.BoxMatrix.GetBorderIndex(0);

            if (initialLocation.Y > this.BoxMatrix.GetBorderIndex(1)) newLocation.Y = 0;
            else if (initialLocation.Y < 0) newLocation.Y = this.BoxMatrix.GetBorderIndex(1);


            return newLocation;
        }

        public class GridBox
        {
            private object _containedObject;
            public object ContainedObject
            {
                get { return _containedObject; }
                set
                {
                    _containedObject = value;
                    if (value == null) HasObject = false;
                    else HasObject = true;
                }
            }
            public bool HasObject = false;
            public GridBox()
            {
                _containedObject = null;
            }
        }
    }
}
