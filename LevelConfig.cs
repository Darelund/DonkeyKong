﻿using Microsoft.Xna.Framework;
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
        public string LevelFile { get; }
        public Vector2 LevelStartPosition { get; }
        public Vector2 PlayerStartPosition { get; }
        public List<(char TileName, Texture2D tileTexture, TileType type, Color tileColor)> TileData { get; }
        public string GameObjectsFile { get; }

        public LevelConfig(string levelFile, Vector2 levelStartPosition,
            List<(char TileName, Texture2D tileTexture, TileType type, Color tileColor)> tileData,
            string gameObjectsFile, Vector2 playerStartPosition)
        {
            LevelFile = levelFile;
            LevelStartPosition = levelStartPosition;
            TileData = tileData;
            GameObjectsFile = gameObjectsFile;
            PlayerStartPosition = playerStartPosition;
        }
    }
}
