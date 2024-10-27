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
        public Vector2 Position { get; set; }
        protected float normalSpeed;
        protected virtual float Speed { get => normalSpeed; set => normalSpeed = value; }
        protected Color Color;
        public float Rotation { get; protected set; }
        public float Size { get; protected set; }
        protected float LayerDepth;
        public Vector2 Origin { get; protected set; }
        protected bool _isActive = true;

        public abstract Rectangle Collision { get; }
       
        public GameObject(Texture2D texture, Vector2 position, float speed, Color color, float rotation, float size, float layerDepth, Vector2 origin) 
        {
            Texture = texture;
            Position = position;
            Speed = speed;
            Color = color;
            Rotation = rotation;
            Size = size;
            LayerDepth = layerDepth;
            Origin = origin;
        }
        public abstract void Update(GameTime gameTime);
       
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color);
        }
        public abstract void OnCollision(GameObject gameObject);
        public abstract bool IsActive();
    }
}
