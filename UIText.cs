using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class UIText : StaticUIElement
    {
        private SpriteFont _font;
        public string _text;

        public UIText(SpriteFont font, string text, Vector2 position, Color color, float size, Vector2 origin, float layerDepth = 0, float rotation = 0) : base(position, color, size, origin, layerDepth, rotation)
        {
            _font = font;
            _text = text;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _text, Position, CurrentColor, Rotation, Origin, Size, SpriteEffects.None, LayerDepth);
        }
        public override void Update(GameTime gameTime)
        {
        }
        public void UpdateText(string newText)
        {
            _text = newText;
        }
    }
}
