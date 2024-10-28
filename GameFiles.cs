using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public static class GameFiles
    {
        public static class Levels
        {
            private static readonly Vector2 defaultLevelStartPosition = new Vector2(120, 120);
            private static readonly Vector2 bossLevelStartPosition = new Vector2(0, 0);

            private static readonly Vector2 playerStartPositionLevel1 = new Vector2(377, 377);
            private static readonly Vector2 playerStartPositionLevel2 = new Vector2(377, 577);
            private static readonly Vector2 playerStartPositionLevel3 = new Vector2(377, 537);
            private static readonly Vector2 playerStartPositionLevel4 = new Vector2(377, 537);
            private static readonly Vector2 playerStartPositionLevel5 = new Vector2(457, 777);


            public static readonly LevelConfig ShowOffLevel = new LevelConfig("Content/ShowOffLevel.txt", defaultLevelStartPosition, Level.ReadTileDataFromFile(LevelType.SHOWOFFLEVEL), "Content/GameObjectsShowOffLevel.txt", playerStartPositionLevel1);
            public static readonly LevelConfig Level1 = new LevelConfig("Content/Level1Map.txt", defaultLevelStartPosition, Level.ReadTileDataFromFile(LevelType.REACHTARGETLEVELS), "Content/GameObjectsLevel1.txt", playerStartPositionLevel1);
            public static readonly LevelConfig Level2 = new LevelConfig("Content/Level2Map.txt", defaultLevelStartPosition, Level.ReadTileDataFromFile(LevelType.REACHTARGETLEVELS), "Content/GameObjectsLevel2.txt", playerStartPositionLevel2);
            public static readonly LevelConfig Level3 = new LevelConfig("Content/Level3Map.txt", defaultLevelStartPosition, Level.ReadTileDataFromFile(LevelType.REMOVETARGETLEVELS), "Content/GameObjectsLevel3.txt", playerStartPositionLevel3);
            public static readonly LevelConfig Level4 = new LevelConfig("Content/Level4Map.txt", defaultLevelStartPosition, Level.ReadTileDataFromFile(LevelType.REMOVETARGETLEVELS), "Content/GameObjectsLevel4.txt", playerStartPositionLevel4);
            public static readonly LevelConfig Level5 = new LevelConfig("Content/Level5Map.txt", bossLevelStartPosition, Level.ReadTileDataFromFile(LevelType.FALLINGPLATFORMSLEVELS), "Content/GameObjectsLevel5.txt", playerStartPositionLevel5);
        }
        public static class LevelType
        {
            //Create Level tiles
            public const string SHOWOFFLEVEL = "Content/ShowOffLevelConfig.txt";
            public const string REACHTARGETLEVELS = "Content/ReachTargetLevelConfig.txt";
            public const string REMOVETARGETLEVELS = "Content/RemoveTargetLevelConfig.txt";
            public const string FALLINGPLATFORMSLEVELS = "Content/FallingPlatformsLevelConfig.txt";
        }
        public static class Character
        {
            //Create Level tiles
            public static string CHARACTER = "Content/MarioCharacter.txt";
        }
        public static class Config
        {
            public const string UICONFIG = "Content/UIConfig.txt";
            public const string GAMESETTINGS = "Content/GameSettings.txt";
        }
    }
}
