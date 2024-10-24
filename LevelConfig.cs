using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class LevelConfig
    {
        public string LevelFile { get; set; }
        public Vector2 StartPosition { get; set; }
        public List<(char TileName, Texture2D tileTexture, TileType type, Color tileColor)> TileData { get; set; }
        public string GameObjectsFile { get; set; }

        public LevelConfig(string levelFile, Vector2 startPosition,
            List<(char TileName, Texture2D tileTexture, TileType type, Color tileColor)> tileData,
            string gameObjectsFile)
        {
            LevelFile = levelFile;
            StartPosition = startPosition;
            TileData = tileData;
            GameObjectsFile = gameObjectsFile;
        }
    }
}
