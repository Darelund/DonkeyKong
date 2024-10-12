﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public abstract class StaticUIElement : UIElement
    {
        public StaticUIElement(Vector2 position, Color color, float size, float layerDepth = 0, float rotation = 0) : base(position, color, size, layerDepth, rotation)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void Update(GameTime gameTime)
        {
           
        }
    }
}
