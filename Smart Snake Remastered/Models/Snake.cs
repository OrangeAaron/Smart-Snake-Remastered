using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Smart_Snake_Remastered.Logic;

namespace Smart_Snake_Remastered.Models
{


    class Snake
    {
        //Genetics
        public uint Vision;
        public uint Stamina;
        public uint Intelligence;
        public uint Boldness;
        public Random ExtensionGenes;

        //Current State
        public Point Location;
        private Direction _direction;
        private uint _energy;
        private uint _health;

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
            _direction = ExtensionGenes.Next(4).ToDirection();
            _energy = this.Stamina;
            _health = GetFullHealth(this.Stamina);
            Location = GetEggSpot(currentGrid, firstParent.Location);
        }

        public Snake(uint vision, uint stamina, uint intelligence, uint boldness, Grid currentGrid)
        {
            this.Vision = vision;
            this.Stamina = stamina;
            this.Intelligence = intelligence;
            this.Boldness = boldness;
            ExtensionGenes = new Random(currentGrid.GridSeed.Next(1000000));
            _energy = stamina;
            _direction = ExtensionGenes.Next(4).ToDirection();
            _health = GetFullHealth(stamina);
            Location = GetEggSpot(currentGrid);
        }

        public void Act(Grid currentGrid)
        {
            var motivation = this.ExtensionGenes.Next(101) + (this.Boldness / 10);
            if (motivation > 50)
            {
                ChangeDirection();
                Move();
            }
            else if(motivation > 20)
            {
                Move();
            }
            else if (motivation > 10)
            {
                ChangeDirection();
            }
        }

        private void Move(Grid currentGrid)
        {
            Location = Business.MoveDirection(Location, _direction);
            _energy--;
        }

        private void ChangeDirection()
        {
            _direction = ExtensionGenes.Next(4).ToDirection();
            _energy--;
        }

        private Point GetEggSpot(Grid currentGrid)
        {
            Point eggLocation = new Point();
            do
            {
                eggLocation.X = currentGrid.GridSeed.Next(currentGrid.BoxMatrix.GetLength(0) + 1);
                eggLocation.Y = currentGrid.GridSeed.Next(currentGrid.BoxMatrix.GetLength(1) + 1);
            } while (currentGrid.IsAvailable(eggLocation));
            return eggLocation;
        }

        private Point GetEggSpot(Grid currentGrid, Point initialLocation)
        {
            Point eggLocation = new Point(initialLocation.X, initialLocation.Y);

            int radius = 1;
            int directionFlag = 1;

            do
            {
                for (int x = 0; (x <= radius) && (!currentGrid.IsAvailable(eggLocation)); x++)
                {
                    eggLocation.X = eggLocation.X + radius;
                    if (!currentGrid.WithinBounds(eggLocation)) throw new Exception("No room for an egg.");
                }

                for (int y = 0; (y <= radius) && (!currentGrid.IsAvailable(eggLocation)); y++)
                {
                    eggLocation.Y = eggLocation.Y + radius;
                    if (!currentGrid.WithinBounds(eggLocation)) throw new Exception("No room for an egg.");
                }
                radius = (radius + 1);
                directionFlag = directionFlag * -1;
            } while (!currentGrid.IsAvailable(eggLocation));

            return eggLocation;
        }
        private uint GetFullHealth(uint stamina)
        {
            return 100 + (stamina / 10);
        }
    }
}
