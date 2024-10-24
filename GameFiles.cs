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
            public static readonly LevelConfig Level1 = new LevelConfig("Content/Level1.txt", new Vector2(120, 120), Level.ReadTileDataFromFile(LevelType.REACHTARGETLEVELS), "Content/GameObjectsLevel1.txt");
            public static readonly LevelConfig Level2 = new LevelConfig("Content/Level2.txt", new Vector2(120, 120), Level.ReadTileDataFromFile(LevelType.REACHTARGETLEVELS), "Content/GameObjectsLevel2.txt");
            public static readonly LevelConfig Level3 = new LevelConfig("Content/Level3.txt", new Vector2(120, 120), Level.ReadTileDataFromFile(LevelType.REMOVETARGETLEVELS), "Content/GameObjectsLevel3.txt");
            public static readonly LevelConfig Level4 = new LevelConfig("Content/Level4.txt", new Vector2(120, 120), Level.ReadTileDataFromFile(LevelType.REMOVETARGETLEVELS), "Content/GameObjectsLevel4.txt");
            public static readonly LevelConfig Level5 = new LevelConfig("Content/Level5.txt", new Vector2(120, 120), Level.ReadTileDataFromFile(LevelType.REMOVETARGETLEVELS), "Content/GameObjectsLevel5.txt");
        }
        public static class LevelType
        {
            //Create Level tiles
            public const string REACHTARGETLEVELS = "Content/ReachTargetLevel.txt";
            public const string REMOVETARGETLEVELS = "Content/RemoveTargetLevel.txt";
        }
        public static class Config
        {
            public const string UICONFIG = "Content/UIConfig.txt";
            public const string GAMESETTINGS = "Content/GameSettings.txt";
        }
    }
}
