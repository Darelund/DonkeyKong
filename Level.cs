﻿using Microsoft.Xna.Framework;
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

namespace DonkeyKong
{
    public abstract class Level
    {
        protected Vector2 _startPosition;
        protected Tile[,] _tiles;
        public bool LevelCompleted { get; set; } = false;
        public event Action<Tile> TileSteppedOnHandler;


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
        public abstract bool CheckLevelCompletion();
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
                GameObject newObject = CreateGameObjectFromType(objectType, objectData);

                if (newObject != null)
                {
                    GameManager.AddGameObject(newObject);
                }

                i++;  // Skip the blank line between object definitions
            }
        }

        private GameObject CreateGameObjectFromType(string objectType, List<string> objectData)
        {
            // Create objects based on their type
            switch (objectType)
            {
                case "EnemyController":
                    Debug.WriteLine("Returning?");
                    return CreateEnemyController(objectData);
                case "PlayerController":
                  //  return CreatePlayerController(objectData);
                //case "PickUp":
                //    return CreatePickUp(objectData);
                default:
                    Debug.WriteLine("Unknown object type: " + objectType);
                    return null;
            }
        }
        private EnemyController CreateEnemyController(List<string> data)
        {
            // Parse the data and initialize the object
            // Assume data contains all the needed parameters in the expected order

            // 1. Parse general properties (like sprite, position, color, etc.)
            string sprite = data[0];  // Texture name or path
            string[] positionParts = data[1].Split(',');
            int xPos = int.Parse(positionParts[0].Trim());
            int yPos = int.Parse(positionParts[1].Trim());
            Vector2 position = new Vector2(xPos, yPos);

            string colorName = data[2].Trim();
            Color color = colorName switch
            {
                "white" => Color.White,
                "red" => Color.Red,
                "blue" => Color.Blue,
                "green" => Color.Green,
                _ => Color.White // Default color if not found
            };

            float rotation = float.Parse(data[3].Trim());  // Rotation
            float size = float.Parse(data[4].Trim());      // Size/scale
            float layerDepth = float.Parse(data[5].Trim());  // Layer depth

            string[] originParts = data[6].Split(',');
            xPos = int.Parse(originParts[0].Trim());
            yPos = int.Parse(originParts[1].Trim());
            Vector2 origin = new Vector2(xPos, yPos);
            // 2. Parse animation clips (loop through the animation data in the input list)
            var animationClips = new Dictionary<string, AnimationClip>();

            // Example: WalkAnimation:5,86,18,18|23,86,18,18|41,86,18,18;4f
            for (int i = 7; i < data.Count; i++)
            {
                string animationData = data[i];
                if (!string.IsNullOrWhiteSpace(animationData))
                {
                    // Split each animation data (format: "Name:rect1|rect2|...;speed")
                    string[] animationParts = animationData.Split(':');
                    string animationName = animationParts[0].Trim();

                    string[] rectsAndSpeed = animationParts[1].Split(';');
                    string[] rectStrings = rectsAndSpeed[0].Split('|'); // Each rectangle info

                    // Create an array of rectangles for the animation
                    Rectangle[] frames = rectStrings.Select(rectStr =>
                    {
                        string[] rectComponents = rectStr.Split(',');
                        int rectX = int.Parse(rectComponents[0].Trim());
                        int rectY = int.Parse(rectComponents[1].Trim());
                        int rectWidth = int.Parse(rectComponents[2].Trim());
                        int rectHeight = int.Parse(rectComponents[3].Trim());

                        return new Rectangle(rectX, rectY, rectWidth, rectHeight);
                    }).ToArray();

                    // Parse the speed of the animation
                    float animationSpeed = float.Parse(rectsAndSpeed[1].Trim());

                    // Add the parsed animation to the dictionary
                    animationClips[animationName] = new AnimationClip(frames, animationSpeed);
                }
            }
            // 3. Create and return the EnemyController object with the parsed data
            return new EnemyController(
                ResourceManager.GetTexture(sprite),  // Texture loading handled elsewhere
                position,
                color,
                rotation,
                size,
                layerDepth,
                origin,
                animationClips  // Add the parsed animation clips
            );
        }

        //private PlayerController CreatePlayerController(List<string> data)
        //{
        //    // Parse player-specific data
        //    string sprite = data[0];
        //    string[] positionParts = data[1].Split(',');
        //    int xPos = int.Parse(positionParts[0].Trim());
        //    int yPos = int.Parse(positionParts[1].Trim());

        //    // Parse other properties from data...

        //   // return new PlayerController(sprite, new Vector2(xPos, yPos), /* other params */);
        //}

        //private PickUp CreatePickUp(List<string> data)
        //{
        //    // Parse pick-up-specific data
        //    string sprite = data[0];
        //    string[] positionParts = data[1].Split(',');
        //    int xPos = int.Parse(positionParts[0].Trim());
        //    int yPos = int.Parse(positionParts[1].Trim());

        //    // Parse other properties from data...

        //    return new PickUp(sprite, new Vector2(xPos, yPos), /* other params */);
        //}


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in _tiles)
            {
                tile.Draw(spriteBatch);
            }
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
