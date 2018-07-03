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
        public static Color color = Color.Black;

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
        private uint _energy;
        private uint _age;
        private uint _health;


        public void GetOlder()
        {
            _age++;
                _energy++;
        }

        public uint CheckEnergy()
        {
            return _energy;
        }

        public void UpdateLocationInGrid(int oldLocationX, int oldLocationY, Grid currentGrid)
        {
            currentGrid[oldLocationX, oldLocationY] = Main.empty;
            currentGrid[Location.X, Location.Y] = this;
        }


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

        public void MotivatedExpendEnergy(uint amount)
        {
            var newEnergy = _energy - amount;
            if (newEnergy >= 0)
            {
                _energy = newEnergy;
            }
            else
            {
                var subtractHealthForEnergy = newEnergy * -1;
                LowerHealth((uint)subtractHealthForEnergy);
                _energy = 0;
            }
        }

        public bool LowerHealth(uint amount)
        {
            var newHealth = _health - amount;
            if (newHealth >= 0)
            {
                _health = newHealth;
                return true;
            }
            else
            {
                Die();
                return false;
            }
        }
        
        public static implicit operator Color(Animal a)
        {
            var col = Color.Black;
            var @switch = new Dictionary<Type, Action> {
                        {typeof(Snake), () =>{col = Snake.color;}}
                         };
            @switch[a.GetType()]();

            return col;
        }

        public void Die()
        {
            Dead = true;
        }

        public abstract void Act(Grid currentGrid);
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
    }
}
