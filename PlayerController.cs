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
        public PlayerController(Texture2D texture, Vector2 position, float speed, Point currentFrame, Point frameSize, Point sheetSize, Color color, int millisecondsPerFrame = 16) : base(texture, position, speed, currentFrame, frameSize, sheetSize, color, millisecondsPerFrame)
        {
        }
        public override void Update(GameTime gameTime)
        {
            if(!moving)
            {
                ChangeDirection(InputManager.GetMovement());
            }
            else
            {
               Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
               base.Update(gameTime);

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
            base.Draw(spriteBatch);
        }
        public void ChangeDirection(Vector2 dir)
        {
            direction = dir;
            Vector2 newDestination = Position + direction * 40.0f;

            //Check if we can move in the desired direction, if not, do nothing
            if (LevelManager.GetCurrentLevel.GetTileAtPosition(newDestination))
            {
                destination = newDestination;
                moving = true;
            }
        }
    }
}
