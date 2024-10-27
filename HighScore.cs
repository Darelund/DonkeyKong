using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class HighScore
    {
        private static List<Score> _highScores = new List<Score>();
        private const string ScoreFilePath = "Content/HighScore.txt";
        //Read high score list from file
        //public static void LoadScores()
        //{
        //    //Todo Load scores from saved file

        //    _highScores.Add(new Score("Daniel", 44, 1));
        //    _highScores.Add(new Score("Harry", 84, 2));
        //    _highScores.Add(new Score("Diane", 176, 4));
        //    _highScores.Add(new Score("Moira", 29, 1));
        //}
        public static void LoadScores()
        {
            List<string> fileLines = FileManager.ReadFromFile(ScoreFilePath);
            foreach (var line in fileLines)
            {
                List<string> scoreLine = line.Split(' ').ToList();
                string name = scoreLine[0];
                int points = int.Parse(scoreLine[1]);
                int level = int.Parse(scoreLine[2]);
                _highScores.Add(new Score(name, points, level));
            }
        }
        private static void SaveScores()
        {
            using (StreamWriter writer = new StreamWriter(ScoreFilePath))
            {
                foreach (var score in _highScores)
                {
                    writer.WriteLine($"{score.Name} {score.Points} {score.Levels}");
                }
            }
        }
        public static void DisplayScores()
        {
            var sortedScore = from score in _highScores orderby score.Points descending select score;
            Debug.WriteLine("Name    | Points    | levels");
            foreach (var score in sortedScore)
            {
                Debug.WriteLine($"{score.Name,-8}{score.Points,-6}{score.Levels,-6}");
            }
        }
        public static void UpdateScore(string name, int points, int level)
        {
            var existingScore = _highScores.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (existingScore.Name != null)
            {
                existingScore.UpdatePoints(points);
                existingScore.UpdateLevel(level);
                //// Update score and level if the new score is higher
                //if (points > existingScore.Points)
                //{
                //    _highScores.Remove(existingScore);
                //    _highScores.Add(new Score(name, points, level));
                //}
            }
            else
            {
                // Add new score
                _highScores.Add(new Score(name, points, level));
            }

            SaveScores(); // Save changes to file immediately
        }
        public class Score
        {
            public readonly string Name;
            public int Points { get; private set; }
            public int Levels { get; private set; }
            public Score(string name, int points, int levels)
            {
                Name = name;
                Points = points;
                Levels = levels;
            }
            public void UpdatePoints(int points)
            {
                if(Points < points)
                {
                    Points = points;
                }
            }
            public void UpdateLevel(int level)
            {
                if (Levels < level)
                {
                    Levels = level;
                }
            }
        }
    }
}
