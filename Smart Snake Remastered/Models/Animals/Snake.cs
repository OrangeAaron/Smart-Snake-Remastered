﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Smart_Snake_Remastered;
using Smart_Snake_Remastered.Logic;

namespace Smart_Snake_Remastered.Models
{
    public class Snake : Animal
    {
        private const int STARTLENGTH = 4;
        public new static Color color = Color.DarkRed;
        public SnakeBody NextBody;

        public Snake(Snake firstParent, Snake secondParent, Grid currentGrid)
        {
            decimal firstParentRatio = (decimal)currentGrid.GridSeed.Next(101) / (decimal)100;
            decimal secondParentRatio = 1 - firstParentRatio;

            this.Vision = (uint)((firstParent.Vision * firstParentRatio)
                + (secondParent.Vision * secondParentRatio));

            this.Stamina = (uint)((firstParent.Stamina * firstParentRatio)
                + (secondParent.Stamina * secondParentRatio));

            this.Intelligence = (uint)((firstParent.Intelligence * firstParentRatio)
                + (secondParent.Intelligence * secondParentRatio));

            this.Boldness = (uint)((firstParent.Boldness * firstParentRatio)
                + (secondParent.Boldness * secondParentRatio));

            ExtensionGenes = firstParent.ExtensionGenes;
            InitializeAnimal(
                this.Stamina,
                ExtensionGenes.Next(4).ToDirection(),
                GetFullHealth(this.Stamina));
            
            Location = GetEggSpot(currentGrid, firstParent.Location);
            AddLength(STARTLENGTH - 1);
        }

        public static implicit operator Color(Snake s)
        {
            return Snake.color;
        }

        public Snake(uint vision, uint stamina, uint intelligence, uint boldness, Grid currentGrid)
        {
            this.Vision = vision;
            this.Stamina = stamina;
            this.Intelligence = intelligence;
            this.Boldness = boldness;
            ExtensionGenes = new Random(currentGrid.GridSeed.Next(1000000));
            InitializeAnimal(
                this.Stamina,
                ExtensionGenes.Next(4).ToDirection(),
                GetFullHealth(this.Stamina));
            Location = GetEggSpot(currentGrid);
            AddLength(STARTLENGTH -1);
        }

        public override void Act(List<Animal> lifeforms, Grid currentGrid)
        {
            var oldList = GetAllLocations();
            uint motivation = (uint)(this.ExtensionGenes.Next(101) + (this.Boldness / 10));
            if (motivation > 50 && CheckEnergy() > 10)
            {
                ChangeDirection();
                Move(lifeforms, currentGrid);
                ExpendEnergy(10);
                var newList = GetAllLocations();
                UpdateLocationInGrid(oldList, newList, currentGrid);
            }
            else
            {
                Move(lifeforms, currentGrid);
                var newList = GetAllLocations();
                UpdateLocationInGrid(oldList, newList, currentGrid);
            }
            GetOlder();
        }

        public override void Die(List<Animal> lifeforms, Grid currentGrid)
        {
            var deleteList = new List<Point>();
            deleteList.Add(this.Location);

            var nextPartToDie = this.NextBody;
            this.NextBody = null;
            while (nextPartToDie != null)
            {
                deleteList.Add(nextPartToDie.Location);
                var nextPartToDieTemp = nextPartToDie.NextBody;
                nextPartToDie.NextBody = null;
                nextPartToDie = nextPartToDieTemp;
            }
            lifeforms.Remove(this);
            DeleteBodyFromGrid(deleteList, currentGrid);
        }

        public override List<Point> GetAllLocations()
        {
            var result = new List<Point>();

            if (this == null)
            {
                result.Add(this.Location);
                    SnakeBody current = this.NextBody;
                    while (current.NextBody != null)
                    {
                        result.Add(current.Location);
                        current = current.NextBody;
                    }
            }
            return result;
        }

        public override void GetOlder()
        {
            _age++;
            _energy++;
            if(_age % 15 == 0)
            {
                AddLength();
            }
        }

        public void AddLength()
        {
            SnakeBody toAdd = new SnakeBody(this, Location);

            Snake head = this;
            if (head.NextBody != null)
            {
                SnakeBody current = head.NextBody;
                while(current.NextBody != null)
                {
                    current = current.NextBody;
                }
                current.NextBody = toAdd;
            }
            else
            {
                head.NextBody = toAdd;
            }
        }

        public void AddLength(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                AddLength();
            }
        }

        private void Move(List<Animal> lifeforms, Grid currentGrid)
        {
            var nextLocation = this.NextLocation(currentGrid);
            var nextLocationObject = currentGrid[nextLocation.X, nextLocation.Y];
            if (nextLocationObject.HasObject())
            {
                if (nextLocationObject == this)
                {
                    var isSnakeHead = false;
                    foreach(Snake s in lifeforms)
                    {
                       if (nextLocation == s.Location)
                        {
                            lifeforms.Add(new Snake(this, s, currentGrid));
                            isSnakeHead = true;
                            break;
                        }
                    }
                    if (!isSnakeHead)
                        this.Die(lifeforms, currentGrid);
                }
            }
            else
            {
                MoveBodyAlongOverHead();
                Location = nextLocation;
            }
        }
        
        private void MoveBodyAlongOverHead()
        {
            if (this.NextBody != null)
            {
                SnakeBody current = this.NextBody;
                var tempLocation = current.Location;
                current.Location = this.Location;
                while (current.NextBody != null)
                {
                    current = current.NextBody;
                    var tempLocation2 = current.Location;
                    current.Location = tempLocation;
                    tempLocation = tempLocation2;
                }
            }
        }

        private static Point GetEggSpot(Grid currentGrid)
        {
            Point eggLocation = new Point();
            do
            {
                eggLocation.X = currentGrid.GridSeed.Next(currentGrid.GetBorderIndex(0) + 1);
                eggLocation.Y = currentGrid.GridSeed.Next(currentGrid.GetBorderIndex(1) + 1);
            } while (!currentGrid.IsAvailable(eggLocation));
            return eggLocation;
        }

        private static Point GetEggSpot(Grid currentGrid, Point initialLocation)
        {
            Point eggLocation = new Point(initialLocation.X, initialLocation.Y);

            int radius = 10;
            do
            {
                for (int x = 5; (x < radius) && (!currentGrid.IsAvailable(eggLocation)); x++)
                {
                    eggLocation.X = eggLocation.X + radius;
                    if (!currentGrid.WithinBounds(eggLocation)) throw new Exception("No room for an egg.");
                }

                for (int y = 5; (y < radius) && (!currentGrid.IsAvailable(eggLocation)); y++)
                {
                    eggLocation.Y = eggLocation.Y + radius;
                    if (!currentGrid.WithinBounds(eggLocation)) throw new Exception("No room for an egg.");
                }
                radius = (radius + 1) * -1;
            } while (!currentGrid.IsAvailable(eggLocation));

            return eggLocation;
        }
        private static uint GetFullHealth(uint stamina)
        {
            return 100 + (stamina / 10);
        }


        public static bool operator ==(Snake x, Color y)
        {
            return Snake.color.ToArgb() == y.ToArgb();
        }
        public static bool operator !=(Snake x, Color y)
        {
            return Snake.color.ToArgb() != y.ToArgb();
        }
        public static bool operator ==(Color y, Snake x)
        {
            return Snake.color.ToArgb() == y.ToArgb();
        }
        public static bool operator !=(Color y, Snake x)
        {
            return Snake.color.ToArgb() != y.ToArgb();
        }
        public class SnakeBody
        {
            public Snake Head;
            public SnakeBody NextBody;
            public Point Location;

            public SnakeBody(Snake hd, Point loc)
            {
                Location = loc;
                Head = hd;
                NextBody = null;
            }
        }
    }
}
