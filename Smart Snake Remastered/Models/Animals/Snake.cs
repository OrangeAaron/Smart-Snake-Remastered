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
        private const int STARTLENGTH = 4;
        public override Color Visual {
            get
            {
                return Color.DarkRed;
            }
        }
            public SnakeBody NextBody;

        public Snake(Snake firstParent, Snake secondParent, Grid currentGrid)
        {
            decimal firstParentRatio = (decimal)currentGrid.GridSeed.Next(101) / (decimal)100;
            decimal secondParentRatio = 1 - firstParentRatio;

            var vis = ((firstParent.Vision * firstParentRatio)
                + (secondParent.Vision * secondParentRatio) + this.RandomMutation());
            if (vis < 0) this.Vision = 0;
            else this.Vision = (uint)vis;

            var sta = (firstParent.Stamina * firstParentRatio)
                + (secondParent.Stamina * secondParentRatio) + this.RandomMutation();
            if (sta < 0) this.Stamina = 0;
            else this.Stamina = (uint)sta;

            var hear = ((firstParent.Hearing * firstParentRatio)
                + (secondParent.Hearing * secondParentRatio) + this.RandomMutation());
            if (hear < 0) this.Hearing = 0;
            else this.Hearing = (uint)hear;

            var smel = ((firstParent.Smell * firstParentRatio)
                  + (secondParent.Smell * secondParentRatio) + this.RandomMutation());
            if (smel < 0) this.Smell = 0;
            else this.Smell = (uint)smel;

            var bol = ((firstParent.Boldness * firstParentRatio)
                + (secondParent.Boldness * secondParentRatio) + this.RandomMutation());
            if (bol < 0) this.Boldness = 0;
            else this.Boldness = (uint)bol;


            ExtensionGenes = firstParent.ExtensionGenes;
            InitializeAnimal(
                this.Stamina,
                ExtensionGenes.Next(4).ToDirection(),
                GetFullHealth(this.Stamina));

            Location = GetEggSpot(currentGrid, firstParent.Location);
            AddLength(STARTLENGTH - 1);
        }
        

        public Snake(uint senses, uint stamina, uint boldness, Grid currentGrid)
        {
            this.Vision = senses;
            this.Hearing = senses;
            this.Smell = senses;
            this.Stamina = stamina;
            this.Boldness = boldness;
            ExtensionGenes = new Random(currentGrid.GridSeed.Next(1000000));
            InitializeAnimal(
                this.Stamina,
                ExtensionGenes.Next(4).ToDirection(),
                GetFullHealth(this.Stamina));
            Location = GetEggSpot(currentGrid);
            AddLength(STARTLENGTH - 1);
        }

        public override Animal GiveBirth(Animal father, Grid currentGrid)
        {
            return new Snake(this, (Snake)father, currentGrid);
        }

        public override List<Birth> Act(List<Animal> lifeforms, Grid currentGrid)
        {
            var newLifeList = new List<Birth>();
            var oldList = GetAllLocations();
            uint motivation = (uint)(this.ExtensionGenes.Next((int)(this.Boldness/10)));


            
            if (motivation > 50 && CheckEnergy() > 10)
            {
                ChangeDirection();
                newLifeList = Move(lifeforms, currentGrid);
                    ExpendEnergy(10);
                    var newList = GetAllLocations();
                    currentGrid.UpdateLocationInGrid(oldList, newList, this);
            }
            else
            {
                newLifeList = Move(lifeforms, currentGrid);
                var newList = GetAllLocations();
                currentGrid.UpdateLocationInGrid(oldList, newList, this);
            }
            GetOlder();
            return newLifeList;
        }

        public override List<Point> GetAllLocations()
        {
            var result = new List<Point>();

            if (this != null)
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
            Age++;
            _energy++;
            if (Age % 15 == 0)
            {
                AddLength();
            }
        }

        public void AddLength()
        {

            Snake head = this;
            if (head.NextBody != null)
            {
                SnakeBody current = head.NextBody;
                while (current.NextBody != null)
                {
                    current = current.NextBody;
                }
                SnakeBody toAdd = new SnakeBody(this, current.Location);
                current.NextBody = toAdd;
            }
            else
            {
                SnakeBody toAdd = new SnakeBody(this, Location);
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

        private List<Birth> Move(List<Animal> lifeforms, Grid currentGrid)
        {
            var newLifeList = new List<Birth>();
            var nextLocation = this.NextLocation(currentGrid);
            var nextLocationObject = currentGrid[nextLocation.X, nextLocation.Y];
            if (nextLocationObject.HasObject())
            {
                if (nextLocationObject.Equivalent(this))
                {
                    var isSnakeHead = false;
                    foreach (Snake s in lifeforms)
                    {
                        if (nextLocation == s.Location)
                        {
                            newLifeList.Add(new Birth(this, s));
                            ChangeDirection();
                            isSnakeHead = true;
                            break;
                        }
                    }
                    if(!isSnakeHead) this.Dead = true;
                }
            }
            else
            {
                MoveBodyAlongOverHead();
                Location = nextLocation;
            }
            return newLifeList;
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
            var timeout = 0;
            do
            {
                timeout++;
                eggLocation.X = currentGrid.GridSeed.Next(currentGrid.GetBorderIndex(0) + 1);
                eggLocation.Y = currentGrid.GridSeed.Next(currentGrid.GetBorderIndex(1) + 1);

            } while (!currentGrid.IsAvailable(eggLocation) && timeout < 500);
            if (timeout >= 500) throw new Exception("Not enough space for snakes.");
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
