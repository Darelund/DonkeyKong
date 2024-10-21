using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DonkeyKong
{
    public class EnemyController : AnimatedGameObject
    {
        private Vector2 destination;
        private Vector2 direction;
        private float speed = 100.0f;
        private bool moving = false;

      
        public EnemyController(Texture2D texture, Vector2 position, float speed, Point currentFrame, Point frameSize, Point sheetSize, Color color, float rotation, int size, float layerDepth, Vector2 origin, int millisecondsPerFrame = 16) : base(texture, position, speed, currentFrame, frameSize, sheetSize, color, rotation, size, layerDepth, origin, millisecondsPerFrame)
        {
            Random ran = new Random();
            int randomStartDirection = ran.Next(0, 2);
            Debug.WriteLine(randomStartDirection);
            direction.X = randomStartDirection == 1 ? -1 : 1;
           // CollisionManager.AddCollisionObject(this);
        }
        public override void Update(GameTime gameTime)
        {
            if (!moving)
            {
                //Need to fix this
                ChangeDirection(direction);
            }
            else
            {
                Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                HandleAnimation(direction, gameTime);


                //Check if we are near enough to the destination
                if (Vector2.Distance(Position, destination) < 1)
                {
                    Position = destination;
                    moving = false;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, new Rectangle(_currentFrame.X * _frameSize.X, _currentFrame.Y * _frameSize.Y, _frameSize.X, _frameSize.Y), Color, 0f, Origin, Size, currentDirection, LayerDepth);

        }
        private SpriteEffects currentDirection = SpriteEffects.None;
        private void HandleAnimation(Vector2 dir, GameTime gameTime)
        {
            if (dir.Length() <= 0) return;
            else
            {
                AnimationFlip(dir);
            }
            base.Update(gameTime);
        }
        private void AnimationFlip(Vector2 dir)
        {
            currentDirection = dir.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        }
        public void ChangeDirection(Vector2 dir)
        {
            direction = dir;
            //I want to get tilesize somehow, what if they are a dífferent size?
            float tileSize = 40.0f;
            Vector2 newDestination = Position + direction * tileSize;

            //Check if we can move in the desired direction, if not, do nothing
            if (LevelManager.GetCurrentLevel.GetTileAtPosition(newDestination))
            {
                destination = newDestination;
                moving = true;
            }
            else
            {
                direction.X *= -1;
            }
        }
        //private int GetMovement()
        //{

        //}
        public bool HasCollided(Rectangle rec)
        {
            return Collision.Intersects(rec);
        }
    }
}
