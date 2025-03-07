﻿using Microsoft.Xna.Framework;
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
    public enum TileState
    { 
        Static,
        Dynamic
    }
    public class Tile : ICloneable
    {
        
        public TileType Type { get; set; }
        private Texture2D _texture;
        public Vector2 Pos { get; private set; }
        public char Name { get; set; }
        private Color _color;
        public TileState _state = TileState.Static;
        private const float _FallSpeed = 90f; 
        public Tile(Vector2 pos, Texture2D texture, TileType type, Color color, char name)
        {
            Pos = pos;
            _texture = texture;
            Type = type;
            _color = color;
            Name = name;
        }
        public void Update(GameTime gameTime)
        {
            if(_state == TileState.Dynamic)
            {
                float fallDistance = _FallSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Pos = new Vector2(Pos.X, Pos.Y + fallDistance);

                if (Pos.Y > GameManager.Window.ClientBounds.Height) // Bottom boundary
                {
                    Pos = new Vector2(Pos.X, GameManager.Window.ClientBounds.Height);
                }
            }
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

        public object Clone()
        {
           return MemberwiseClone();
        }
    }
}
