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
        private Texture2D _texture;
        private Vector2 _pos;
        public bool NotWalkable { get; }
        private int _name;
        private Color _color;

        public Tile(Vector2 pos, Texture2D texture, bool notWalkable, Color color)
        {
            _pos = pos;
            _texture = texture;
            NotWalkable = notWalkable;
            _name = LevelManager.NameIndex++;
            _color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //To add more functionality
            spriteBatch.Draw(_texture, _pos, null, _color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
           // spriteBatch.Draw(Texture, Pos, Color.White);
        }
    }
}
