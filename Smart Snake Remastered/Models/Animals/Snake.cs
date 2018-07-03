using System;
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
        public static Color color = Color.DarkRed;

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
        }

        public override void Act(Grid currentGrid)
        {
            var oldLocationX = Location.X;
            var oldLocationY = Location.Y;
            uint motivation = (uint)(this.ExtensionGenes.Next(101) + (this.Boldness / 10));
            if (motivation > 50 && CheckEnergy() > 2)
            {
                ChangeDirection();
                Move(currentGrid);
                ExpendEnergy(2);
                UpdateLocationInGrid(oldLocationX, oldLocationY, currentGrid);
            }
            else if(motivation > 20 && CheckEnergy() > 1)
            {
                Move(currentGrid);
                ExpendEnergy(1);
                UpdateLocationInGrid(oldLocationX, oldLocationY, currentGrid);
            }
            else if (motivation > 10 && CheckEnergy() > 1)
            {
                ChangeDirection();
                ExpendEnergy(1);
            }
            GetOlder();
        }
        private void Move(Grid currentGrid)
        {
            var nextLocation = this.NextLocation(currentGrid);
            if (currentGrid.World.GetPixel(nextLocation.X, nextLocation.Y).HasObject())
            {
                if (currentGrid.World.GetPixel(nextLocation.X, nextLocation.Y) is Animal)
                {
                    //do nothing for now
                }
            }
            else
            {
                Location = nextLocation;
            }
        }
        

        private static Point GetEggSpot(Grid currentGrid)
        {
            Point eggLocation = new Point();
            do
            {
                eggLocation.X = currentGrid.GridSeed.Next(currentGrid.World.GetBorderIndex(0) + 1);
                eggLocation.Y = currentGrid.GridSeed.Next(currentGrid.World.GetBorderIndex(1) + 1);
            } while (!currentGrid.IsAvailable(eggLocation));
            return eggLocation;
        }

        private static Point GetEggSpot(Grid currentGrid, Point initialLocation)
        {
            Point eggLocation = new Point(initialLocation.X, initialLocation.Y);

            int radius = 1;
            int directionFlag = 1;

            do
            {
                for (int x = 0; (x < radius) && (!currentGrid.IsAvailable(eggLocation)); x++)
                {
                    eggLocation.X = eggLocation.X + radius;
                    if (!currentGrid.WithinBounds(eggLocation)) throw new Exception("No room for an egg.");
                }

                for (int y = 0; (y < radius) && (!currentGrid.IsAvailable(eggLocation)); y++)
                {
                    eggLocation.Y = eggLocation.Y + radius;
                    if (!currentGrid.WithinBounds(eggLocation)) throw new Exception("No room for an egg.");
                }
                radius = (radius + 1);
                directionFlag = directionFlag * -1;
            } while (!currentGrid.IsAvailable(eggLocation));

            return eggLocation;
        }
        private static uint GetFullHealth(uint stamina)
        {
            return 100 + (stamina / 10);
        }

        public static implicit operator Color(Snake v)
        {
            return color;
        }

        private class SnakeBody
        {
            private Snake Head;
            private SnakeBody NextBody;
        }
    }
}
