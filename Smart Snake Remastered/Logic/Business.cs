using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Smart_Snake_Remastered.Models;
using static Smart_Snake_Remastered.Models.Animal;

namespace Smart_Snake_Remastered.Logic
{
    public static class Business
    {
        public static List<Animal> CreateLife(Grid currentGrid, int numberOfSnakes)
        {
            List<Animal> life = new List<Animal>();
            for (int i = 0; i < numberOfSnakes; i++)
            {
                var snake = new Snake(20, 20, 20, 20, currentGrid);
                currentGrid[snake.Location.X, snake.Location.Y] = snake.Visual;
                life.Add(snake);
            }
            return life;
        }
        public static void Live(List<Animal> lifeforms, Grid environment)
        {
            try
            {
                if (lifeforms.Count != 0)
                {
                    var newLives = new List<Birth>();
                    var newDeaths = new List<Animal>();
                    foreach (Animal animal in lifeforms)
                    {
                        newLives = animal.Act(lifeforms, environment);
                        if (animal.Dead == true)
                            newDeaths.Add(animal);
                    }
                    foreach(Birth birth in newLives)
                    {
                       lifeforms.Add(birth.Mother.GiveBirth(birth.Father, environment));
                    }
                    foreach (Animal deadAnimal in newDeaths)
                    {
                        lifeforms.Remove(deadAnimal);
                        environment.DeleteFromGrid(deadAnimal.GetAllLocations());
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }
    }
}