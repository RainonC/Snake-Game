using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Walls walls = new Walls(80, 25);
            walls.Draw();

            // Отрисовка точек			
            Point p = new Point(4, 5, '*');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();

            FoodCreator foodCreator = new FoodCreator(80, 25, '$');
            Point food = foodCreator.CreateFood();
            food.Draw();

            int score = 0; // переменная для хранения очков
            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }
                if (snake.Eat(food))
                {
                    score += 10; // добавляем 10 очков за съеденную еду
                    food = foodCreator.CreateFood();
                    food.Draw();
                }
                else
                {
                    snake.Move();
                }

                Thread.Sleep(100);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
            }
            // Записываем очки в файл и считываем их
            List<int> scores = new List<int>();
            if (File.Exists("scores.txt"))
            {
                // Читаем очки из файла, если файл существует
                string[] lines = File.ReadAllLines("scores.txt");
                foreach (string line in lines)
                {
                    if (int.TryParse(line, out int scoreFromFile))
                    {
                        scores.Add(scoreFromFile);
                    }
                }
            }
            // Добавляем текущие очки в список и сортируем его по убыванию
            scores.Add(score);
            scores.Sort();
            scores.Reverse();

            // Сохраняем отсортированные очки в файл
            using (StreamWriter sw = new StreamWriter("scores.txt"))
            {
                foreach (int s in scores)
                {
                    sw.WriteLine(s);
                }
            }

            WriteGameOver();
            Console.ReadLine();
        }

        static void WriteGameOver()
        {
            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            WriteText("Game Over!", xOffset + 1, yOffset++);
            yOffset++;
            WriteText("Made by Rainon Kaska", xOffset + 2, yOffset++);
            WriteText("============================", xOffset, yOffset++);
        }

        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }
    }
}