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
        private bool moving = false;

      
        public EnemyController(Texture2D texture, Vector2 position, Color color, float rotation, float size, float layerDepth, Vector2 origin, Dictionary<string, AnimationClip> animationClips) : base(texture, position, 0f, color, rotation, size, layerDepth, origin, animationClips)
        {
            Random ran = new Random();
            int left = 0;
            int right = 2;
            int randomStartDirection = ran.Next(left, right);

            int leftDirection = -1;
            int rightDirection = 1;
            direction.X = randomStartDirection == 1 ? rightDirection : leftDirection;

            int minSpeed = 25;
            int maxSpeed = 101;
            int randomSpeed = ran.Next(minSpeed, maxSpeed);
            Speed = randomSpeed;
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
                Position += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
           // spriteBatch.Draw(Texture, Position, _currentClip.GetCurrentSourceRectangle(), Color, 0f, Origin, Size, currentDirection, LayerDepth);
           base.Draw(spriteBatch);
        }
        private void HandleAnimation(Vector2 dir, GameTime gameTime)
        {
            if (dir.Length() <= 0) return;
            else
            {
                AnimationFlip(-dir);
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
            if (LevelManager.GetCurrentLevel.IsTileWalkable(newDestination) && (LevelManager.GetCurrentLevel.IsGrounded(newDestination) || LevelManager.GetCurrentLevel.IsTileLadder(newDestination, (int)dir.Y)))
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
        //public bool HasCollided(Rectangle rec)
        //{
        //    return Collision.Intersects(rec);
        //}
    }
}
