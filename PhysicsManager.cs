using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class PhysicsManager
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
        public static void ApplyPhysics(GameTime gameTime, ref Vector2 pos, Vector2 inputDirection, float speed)
        {
            IsOnGround = LevelManager.GetCurrentLevel.IsTileWalkable(pos);
            IsClimbing = LevelManager.GetCurrentLevel.IsTileLadder(pos);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Handle climbing
            if (IsClimbing)
            {
                Velocity = new Vector2(Velocity.X, 0); // No gravity while climbing
                                                       // Allow climbing based on vertical input
                Velocity = new Vector2(Velocity.X, inputDirection.Y * speed);
            }
            else if (!IsOnGround)
            {
                // Apply gravity if not on the ground
                Velocity = new Vector2(Velocity.X, Velocity.Y + GRAVITY * deltaTime);
            }

            // Apply horizontal movement based on input direction
            if (!IsClimbing) // Do not apply horizontal movement while climbing
            {
                Velocity = new Vector2(inputDirection.X * speed, Velocity.Y);
            }

            // Apply velocity to the position
            Vector2 newPosition = Velocity * deltaTime;

            pos += newPosition;
        }
    }
}
