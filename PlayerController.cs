using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class PlayerController : AnimatedGameObject
    {
        public bool wasHit = false;
        public PlayerController(Texture2D texture, Vector2 position, float speed, Point currentFrame, Point frameSize, Point sheetSize, Color color, int millisecondsPerFrame = 16) : base(texture, position, speed, currentFrame, frameSize, sheetSize, color, millisecondsPerFrame)
        {
        }
        public override void Update(GameTime gameTime)
        {


            Position += InputManager.GetMovement();
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if(wasHit)
            {
                Color = Color.GhostWhite;

            }
           
            base.Draw(spriteBatch);
        }
    }
}
