using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class AnimatedGameObject : GameObject
    {
        protected Dictionary<string, AnimationClip> _animationClips;
        protected AnimationClip _currentClip;
        private float _deltaTime;
        protected SpriteEffects currentDirection;
        public override Rectangle Collision
        {
            //I might need to adjust the box to make the collision more fun for the player
            get
            {
                Rectangle rec = _currentClip.GetCurrentSourceRectangle();
                return new Rectangle((int)Position.X - (int)Origin.X * (int)Size, (int)Position.Y - (int)Origin.Y * (int)Size, rec.Width * (int)Size, rec.Height * (int)Size);
            }
        }
        public AnimatedGameObject(Texture2D texture, Vector2 position, float speed, Color color, float rotation, float size, float layerDepth, Vector2 origin, Dictionary<string, AnimationClip> animationClips) : base(texture, position, speed, color, rotation, size, layerDepth, origin)
        {
            _animationClips = animationClips;
            //Todo maybe make it possible to choose what animation to start at?
            _currentClip = _animationClips[_animationClips.Keys.First()];
        }

        public override void Update(GameTime gameTime)
        {
            _deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _currentClip.Update(_deltaTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Why did I put a 2 here?
            //I think it was to position the sprite right if it had a new origin
            //Seems to be outdated, think I added the offset when creating the character
            //Vector2 positionOffset = Origin * 2;
            spriteBatch.Draw(Texture, Position, _currentClip.GetCurrentSourceRectangle(), Color, Rotation, Origin, Size, currentDirection, LayerDepth);
        }
        public override void OnCollision(GameObject gameObject)
        {
           // return Collision.Intersects(gameObject.Collision);
        }
        protected void SwitchAnimation(string name)
        {
            if(_currentClip != _animationClips[name])
            _currentClip = _animationClips[name];
        }
        public override bool IsActive()
        {
            return _isActive;
        }
    }
}
