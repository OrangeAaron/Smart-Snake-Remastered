using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Smart_Snake_Remastered.Models;
using static Smart_Snake_Remastered.Models.Animal;
using Smart_Snake_Remastered.Models.Animals;

namespace Smart_Snake_Remastered.Logic
{
    public static class Business
    {
        public static List<Animal> CreateLife(Grid currentGrid, int numberOfSnakes, int numberOfBunnies)
        {
            List<Animal> life = new List<Animal>();
            for (int i = 0; i < numberOfSnakes; i++)
            {
                var snake = new Snake(20, 20, 20, currentGrid);
                currentGrid[snake.Location.X, snake.Location.Y] = snake.Visual;
                life.Add(snake);
            }

            for (int i = 0; i < numberOfBunnies; i++)
            {
                var bunny = new Bunny(5, 40, 0, currentGrid);
                currentGrid[bunny.Location.X, bunny.Location.Y] = bunny.Visual;
                life.Add(bunny);
            }
            return life;
        }
        public static Animal Live(List<Animal> lifeforms, Grid environment)
        {
            Animal smartestAnimal = null;
            try
            {
                if (lifeforms.Count != 0)
                {
                    var newLives = new List<Birth>();
                    var newDeaths = new List<Animal>();
                    foreach (Animal animal in lifeforms)
                    {
                        newLives.AddRange(animal.Act(lifeforms, environment));
                        if (animal is Snake && (smartestAnimal == null || animal.Age > smartestAnimal.Age))
                            smartestAnimal = animal;
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
            return smartestAnimal;
        }
    }
}