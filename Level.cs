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

namespace DonkeyKong
{
    public class Level
    {
        private Vector2 _startPosition;
        private Tile[,] _tiles;



        //public List<GameObject> GameObjects = new List<GameObject>();

        /// <summary>
        /// Reads the contents of a specified text file line by line and populates a list of strings,
        /// where each string represents a line from the file. This is useful for processing 
        /// paragraphs or multiline text, as each line will be stored as a separate element in the list.
        /// </summary>
        /// <param name="fileName">The path to the text file to be read.</param>
        /// <returns>A list of strings, where each string is a line from the file.</returns>
        private List<string> ReadFromFile(string fileName)
        {
            List<string> result = new List<string>();

            using (StreamReader sR = new StreamReader(fileName))
            {
                while (!sR.EndOfStream)
                {
                    string line = sR.ReadLine();
                    result.Add(line);
                    Debug.WriteLine(line);
                }
            }
            return result;
        }
        /// <summary>
        /// Creates a 2D grid level based on the file you give it. In the file each character represents one tile. 
        /// So you give it a list of tuples where each tuple is its character, texture and if you can walk on it.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="tileTexture"></param>
        public void CreateLevel(string file, Vector2 startPosition, List<(char TileName, Texture2D tileTexture, TileType type, Color tileColor)> tileTexture)
        {
            List<string> result = ReadFromFile(file);
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
        public void CreateLevelGameObjects(string objectsFilePath)
        {
            List<string> fileLines = ReadFromFile(objectsFilePath);

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
            string sprite = data[0];
            string[] positionParts = data[1].Split(',');
            int xPos = int.Parse(positionParts[0].Trim());
            int yPos = int.Parse(positionParts[1].Trim());
            Vector2 Position = new Vector2(xPos, yPos);
            int speed = int.Parse(data[2]);

            string[] currentFrameParts = data[3].Split(',');
            xPos = int.Parse(currentFrameParts[0].Trim());
            yPos = int.Parse(currentFrameParts[1].Trim());
            Point currentFrame = new Point(xPos, yPos);

            string[] FrameSizeParts = data[4].Split(',');
            xPos = int.Parse(FrameSizeParts[0].Trim());
            yPos = int.Parse(FrameSizeParts[1].Trim());
            Point frameSize = new Point(xPos, yPos);

            string[] sheetSizeParts = data[5].Split(',');
            xPos = int.Parse(sheetSizeParts[0].Trim());
            yPos = int.Parse(sheetSizeParts[1].Trim());
            Point sheetSize = new Point(xPos, yPos);

            string colorName = data[6].Trim();
            Color color = colorName switch
            {
                "white" => Color.White,
                "red" => Color.Red,
                "blue" => Color.Blue,
                "green" => Color.Green,
                // Add more colors as needed
                _ => Color.White // Default color if not found
            };

            float rotation = float.Parse(data[7].Trim());
            int size = int.Parse(data[8].Trim());
            float layerDepth = float.Parse(data[9].Trim());


            string[] originParts = data[10].Split(',');
            xPos = int.Parse(originParts[0].Trim());
            yPos = int.Parse(originParts[1].Trim());
            Vector2 origin = new Vector2(xPos, yPos);



            // Parse other properties from data...

            return null;
            //return new EnemyController(ResourceManager.GetTexture(sprite), Position, speed, currentFrame, frameSize, sheetSize, color, rotation, size, layerDepth, origin, 100 /* other params */);
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

        //private void CreateLevelGameObjects(string objects, string types)
        //{
        //    //Hur läser man av många saker från samma fil?
        //    List<string> result = ReadFromFile(objects);
        //    List<string> GameObjectTypes = ReadFromFile(types);

        //    for (int i = 0; i < result.Count; i++)
        //    {
        //        foreach (var type in GameObjectTypes)
        //        {
        //            if (result[i][0] == types[0])
        //            {
        //                GameObjects.Add()
        //            }
        //        }
        //    }
        //}
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in _tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
        public bool IsTileWalkable(Vector2 vec)
        {
            int tileSize = 40;
            vec -= _startPosition;
            Point tilePos = new Point((int)vec.X / tileSize, (int)vec.Y / tileSize);

            if (!TileExistsAtPosition(tilePos)) return false;

            return !(_tiles[tilePos.X, tilePos.Y].Type == TileType.NonWalkable);
        }
        public bool IsTileLadder(Vector2 vec, int dir)
        {
            int tileSize = 40;
            vec -= _startPosition;
            Point tilePos = new Point((int)vec.X / tileSize, (int)vec.Y / tileSize);

            if (!TileExistsAtPosition(tilePos)) return false;

            return /*_tiles[tilePos.X, tilePos.Y].Type == TileType.Ladder)*/  _tiles[tilePos.X, tilePos.Y + 1].Type == TileType.Ladder || _tiles[tilePos.X, tilePos.Y + 1].Type == TileType.NonWalkable || _tiles[tilePos.X, tilePos.Y + (dir * -1)].Type == TileType.Ladder && _tiles[tilePos.X, tilePos.Y].Type == TileType.Walkable;
        }

       
        //Maybe switch to this

        //public bool DoesTileExistOfType(Vector2 vec, TileType type)
        //{
        //    int tileSize = 40;
        //    vec -= _startPosition;
        //    Point tilePos = new Point((int)vec.X / tileSize, (int)vec.Y / tileSize);

        //    if (!TileExistsAtPosition(tilePos)) return false;

        //    return (_tiles[tilePos.X, tilePos.Y].Type == type);
        //}
        public bool IsGrounded(Vector2 vec)
        {
            int tileSize = 40;
            vec -= _startPosition;

            // Calculate the current tile position of the player
            Point tilePos = new Point((int)vec.X / tileSize, (int)vec.Y / tileSize);

            // Move the tilePos down by 1 tile to check the block directly below the player
            tilePos.Y += 1;

            // Check if the new tile position is within bounds
            TileExistsAtPosition(tilePos);

            // Return true if the tile exists and is not empty (you can add custom logic for specific blocks here)
            return (_tiles[tilePos.X, tilePos.Y].Type == TileType.NonWalkable); // Assuming Empty is a walkable/ground type
        }
        private bool TileExistsAtPosition(Point tilePos)
        {
            if (tilePos.X < 0 || tilePos.X >= _tiles.GetLength(0)) return false;
            if (tilePos.Y < 0 || tilePos.Y >= _tiles.GetLength(1)) return false;

            return true;
        }
    }
}
