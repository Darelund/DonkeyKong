using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class DonkeyKong : AnimatedGameObject
    {
        private Vector2 destination;
        public Vector2 direction { get; private set; }
        private float _sprintSpeed = 50;
        private float _fallSpeed = 150f;
        private bool _moving = false;
        public DonkeyKong(Texture2D texture, Vector2 position, float speed, Color color, float rotation, float size, float layerDepth, Vector2 origin, Dictionary<string, AnimationClip> animationClips) : base(texture, position, speed, color, rotation, size, layerDepth, origin, animationClips)
        {
        }
        public override void Update(GameTime gameTime)
        {
            // base.Update(gameTime);
            if(!_moving)
            {
                ChangeDirection();

            }
            else
            {
                Position += direction * _fallSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(Vector2.Distance(Position, destination) < 1)
                {
                    Position = destination;
                    _moving = false;
                }
            }
            HandleAnimation(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        private void HandleAnimation(GameTime gameTime)
        {
            if (!(LevelManager.GetCurrentLevel.IsGrounded(Position)))
            {
                SwitchAnimation("Die");
            }
            base.Update(gameTime);
        }
        private void AnimationFlip(Vector2 dir)
        {
            currentDirection = dir.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        }
        //Doesn't walk, probably in the middle
        public void ChangeDirection()
        {

            float tileSize = 40.0f;
            // Vector2 newDestination = Position + direction * tileSize;
            Vector2 newDestination;
            direction = new Vector2(0, 0);
            if (!(LevelManager.GetCurrentLevel.IsGrounded(Position)))
            {
                direction = new Vector2(0, 1);
                newDestination = Position + direction * tileSize;
                destination = newDestination;
                _moving = true;
            }
        }
        public void TakeDamage(int amount)
        {
            ////Maybe add thís back later
            //// if(!IsImmune)
            //_health -= amount;
            //if (_health <= 0)
            //{
            //    _isActive = false;
            //    int score = 50;
            //    ScoreManager.UpdateScore(score);
            //}
        }
    }
}
