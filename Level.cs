using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class Level
    {
        private Tile[,] _tiles;

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
        public void CreateLevel(string file, Vector2 startPosition, List<(char TileName, Texture2D tileTexture, bool notWalkable)> tileTexture)
        {
            List<string> result = ReadFromFile(file);

            _tiles = new Tile[result.Count, result[0].Length];

            for (int i = 0; i < result[0].Length; i++)
            {
                for (int j = 0; j < result.Count; j++)
                {
                    foreach (var textureTuple in tileTexture)
                    {
                        if (result[j][i] == textureTuple.TileName)
                        {
                            _tiles[j, i] = new Tile(new Vector2(textureTuple.tileTexture.Width * i + startPosition.X, textureTuple.tileTexture.Height * j + startPosition.Y), textureTuple.tileTexture, true);
                            break;
                        }
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in _tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
        public bool GetTileAtPosition(Vector2 vec)
        {
            return _tiles[(int)vec.X / 40, (int)vec.Y / 40].NotWalkable;
        }
    }
}
