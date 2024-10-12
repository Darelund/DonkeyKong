using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class Tile
    {
        public Texture2D Texture;
        public Vector2 Pos;
        public bool NotWalkable;

        public Tile(Vector2 pos, Texture2D texture, bool notWalkable)
        {
            Pos = pos;
            Texture = texture;
            NotWalkable = notWalkable;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Pos, Color.White);
        }
    }
}
