using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class UIImage : StaticUIElement
    {
        private Texture2D _texture;
        private Rectangle ImageSourceRect
        {
            get => new Rectangle(0, 200, 30, 30);
    }

        public UIImage(Texture2D texture, Vector2 position, Color color, float size, Vector2 origin, float layerDepth = 0, float rotation = 0) : base(position, color, size, origin, layerDepth, rotation)
        {
            _texture = texture;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < PlayerController.Instance.Health; i++)
            {
                spriteBatch.Draw(_texture, Position + new Vector2((i * 50), 0), ImageSourceRect, CurrentColor, Rotation, Origin, Size, SpriteEffects.None, LayerDepth);
            }
        }
        public override void Update(GameTime gameTime)
        {
        }
    }
}
