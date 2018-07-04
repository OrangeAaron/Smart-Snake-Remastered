using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Smart_Snake_Remastered.Models;

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
                currentGrid[snake.Location.X, snake.Location.Y] = snake;
                life.Add(snake);
            }
            return life;
        }
        public static void Live(List<Animal> lifeforms, Grid environment)
        {
            try
            {
                foreach (Animal animal in lifeforms)
                {
                    animal.Act(lifeforms, environment);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }
    }
}