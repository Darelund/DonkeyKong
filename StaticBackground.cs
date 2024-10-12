using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class StaticBackground : StaticUIElement
    {
        private Texture2D _texture;
        public StaticBackground(Texture2D texture, Vector2 position, Color color, float size, float layerDepth = 0, float rotation = 0) : base(position, color, size, layerDepth, rotation)
        {
            _texture = texture;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, CurrentColor, Rotation, Vector2.Zero, Size, SpriteEffects.None, LayerDepth);
        }
    }
}
