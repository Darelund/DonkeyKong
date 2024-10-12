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
        private Point _currentFrame;
        private Point _frameSize;
        private Point _sheetSize;

        private int _millisecondsPerFrame;
        private float _timeSinceLastFrame = 0;
        public AnimatedGameObject(Texture2D texture, Vector2 position, float speed, Point currentFrame, Point frameSize, Point sheetSize, Color color, int millisecondsPerFrame = 16) : base(texture, position, speed, color)
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
            base.Draw(spriteBatch);
        }
    }
}
