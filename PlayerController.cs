﻿using Microsoft.Xna.Framework;
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
        public bool IsImmune { get; private set; } = false;
        //private Rectangle rec;

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

            //if(LevelManager.GetCurrentLevel.IsGrounded(Position))
            //{
            //    Position += new Vector2(0, 10 * (float)gameTime.ElapsedGameTime.TotalSeconds);
            //}
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
           // spriteBatch.DrawRectangle(rec, Color.White);

           // spriteBatch.Draw(Texture, Position, _currentClip.GetCurrentSourceRectangle(), Color, 0f, Origin, Size, currentDirection, LayerDepth);
            base.Draw(spriteBatch);
        }
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
            float tileSize = 40.0f;
            Vector2 newDestination = Position + direction * tileSize;

            if (!(LevelManager.GetCurrentLevel.IsGrounded(Position)) && !(LevelManager.GetCurrentLevel.IsTileLadder(newDestination, (int)direction.Y)))
            {
                direction = new Vector2(0, 1);
                newDestination = Position + direction * tileSize;
                destination = newDestination;
                moving = true;
                return;
            }
            if (direction.Y != 0)
            {
                if(LevelManager.GetCurrentLevel.IsTileLadder(newDestination, (int)direction.Y))
                {
                    destination = newDestination;
                    moving = true;
                }
                return;
            }

            // Horizontal walkability check (optional, depends on your design)
            if (LevelManager.GetCurrentLevel.IsTileWalkable(newDestination))
            {
                destination = newDestination;
                moving = true;
            }

            //if (LevelManager.GetCurrentLevel.IsTileWalkable(newDestination))
            //{
            //    destination = newDestination;
            //    moving = true;
            //}
        }
        public void TakeDamage(int amount)
        {
            //Maybe add thís back later
           // if(!IsImmune)
            Health -= amount;
            if(Health <= 0)
            {
                GameManager.RemoveGameObject(this);
            }
            Debug.WriteLine(Health);
        }
        public void ImmuneHandler(bool immune)
        {
            IsImmune = immune;
        }
    }
}
