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
        public static void CreateLevel(string file)
        {
            List<string> result = ReadFromFile(file);

            Tiles = new Tile[result.Count, result[0].Length];

            for (int i = 0; i < result[0].Length; i++)
            {
                for (int j = 0; j < result.Count; j++)
                {
                    //B stands for bridge
                    //- stands for empty
                    //L stands for ladder
                    if (result[j][i] == 'B')
                    {
                        Tiles[j, i] = new Tile(new Vector2(ResourceManager.GetTexture("bridge").Width * i, ResourceManager.GetTexture("bridge").Height * j), ResourceManager.GetTexture("bridge"), true);
                    }
                    if (result[j][i] == 'L')
                    {
                        Tiles[j, i] = new Tile(new Vector2(ResourceManager.GetTexture("ladder").Width * i, ResourceManager.GetTexture("ladder").Height * j), ResourceManager.GetTexture("ladder"), false);
                    }
                    if (result[j][i] == 'b')
                    {
                        Tiles[j, i] = new Tile(new Vector2(ResourceManager.GetTexture("bridgeLadder").Width * i, ResourceManager.GetTexture("bridgeLadder").Height * j), ResourceManager.GetTexture("bridgeLadder"), false);
                    }
                    if (result[j][i] == '-')
                    {
                        Tiles[j, i] = new Tile(new Vector2(ResourceManager.GetTexture("empty").Width * i, ResourceManager.GetTexture("empty").Height * j), ResourceManager.GetTexture("empty"), false);
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
