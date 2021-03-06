﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Threading;

namespace Smart_Snake_Remastered.Models
{
    public class Grid
    {
        //public GridBox[,] BoxMatrix;
        public UInt64 Lifetime;
        public Bitmap World;
        public Random GridSeed;
        public Mutex WorldLock = new Mutex();


        public Color this[int x, int y]
        {
            get
            {
                WorldLock.WaitOne();
                var pixel = World.GetPixel(x, y);
                WorldLock.ReleaseMutex();
                return pixel;
            }
            set
            {
                WorldLock.WaitOne();
                World.SetPixel(x, y, value);
                WorldLock.ReleaseMutex();
            }
        }

        public Grid() : this(new Size(10, 10)) {}
        
        public Grid(Size sizeXbyY)
        {
            Lifetime = 0;
            WorldLock.WaitOne();
            World = new Bitmap(Properties.Resources.arena, sizeXbyY);
            WorldLock.ReleaseMutex();
            GridSeed = new Random(DateTime.Now.Millisecond + DateTime.Now.Day + DateTime.Now.Year);
        }
        
        public void DeleteFromGrid(List<Point> deleteList)
        {
            foreach (Point p in deleteList.Distinct())
            {
                this[p.X, p.Y] = Main.empty;
            }
        }


        public void UpdateLocationInGrid(List<Point> deleteList, List<Point> addList, Animal ani)
        {
            foreach (Point p in deleteList.Except(addList))
            {
                this[p.X, p.Y] = Main.empty;
            }
            foreach (Point p in addList.Except(deleteList))
            {
                this[p.X, p.Y] = ani.Visual;
            }
        }

        public void InitializeTo(Color color)
        {
            WorldLock.WaitOne();
            for (int x = 0; x < World.Width; x++)
            {
                for (int y = 0; y < World.Height; y++)
                {
                    World.SetPixel(x, y, color);
                }
            }
            WorldLock.ReleaseMutex();
        }

        public bool IsAvailable(Point location)
        {
            if (!this[location.X, location.Y].HasObject() && WithinBounds(location)) return true;
            return false;
        }
        
        public int GetBorderIndex(int index)
        {
            var result = 0;
            WorldLock.WaitOne();
            if (index == 0)
            {
                result = World.Width - 1;
            }
            else
            {
                result = World.Height - 1;
            }
            WorldLock.ReleaseMutex();
            return result;
        }

        public Boolean WithinBounds(Point location)
        {
            var maxXCoordinate = this.GetBorderIndex(0);
            var maxYCoordinate = this.GetBorderIndex(1);
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
            Point newLocation = new Point(initialLocation.X, initialLocation.Y);
            if (initialLocation.X > this.GetBorderIndex(0))
                newLocation.X = (initialLocation.X - this.GetBorderIndex(0)) - 1;
            else if (initialLocation.X < 0)
            newLocation.X = (this.GetBorderIndex(0) - (initialLocation.X * -1));
            if (initialLocation.Y > this.GetBorderIndex(1))
            newLocation.Y = (initialLocation.Y - this.GetBorderIndex(1)) - 1;
            else if (initialLocation.Y < 0)
            newLocation.Y = (this.GetBorderIndex(1) - (initialLocation.Y * -1)) + 1;
            if (newLocation.X < 0 || newLocation.X > this.GetBorderIndex(0) || newLocation.Y < 0 || newLocation.Y > this.GetBorderIndex(1))
                newLocation = SendToOtherSideOfScreen(newLocation);
            return newLocation;
        }
    }
}
