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
            public static readonly LevelConfig ShowOffLevel = new LevelConfig("Content/ShowOffLevel.txt", new Vector2(120, 120), Level.ReadTileDataFromFile(LevelType.SHOWOFFLEVEL), "Content/GameObjectsShowOffLevel.txt");
            public static readonly LevelConfig Level1 = new LevelConfig("Content/Level1Map.txt", new Vector2(120, 120), Level.ReadTileDataFromFile(LevelType.REACHTARGETLEVELS), "Content/GameObjectsLevel1.txt");
            public static readonly LevelConfig Level2 = new LevelConfig("Content/Level2Map.txt", new Vector2(120, 120), Level.ReadTileDataFromFile(LevelType.REACHTARGETLEVELS), "Content/GameObjectsLevel2.txt");
            public static readonly LevelConfig Level3 = new LevelConfig("Content/Level3Map.txt", new Vector2(120, 120), Level.ReadTileDataFromFile(LevelType.REMOVETARGETLEVELS), "Content/GameObjectsLevel3.txt");
            public static readonly LevelConfig Level4 = new LevelConfig("Content/Level4Map.txt", new Vector2(120, 120), Level.ReadTileDataFromFile(LevelType.REMOVETARGETLEVELS), "Content/GameObjectsLevel4.txt");
            public static readonly LevelConfig Level5 = new LevelConfig("Content/Level5Map.txt", new Vector2(0, 0), Level.ReadTileDataFromFile(LevelType.FALLINGPLATFORMSLEVELS), "Content/GameObjectsLevel5.txt");
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
