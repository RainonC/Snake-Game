using System;
using System.Collections.Generic;
using System.IO;

namespace Snake
{
    class ScoreManager
    {
        private int score;
        private List<int> scoresList;

        public ScoreManager()
        {
            score = 0;
            scoresList = new List<int>();
            LoadScores();
        }

        public void AddScore(int points)
        {
            score += points;
        }

        public int GetScore()
        {
            return score;
        }

        public void SaveScore()
        {
            scoresList.Add(score);
            scoresList.Sort();
            scoresList.Reverse();
            using (StreamWriter writer = new StreamWriter("scores.txt"))
            {
                foreach (int score in scoresList)
                {
                    writer.WriteLine(score);
                }
            }
        }

        private void LoadScores()
        {
            if (File.Exists("scores.txt"))
            {
                using (StreamReader reader = new StreamReader("scores.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (int.TryParse(line, out int score))
                        {
                            scoresList.Add(score);
                        }
                    }
                }
            }
        }
    }
}
