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
        protected Point _currentFrame;
        protected Point _frameSize;
        protected Point _sheetSize;

        protected int _millisecondsPerFrame;
        protected float _timeSinceLastFrame = 0;
        protected bool UseColumns = true;
        public override Rectangle Collision
        {
            get
            {
                return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, _frameSize.X * Size, _frameSize.Y * Size);
            }
        }
        public AnimatedGameObject(Texture2D texture, Vector2 position, float speed, Point currentFrame, Point frameSize, Point sheetSize, Color color, float rotation, int size, float layerDepth, Vector2 origin, int millisecondsPerFrame = 16) : base(texture, position, speed, color, rotation, size, layerDepth, origin)
        {
            _currentFrame = currentFrame;
            _frameSize = frameSize;
            _sheetSize = sheetSize;
            _millisecondsPerFrame = millisecondsPerFrame;
        }

        public override void Update(GameTime gameTime)
        {
            _timeSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
           if(_timeSinceLastFrame >= _millisecondsPerFrame)
            {
                _timeSinceLastFrame -= _millisecondsPerFrame;

                _currentFrame.X++;
                if(_currentFrame.X >= _sheetSize.X)
                {
                    _currentFrame.X = 0;
                    _currentFrame.Y++;
                    if(_currentFrame.Y >= _sheetSize.Y)
                    {
                        _currentFrame.Y = 0;
                    }
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position + Origin * 2, new Rectangle(_currentFrame.X * _frameSize.X, _currentFrame.Y * _frameSize.Y, _frameSize.X, _frameSize.Y), Color, 0f, Origin, Size, SpriteEffects.None, LayerDepth);
        }
        public override bool CheckCollision(GameObject gameObject)
        {
            return Collision.Intersects(gameObject.Collision);
        }
    }
}
