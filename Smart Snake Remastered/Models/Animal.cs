using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Smart_Snake_Remastered.Models
{

    public abstract class Animal
    {
        public abstract Color Visual { get;}


        //Genetics
        public uint Vision;
        public uint Stamina;
        public uint Intelligence;
        public uint Boldness;
        public Random ExtensionGenes;

        //Current State
        public bool Dead = false;
        public Point Location;
        public Direction Direction;
        protected uint _energy;
        protected uint _age;
        protected uint _health;


        public virtual void GetOlder()
        {
            _age++;
            _energy++;
        }

        public uint CheckEnergy()
        {
            return _energy;
        }

        public abstract List<Point> GetAllLocations();

        public void InitializeAnimal(uint energy, Direction dir, uint health)
        {
            _energy = energy;
            Direction = dir;
            _health = health;
        }

        public void ChangeDirection()
        {
            var leftOrRight = ExtensionGenes.Next(2);
            var dir = Direction.ToInt();
            if (leftOrRight == 1)
                if (dir == 0) Direction = Direction.West;
                else Direction = (dir - 1).ToDirection();
            else
                if (dir == 3) Direction = Direction.North;
                else Direction = (dir + 1).ToDirection();
        }

        public void ExpendEnergy(uint amount)
        {
            var newEnergy = _energy - amount;
            if (newEnergy >= 0)
            {
                _energy = newEnergy;
            }
            else throw new InvalidOperationException("Animal tried to expend energy it couldn't have.");
        }

        public void MotivatedExpendEnergy(List<Animal> lifeforms, Grid currentGrid, uint amount)
        {
            var newEnergy = _energy - amount;
            if (newEnergy >= 0)
            {
                _energy = newEnergy;
            }
            else
            {
                var subtractHealthForEnergy = newEnergy * -1;
                LowerHealth(lifeforms, currentGrid,(uint)subtractHealthForEnergy);
                _energy = 0;
            }
        }

        public bool LowerHealth(List<Animal> lifeforms, Grid currentGrid , uint amount)
        {
            var newHealth = _health - amount;
            if (newHealth >= 0)
            {
                _health = newHealth;
                return true;
            }
            else
            {
                return false;
            }
        }

        public abstract Animal GiveBirth(Animal father, Grid currentGrid);

        public abstract List<Birth> Act(List<Animal> lifeforms, Grid currentGrid);

        public Point NextLocation(Grid currentGrid)
        {
            Point newLocation = new Point(Location.X, Location.Y);

            switch (Direction)
            {
                case Direction.North:
                    newLocation.Y = Location.Y - 1;
                    break;
                case Direction.East:
                    newLocation.X = Location.X + 1;
                    break;
                case Direction.South:
                    newLocation.Y = Location.Y + 1;
                    break;
                case Direction.West:
                    newLocation.X = Location.X - 1;
                    break;
                default:
                    throw new Exception("Unsupported direction");
            }
            if (currentGrid.WithinBounds(newLocation))
            {
                return newLocation;
            }
            else
            {
                return currentGrid.SendToOtherSideOfScreen(newLocation);
            }
        }

        public class Birth
        {
            public Animal Mother;
            public Animal Father;

            public Birth(Animal m, Animal f)
            {
                Mother = m;
                Father = f;
            }
        }
    }
}
