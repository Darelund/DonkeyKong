using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public enum LevelType
    {
        ReachTarget,
        RemoveTarget
    }
    public class LevelManager
    {
        //Probably will rework this to use the state design pattern if I have time.
        //Never did it but I wonder how that would have looked?
        //If I made this into a state machine and then had a class for each level

       public static List<Level> Levels = new List<Level>();
        public static Level GetCurrentLevel => Levels[_levelIndex];
        private static int _levelIndex = 0;

        public static int NameIndex = 0; //What is this doing?

        //public static void LoadContent()
        //{

        //}
        //public static void Update(GameTime gameTime)
        //{

        //}
        public static void Update(GameTime gameTime)
        {
            if (GetCurrentLevel.CheckLevelCompletion())
            {
                GetCurrentLevel.LevelCompleted = true;
            }
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            GetCurrentLevel.Draw(spriteBatch);
        }
        /// <summary>
        /// Creates a new level from the file you provide and x tiles based on the list you provided
        /// </summary>
        /// <param name="file">A 2D Grid Texture file which is how you want your map to look</param>
        /// <param name="tileTexture">A list where each element represents one tile in your map</param>
        public static void AddLevel(string file, LevelType type, Vector2 startPosition, List<(char TileName, Texture2D tileTexture, TileType type, Color tileColor)> tileTexture)
        {
            Level newLevel = null;
            switch (type)
            {
                case LevelType.ReachTarget:
                    newLevel = new ReachTargetLevel('F');
                    newLevel.CreateLevel(file, startPosition, tileTexture);
                    var l1 = newLevel as ReachTargetLevel;
                    l1.SetTarget();
                    break;
                case LevelType.RemoveTarget:
                    newLevel = new RemoveTargetLevel('T');
                    newLevel.CreateLevel(file, startPosition, tileTexture);
                    var t1 = newLevel as RemoveTargetLevel;
                    t1.SetTargets();
                    break;
            }
            // newLevel.CreateLevelGameObjects("Content/GameObjectsInLevel1.txt");
            Levels.Add(newLevel);
        }
       
        public static void ChangeLevel(int newLevel)
        {
            _levelIndex += newLevel;
            if(_levelIndex <= Levels.Count)
            {
                GameManager.ChangeGameState(GameManager.GameState.Playing);
            }
        }
        public static void Restart()
        {
            _levelIndex = 0;
            GameManager.ChangeGameState(GameManager.GameState.Playing);
        }
    }
}
