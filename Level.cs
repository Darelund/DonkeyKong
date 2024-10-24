using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DonkeyKong.GameFiles;

namespace DonkeyKong
{
    public abstract class Level
    {
        protected Vector2 _startPosition;
        protected Tile[,] _tiles;
        public bool LevelCompleted { get; set; } = false;
        public event Action<Tile> TileSteppedOnHandler;
        private GameObjectFactory _factory;
        public List<GameObject> GameObjectsInLevel = new();
        public Level()
        {
            _factory = new GameObjectFactory();
        }


        //public List<GameObject> GameObjects = new List<GameObject>();

        
        /// <summary>
        /// Creates a 2D grid level based on the file you give it. In the file each character represents one tile. 
        /// So you give it a list of tuples where each tuple is its character, texture and if you can walk on it.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="tileTexture"></param>
        public void CreateLevel(string file, Vector2 startPosition, List<(char TileName, Texture2D tileTexture, TileType type, Color tileColor)> tileTexture)
        {
            List<string> result = FileManager.ReadFromFile(file);
            _startPosition = startPosition;
            _tiles = new Tile[result[0].Length, result.Count];
            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < result[0].Length; j++)
                {
                    foreach (var textureTuple in tileTexture)
                    {
                        if (result[i][j] == textureTuple.TileName)
                        {
                            _tiles[j, i] = new Tile(new Vector2(textureTuple.tileTexture.Width * j + startPosition.X, textureTuple.tileTexture.Height * i + startPosition.Y), textureTuple.tileTexture, textureTuple.type, textureTuple.tileColor, textureTuple.TileName);
                            break;
                        }
                    }
                }
            }
        }
        public static List<(char TileName, Texture2D tileTexture, TileType Type, Color tileColor)> ReadTileDataFromFile(string fileName)
        {
            List<(char, Texture2D, TileType, Color)> tileData = new List<(char, Texture2D, TileType, Color)>();

            using (StreamReader reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine().Split(' ');

                    if (line.Length == 4)
                    {
                        char tileName = line[0][0];
                        string textureName = line[1];
                        TileType type = (TileType)Enum.Parse(typeof(TileType), line[2]);
                        string colorName = line[3].Trim();
                        Color color = colorName switch
                        {
                            "White" => Color.White,
                            "Red" => Color.Red,
                            "Brown" => Color.Brown,
                            "Blue" => Color.Blue,
                            "Green" => Color.Green,
                            "DarkGreen" => Color.DarkGreen,
                            _ => Color.White // Default color if not found
                        };

                        // Add the tile to the list, converting the texture name to a Texture2D object
                        tileData.Add((tileName, ResourceManager.GetTexture(textureName), type, color));
                    }
                }
            }

            return tileData;
        }
        public abstract bool CheckLevelCompletion();
        public abstract void SetTarget();
        public void CreateGameObjects(string objectsFilePath)
        {
            List<string> fileLines = FileManager.ReadFromFile(objectsFilePath);

            int i = 0;
            while (i < fileLines.Count)
            {
                // Get the object type from the first line
                string objectType = fileLines[i].Trim();
                i++;  // Move to the next line for object data

                // Read the relevant properties for each object
                List<string> objectData = new List<string>();

                while (i < fileLines.Count && !string.IsNullOrWhiteSpace(fileLines[i]))
                {
                    objectData.Add(fileLines[i].Trim());
                    i++;
                }

                // Create the appropriate game object based on the object type
                GameObject newObject = _factory.CreateGameObjectFromType(objectType, objectData);

                if (newObject != null)
                {
                    GameObjectsInLevel.Add(newObject);
                }

                i++;  // Skip the blank line between object definitions
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in _tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
        public abstract void Update();

        public void UnloadLevel()
        {
            _tiles = null;
            GameObjectsInLevel.Clear();
        }
        //Used to check if tiles exists and are of x type
        public bool IsTileWalkable(Vector2 vec)
        {
            Point tilePos = GetTileAtPosition(vec);

            if (!TileExistsAtPosition(tilePos)) return false;

            return !(_tiles[tilePos.X, tilePos.Y].Type == TileType.NonWalkable);
        }
        public bool IsTileLadder(Vector2 vec, int dir)
        {
            Point tilePos = GetTileAtPosition(vec);

            if (!TileExistsAtPosition(tilePos)) return false;

            return _tiles[tilePos.X, tilePos.Y].Type == TileType.Ladder ||  _tiles[tilePos.X, tilePos.Y + 1].Type == TileType.Ladder || _tiles[tilePos.X, tilePos.Y + 1].Type == TileType.NonWalkable || _tiles[tilePos.X, tilePos.Y + (dir * -1)].Type == TileType.Ladder && _tiles[tilePos.X, tilePos.Y].Type == TileType.Walkable;
        }
        public bool IsGrounded(Vector2 vec)
        {
            Point tilePos = GetTileAtPosition(vec);

            tilePos.Y += 1;
            TileExistsAtPosition(tilePos);

            return (_tiles[tilePos.X, tilePos.Y].Type == TileType.NonWalkable); // Assuming Empty is a walkable/ground type
        }
        private Point GetTileAtPosition(Vector2 vec)
        {
            int tileSize = 40;
            vec -= _startPosition;
            return new Point((int)vec.X / tileSize, (int)vec.Y / tileSize);
        }
        private bool TileExistsAtPosition(Point tilePos)
        {
            if (tilePos.X < 0 || tilePos.X >= _tiles.GetLength(0)) return false;
            if (tilePos.Y < 0 || tilePos.Y >= _tiles.GetLength(1)) return false;
            TileSteppedOnHandler?.Invoke(_tiles[tilePos.X, tilePos.Y]);
            return true;
        }
    }
}
