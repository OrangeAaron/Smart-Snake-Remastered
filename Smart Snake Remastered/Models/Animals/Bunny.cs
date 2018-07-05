using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Snake_Remastered.Models.Animals
{
    class Bunny : Animal
    {
        private const int STARTLENGTH = 4;
        private int BirthCooldown = 20;
        public override Color Visual
        {
            get
            {
                return Color.Blue;
            }
        }
        public BunnyBody Butt;

        public Bunny(Bunny firstParent, Bunny secondParent, Grid currentGrid)
        {
            ExtensionGenes = firstParent.ExtensionGenes;

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


            InitializeAnimal(
                this.Stamina,
                ExtensionGenes.Next(4).ToDirection(),
                GetFullHealth(this.Stamina));

            Location = GetEggSpot(currentGrid, firstParent.Location);
            this.Butt = new BunnyBody(this, this.Location);
        }


        public Bunny(uint senses, uint stamina, uint boldness, Grid currentGrid)
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
            this.Butt = new BunnyBody(this, this.Location);
        }


        public Bunny()
        {
        }

        public override Animal GiveBirth(Animal father, Grid currentGrid)
        {
            return new Bunny(this, (Bunny)father, currentGrid);
        }

        public override List<Birth> Act(List<Animal> lifeforms, Grid currentGrid)
        {
            var newLifeList = new List<Birth>();
            var oldList = GetAllLocations();
            uint motivation = (uint)(this.ExtensionGenes.Next((int)(this.Boldness / 2)));
            var sightResults = LookAhead(currentGrid);
            Scaredness += (uint)sightResults[1];
            var panic = sightResults[0] ;
            motivation += (uint)sightResults[2];

            if ((panic > motivation || Age % 10 == 0) && CheckEnergy() > 10)
            {
                ChangeDirection();
                newLifeList = Move(lifeforms, currentGrid);
                ExpendEnergy(10);
                var newList = GetAllLocations();
                currentGrid.UpdateLocationInGrid(oldList, newList, this);
            }
            else if((Scaredness > motivation) && CheckEnergy() > 4)
            {
                newLifeList = Move(lifeforms, currentGrid);
                var newList = GetAllLocations();
                ExpendEnergy(4);
                currentGrid.UpdateLocationInGrid(oldList, newList, this);
            }
            GetOlder();
            return newLifeList;
        }

        public List<Point> GetSightPoints(Grid currentGrid)
        {
            var farToPeripheralRatio = 1;
            var result = new List<Point>();

            switch (this.Direction)
            {
                case Direction.North:
                    int startYCoor = 0;
                    int startXCoor = 0;
                    int endYCoor = 0;
                    int endXCoor = 0;
                    startYCoor = (int)(this.Location.Y - this.Vision - 1);
                    startXCoor = (int)(this.Location.X + (this.Vision / (farToPeripheralRatio * 2)));
                    endYCoor = (int)(startYCoor + this.Vision);
                    endXCoor = (int)(startXCoor - (this.Vision / (farToPeripheralRatio)));
                    for (int y = startYCoor; y < endYCoor; y++)
                    {
                        for (int x = startXCoor; x > endXCoor; x--)
                        {
                            result.Add(currentGrid.SendToOtherSideOfScreen(new Point(x, y)));
                        }
                    }
                    break;
                case Direction.East:
                    startYCoor = (int)((this.Vision / (farToPeripheralRatio * 2)) + this.Location.Y);
                    startXCoor = (int)(this.Vision + this.Location.X + 1);
                    endYCoor = (int)(startYCoor - (this.Vision / (farToPeripheralRatio)));
                    endXCoor = (int)(startXCoor - this.Vision);
                    for (int y = startYCoor; y > endYCoor; y--)
                    {
                        for (int x = startXCoor; x > endXCoor; x--)
                        {
                            result.Add(currentGrid.SendToOtherSideOfScreen(new Point(x, y)));
                        }
                    }
                    break;
                case Direction.South:
                    startYCoor = (int)(this.Vision + this.Location.Y + 1);
                    startXCoor = (int)(this.Location.X - (this.Vision / (farToPeripheralRatio * 2)));
                    endYCoor = (int)(startYCoor - this.Vision);
                    endXCoor = (int)(startXCoor + (this.Vision / (farToPeripheralRatio)));
                    for (int y = startYCoor; y > endYCoor; y--)
                    {
                        for (int x = startXCoor; x < endXCoor; x++)
                        {
                            result.Add(currentGrid.SendToOtherSideOfScreen(new Point(x, y)));
                        }
                    }
                    break;
                case Direction.West:
                    startYCoor = (int)(this.Location.Y - (this.Vision / (farToPeripheralRatio * 2)));
                    startXCoor = (int)(this.Location.X - this.Vision - 1);
                    endYCoor = (int)(startYCoor + (this.Vision / (farToPeripheralRatio)));
                    endXCoor = (int)(startXCoor + this.Vision);
                    for (int y = startYCoor; y < endYCoor; y++)
                    {
                        for (int x = startXCoor; x < endXCoor; x++)
                        {
                            result.Add(currentGrid.SendToOtherSideOfScreen(new Point(x, y)));
                        }
                    }
                    break;
            }
            return result;
        }

        public int[] LookAhead(Grid currentGrid)
        {
            var result = new int[3] { 0, 0, 0 };
            try
            {
                var points = GetSightPoints(currentGrid);
                foreach (Point pToCheck in points)
                {
                    if (currentGrid[pToCheck.X, pToCheck.Y].Equivalent(new Snake()))
                    {
                        result[0] = 20;
                        result[1] += 5;
                    }
                    else if (currentGrid[pToCheck.X, pToCheck.Y].Equivalent(new Bunny()))
                    {
                        result[2] += 40;
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return result;
        }

        public override List<Point> GetAllLocations()
        {
            var result = new List<Point>();

            if (this != null)
            {
                result.Add(this.Location);
                BunnyBody current = this.Butt;
                    result.Add(current.Location);
            }
            return result;
        }

        public override void GetOlder()
        {
            Age++;
            _energy += 2;
            if (Scaredness > 0) Scaredness -= ((Scaredness / 9) + (Scaredness % 10));
            if (BirthCooldown > 0) BirthCooldown--;
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
                    foreach (Animal b in lifeforms)
                    {
                        if (b is Bunny)
                        {
                            var bunny = (Bunny)b;
                            if (nextLocation == bunny.Location)
                            {

                                if (BirthCooldown <= 0)
                                {
                                    newLifeList.Add(new Birth(this, bunny));
                                    BirthCooldown += 5;
                                }
                                ChangeDirection();
                                break;
                            }
                        }
                    }
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
            if (this.Butt != null)
            {
                BunnyBody current = this.Butt;
                var tempLocation = current.Location;
                current.Location = this.Location;
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
            if (timeout >= 500) throw new Exception("Not enough space for bunny.");
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

        public class BunnyBody
        {
            public Bunny Head;
            public BunnyBody Butt;
            public Point Location;

            public BunnyBody(Bunny hd, Point loc)
            {
                Location = loc;
                Head = hd;
                Butt = null;
            }
        }
    }
}
