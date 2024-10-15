using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class LevelManager
    {
        //Probably will rework this to use the state design pattern if I have time.
       // public enum GameLevels
       private static List<Level> Levels = new List<Level>();
        public static Level GetCurrentLevel => Levels[_levelIndex];
        private static int _levelIndex = 0;
        public static int NameIndex = 0;
        //public static void LoadContent()
        //{

        //}
        //public static void Update(GameTime gameTime)
        //{

        //}
        public static void Draw(SpriteBatch spriteBatch)
        {
            GetCurrentLevel.Draw(spriteBatch);
        }
        /// <summary>
        /// Creates a new level from the file you provide and x tiles based on the list you provided
        /// </summary>
        /// <param name="file">A 2D Grid Texture file which is how you want your map to look</param>
        /// <param name="tileTexture">A list where each element represents one tile in your map</param>
        public static void AddLevel(string file, Vector2 startPosition, List<(char TileName, Texture2D tileTexture, bool notWalkable)> tileTexture)
        {
            Level newLevel = new Level();
            newLevel.CreateLevel(file, startPosition, tileTexture);
            Levels.Add(newLevel);
        }
        public static void ChangeLevel(int newLevel)
        {
            _levelIndex = newLevel;
        }
    }
}
