using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class PlayerController : AnimatedGameObject
    {
        private Vector2 destination;
        private Vector2 direction;
        private float speed = 100.0f;
        private bool moving = false;
        //TEST
        public float Health { get; private set; } = 3;
        public bool IsImmune { get; set; } = false;
        private Rectangle rec;

        public PlayerController(Texture2D texture, Vector2 position, float speed, Color color, float rotation, int size, float layerDepth, Vector2 origin, Dictionary<string, AnimationClip> animationClips) : base(texture, position, speed, color, rotation, size, layerDepth, origin, animationClips)
        {
            //CollisionManager.AddCollisionObject(this);
        }
        public override void Update(GameTime gameTime)
        {
           // rec = new Rectangle((int)Position.X - 17, (int)Position.Y - 17, 17 * 3, 17 * 3);
            if (!moving)
            {
                ChangeDirection(InputManager.GetMovement());
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
           // spriteBatch.DrawRectangle(rec, Color.White);

            spriteBatch.Draw(Texture, Position, _currentClip.GetCurrentSourceRectangle(), Color, 0f, Origin, Size, currentDirection, LayerDepth);

        }
        private SpriteEffects currentDirection = SpriteEffects.None;
        private void HandleAnimation(Vector2 dir, GameTime gameTime)
        {
            //If direction is 0, then we are not moving and should not animate(If I don't add idle)
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
        }
        public void TakeDamage(int amount)
        {
            if(!IsImmune)
            Health -= amount;
        }
        public void ImmuneHandler(bool immune)
        {
            IsImmune = immune;
        }
    }
}
