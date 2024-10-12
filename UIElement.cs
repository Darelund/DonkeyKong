using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class UIElement
    {
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public float Size { get; set; }
        public float LayerDepth { get; set; }

        public UIElement(Vector2 position, Color color, float size, float layerDepth = 0f)
        {
            Position = position;
            Color = color;
            Size = size;
            LayerDepth = layerDepth;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        // Basic Draw method
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
