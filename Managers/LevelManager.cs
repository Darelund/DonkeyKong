using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        private static int _levelIndex = -1;

        public static void CreateLevels()
        {
            AddLevel(LevelType.ReachTarget);
            AddLevel(LevelType.ReachTarget);
            AddLevel(LevelType.RemoveTarget);
            //  AddLevel(GameFiles.Levels.LEVEL2, LevelType.ReachTarget, GameFiles.Levels.LEVEL2StartPosition, Level.ReadTileDataFromFile(GameFiles.LevelType.REACHTARGETLEVELS), GameFiles.GameObjects.LEVEL2OBJ);
            //  AddLevel(GameFiles.Levels.LEVEL3, LevelType.RemoveTarget,GameFiles.Levels.LEVEL3StartPosition, Level.ReadTileDataFromFile(GameFiles.LevelType.REMOVETARGETLEVELS), GameFiles.GameObjects.LEVEL3OBJ);
        }
        //public static void SetUpLevel()
        //{
        //    GameManager.GameObjects.Clear();
        //    GameManager.GameObjects.AddRange(GetCurrentLevel.GameObjectsInLevel);
        //}
        public static void Update(GameTime gameTime)
        {
            if (GetCurrentLevel.CheckLevelCompletion())
            {
                GetCurrentLevel.LevelCompleted = true;

            }
            GetCurrentLevel.Update();
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
        private static void AddLevel(LevelType type)
        {
            Level newLevel = null;
            switch (type)
            {
                case LevelType.ReachTarget:
                    newLevel = new ReachTargetLevel();
                    break;
                case LevelType.RemoveTarget:
                    newLevel = new RemoveTargetLevel();
                    break;
            }
            // newLevel.CreateLevelGameObjects("Content/GameObjectsInLevel1.txt");
            Levels.Add(newLevel);
        }
        //public static void AddLevel(string file, LevelType type, Vector2 startPosition, List<(char TileName, Texture2D tileTexture, TileType type, Color tileColor)> tileTexture, string fileOfLevelObjects)
        //{
        //    Level newLevel = null;
        //    switch (type)
        //    {
        //        case LevelType.ReachTarget:
        //            newLevel = new ReachTargetLevel('F');
        //            newLevel.CreateLevel(file, startPosition, tileTexture);
        //            newLevel.CreateGameObjects(fileOfLevelObjects);
        //            var l1 = newLevel as ReachTargetLevel;
        //            l1.SetTarget();
        //            break;
        //        case LevelType.RemoveTarget:
        //            newLevel = new RemoveTargetLevel('T');
        //            newLevel.CreateLevel(file, startPosition, tileTexture);
        //            newLevel.CreateGameObjects(GameFiles.GameObjects.LEVEL1OBJ);
        //            var t1 = newLevel as RemoveTargetLevel;
        //            t1.SetTargets();
        //            break;
        //    }
        //    // newLevel.CreateLevelGameObjects("Content/GameObjectsInLevel1.txt");
        //    Levels.Add(newLevel);
        //}
        //Where should I call this for Level 1?
        public static void ActivateLevel(int levelIndex, LevelConfig levelConfig)
        {
            // Deactivate or unload the previous level's objects if needed
            if (_levelIndex >= 0 && GetCurrentLevel != null)
            {
                GetCurrentLevel.UnloadLevel();
                //GetCurrentLevel.UnloadGameObjects();
                GameManager.GameObjects.Clear();
            }

            // Get the new level
            _levelIndex = levelIndex;


            if (_levelIndex < Levels.Count)
            {
                // Create the game objects and tiles for the new level
                GetCurrentLevel.CreateLevel(levelConfig.LevelFile, levelConfig.StartPosition, levelConfig.TileData);
                GetCurrentLevel.CreateGameObjects(levelConfig.GameObjectsFile);
                GetCurrentLevel.SetTarget();
                GameManager.GameObjects.AddRange(GetCurrentLevel.GameObjectsInLevel);

                GameManager.ChangeGameState(GameManager.GameState.Playing);
                // SetUpLevel();
            }
        }
        public static void SpecificLevel(int newLevel)
        {
            //Temporary solution
            int zeroIndexing = 1;
            switch (newLevel)
            {
                case 1: ActivateLevel(1 - zeroIndexing, GameFiles.Levels.Level1); break;
                case 2: ActivateLevel(2 - zeroIndexing, GameFiles.Levels.Level2); break;
                case 3: ActivateLevel(3 - zeroIndexing, GameFiles.Levels.Level3); break;
                case 4: ActivateLevel(4 - zeroIndexing, GameFiles.Levels.Level4); break;
                case 5: ActivateLevel(5 - zeroIndexing, GameFiles.Levels.Level5); break;
                default: ActivateLevel(1 - zeroIndexing, GameFiles.Levels.Level1); break;
            }
        }
        public static void NextLevel()
        {
            //Temporary solution
            int newLevel = _levelIndex + 1;
            switch (newLevel)
            {
                case 0: ActivateLevel(0, GameFiles.Levels.Level1); break;
                case 1: ActivateLevel(1, GameFiles.Levels.Level2); break;
                case 2: ActivateLevel(2, GameFiles.Levels.Level3); break;
                case 3: ActivateLevel(3, GameFiles.Levels.Level4); break;
                case 4: ActivateLevel(4, GameFiles.Levels.Level5); break;
                default: ActivateLevel(5, GameFiles.Levels.Level1); break;
            }
        }
        public static void Restart()
        {
            ActivateLevel(0, GameFiles.Levels.Level1);
           // SetUpLevel();
        }
    }
}
