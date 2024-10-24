using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class PhysicsManager //Currently not using
    {
        public static Vector2 Velocity { get; set; }
        public static Vector2 Acceleration { get; set; }
        public static bool IsOnGround { get; set; }
        public static bool IsClimbing { get; set; }
        private const float GRAVITY = 9.81f;

        public PhysicsManager()
        {
            Velocity = Vector2.Zero;
            Acceleration = Vector2.Zero;
            IsOnGround = false;
            IsClimbing = false;
        }
        //public void Update(GameTime gameTime)
        //{
        //  ApplyPhysics(gameTime);
        //}
        public static Vector2 ApplyPhysics(GameTime gameTime, Vector2 pos, Vector2 direction, float speed)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Check if the player is on the ground or climbing
            IsOnGround = LevelManager.GetCurrentLevel.IsGrounded(pos);
            if(direction.Y != 0)
            IsClimbing = LevelManager.GetCurrentLevel.IsTileLadder(pos + direction * 40, (int)direction.Y);

            // Apply gravity if not climbing and not grounded
            if (IsClimbing)
            {
                // Handle climbing logic
                Velocity = direction * speed;
            }
            else if (!IsOnGround)
            {
                // Apply gravity when in the air
                Velocity = new Vector2(0, GRAVITY); // Accumulate gravity
            }
            else
            {
                // Reset vertical velocity when on the ground
                Velocity = new Vector2(direction.X * speed, 0);
            }

            // Ensure gravity is always pulling down when there's no vertical movement
            //if (!IsOnGround && direction.Y == 0 && !IsClimbing)
            //{
            //    Velocity = new Vector2(Velocity.X, Velocity.Y + GRAVITY * deltaTime);
            //}

            // Apply velocity to the position
            Vector2 newPosition = Velocity * deltaTime;
            return newPosition;
        }
    }
}
