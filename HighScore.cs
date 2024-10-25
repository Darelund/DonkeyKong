using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class HighScore
    {
        private static List<ScoreStruct> _highScores = new List<ScoreStruct>();
        //Read high score list from file
        public static void LoadScores()
        {
            //Todo Load scores from saved file

            _highScores.Add(new ScoreStruct("Daniel", 44, 1));
            _highScores.Add(new ScoreStruct("Harry", 84, 2));
            _highScores.Add(new ScoreStruct("Diane", 176, 4));
            _highScores.Add(new ScoreStruct("Moira", 29, 1));
        }
        public static void DisplayScores()
        {
            var sortedScore = from score in _highScores orderby score.Points descending select score;
            // _highScores.OrderByDescending(_highScores => _highScores.Points).ToList();
            Debug.WriteLine("Name | Points  | levels");
            foreach (var score in sortedScore)
            {
                Debug.WriteLine($"{score.Name,8}{score.Points,8}{score.Levels,8}");
            }
        }

        //Struct
        public struct ScoreStruct
        {
            public readonly string Name;
            public readonly int Points;
            public readonly int Levels;
            public ScoreStruct(string name, int points, int levels)
            {
                Name = name;
                Points = points;
                Levels = levels;
            }
        }
        ////Tuple
        //(string Name, int Points, int Levels) ScoreTuple;

        ////Class
        //public class ScoreClass
        //{
        //    public readonly string Name;
        //    public readonly int Points;
        //    public readonly int Levels;
        //    public ScoreClass(string name, int points, int levels)
        //    {
        //        Name = name;
        //        Points = points;
        //        Levels = levels;
        //    }
        //}
        //Record
        //??? Har glömt hur man gör
    }
}
