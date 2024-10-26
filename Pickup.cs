using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    internal class Pickup : AnimatedGameObject
    {
        public Pickup(Texture2D texture, Vector2 position, float speed, Color color, float rotation, float size, float layerDepth, Vector2 origin, Dictionary<string, AnimationClip> animationClips) : base(texture, position, speed, color, rotation, size, layerDepth, origin, animationClips)
        {
        }
        public override void OnCollision(GameObject gameObject)
        {
            //Maybe add it back to a pool and respawn it
            GameManager.GameObjects.Remove(this);
        }
        public override void Update(GameTime gameTime)
        {
            Rotation += (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }
    }
}
