using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class AnimatedUIElement : UIElement
    {
        private Texture2D _texture;
        private float _animationTimer;
        private float _animationSpeed;
        private Rectangle _sourceRectangle;

        public AnimatedUIElement(Texture2D texture, Vector2 position, Color color, float size, float animationSpeed, float layerDepth = 0, float rotation = 0)
            : base(position, color, size, layerDepth, rotation)
        {
            _texture = texture;
            _animationSpeed = animationSpeed;
            _sourceRectangle = new Rectangle(0, 0, _texture.Width, _texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            _animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Update animation logic (e.g., change source rectangle for sprite sheet animations)
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, _sourceRectangle, CurrentColor, 0f, Vector2.Zero, Size, SpriteEffects.None, LayerDepth);
        }
    }
}
