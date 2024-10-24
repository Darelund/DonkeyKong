using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public enum TileType
    {
        Walkable,
        NonWalkable,
        Ladder,
        Hazard, // for future extensibility like spikes, fire, etc.
        Decoration  // For tiles that are purely cosmetic
    }
    public class Tile
    {
        
        public TileType Type { get; }
        private Texture2D _texture;
        public Vector2 Pos { get; private set; }
        public char Name { get; private set; }
        private Color _color;

        public Tile(Vector2 pos, Texture2D texture, TileType type, Color color, char name)
        {
            Pos = pos;
            _texture = texture;
            Type = type;
            _color = color;
            Name = name;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //To add more functionality
            spriteBatch.Draw(_texture, Pos, null, _color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
           // spriteBatch.Draw(Texture, Pos, Color.White);
        }
        public void SwitchTile(Texture2D newTexture)
        {
            _texture = newTexture;
        }
    }
}
