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
        private static Tile[,] Tiles;

        /// <summary>
        /// Reads the contents of a specified text file line by line and populates a list of strings,
        /// where each string represents a line from the file. This is useful for processing 
        /// paragraphs or multiline text, as each line will be stored as a separate element in the list.
        /// </summary>
        /// <param name="fileName">The path to the text file to be read.</param>
        /// <returns>A list of strings, where each string is a line from the file.</returns>
        private static List<string> ReadFromFile(string fileName)
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
        /// <param name="textureTuples"></param>
        public static void CreateLevel(string file, List<(char TileName, Texture2D tileTexture, bool notWalkable)> textureTuples)
        {
            List<string> result = ReadFromFile(file);

            Tiles = new Tile[result.Count, result[0].Length];

            for (int i = 0; i < result[0].Length; i++)
            {
                for (int j = 0; j < result.Count; j++)
                {
                    foreach (var textureTuple in textureTuples)
                    {
                        if (result[j][i] == textureTuple.TileName)
                        {
                            Tiles[j, i] = new Tile(new Vector2(textureTuple.tileTexture.Width * i, textureTuple.tileTexture.Height * j), textureTuple.tileTexture, true);
                            break;
                        }
                    }
                }
            }
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in Tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}
