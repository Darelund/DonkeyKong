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
        public override Rectangle Collision
        {
            //Out of work
            //get
            //{
            //    return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, _frameSize.X * Size, _frameSize.Y * Size);
            //}
            get => new Rectangle(0, 0, 0, 0);
        }
        public AnimatedGameObject(Texture2D texture, Vector2 position, float speed, Color color, float rotation, int size, float layerDepth, Vector2 origin, Dictionary<string, AnimationClip> animationClips) : base(texture, position, speed, color, rotation, size, layerDepth, origin)
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
            spriteBatch.Draw(Texture, Position + Origin * 2, _currentClip.GetCurrentSourceRectangle(), Color, 0f, Origin, Size, SpriteEffects.None, LayerDepth);
        }
        public override bool CheckCollision(GameObject gameObject)
        {
            return Collision.Intersects(gameObject.Collision);
        }
    }
}
