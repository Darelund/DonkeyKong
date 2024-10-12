using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public abstract class GameObject
    {
        protected Texture2D Texture;
        protected Vector2 Position;
        protected float Speed;
        protected Color Color;
        //public float Rotation
        //public Point Size
        public GameObject(Texture2D texture, Vector2 position, float speed, Color color) 
        {
            Texture = texture;
            Position = position;
            Speed = speed;
            Color = color;
        }
        public abstract void Update(GameTime gameTime);
       
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color);
        }
    }
}
