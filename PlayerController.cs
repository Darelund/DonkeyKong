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
        private float _speed => InputManager.IsLeftShiftDown() ? 150.0f : 100.0f;
        private bool moving = false;
        //TEST
        public float Health { get; private set; } = 3;
        public bool IsAlive { get; private set; } = true;
        public bool IsImmune { get; private set; } = false;
        private bool _attacking = false;
        //private Rectangle rec;

        public PlayerController(Texture2D texture, Vector2 position, float speed, Color color, float rotation, int size, float layerDepth, Vector2 origin, Dictionary<string, AnimationClip> animationClips) : base(texture, position, speed, color, rotation, size, layerDepth, origin, animationClips)
        {
            //CollisionManager.AddCollisionObject(this);
        }
        public override void Update(GameTime gameTime)
        {
           // rec = new Rectangle((int)Position.X - 17, (int)Position.Y - 17, 17 * 3, 17 * 3);
           if(IsAlive)
            {
                if (!moving)
                {
                    ChangeDirection(InputManager.GetMovement());
                }
                else
                {
                    Position += direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;


                    //Check if we are near enough to the destination
                    if (Vector2.Distance(Position, destination) < 1)
                    {
                        Position = destination;
                        moving = false;
                    }
                }
            }
                HandleAnimation(gameTime);

            //if(LevelManager.GetCurrentLevel.IsGrounded(Position))
            //{
            //    Position += new Vector2(0, 10 * (float)gameTime.ElapsedGameTime.TotalSeconds);
            //}
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        private void HandleAnimation(GameTime gameTime)
        {
            //If direction is 0, then we are not moving and should not animate(If I don't add idle)
            if (!IsAlive)
            {
                SwitchAnimation("Die");
            }
            else
            {
                if(InputManager.LeftClick() && !_attacking)
                {
                    _attacking = true;
                    SwitchAnimation("Attack");
                }
                if (_attacking)
                {
                    if (_currentClip.HasLoopedOnce())
                    {
                        _attacking = false;
                        Debug.WriteLine("Attacking is done");
                    }
                   // AnimationFlip();
                }
                else if (direction.Length() == 0)
                {
                    SwitchAnimation("Idle");
                    // AnimationFlip();
                }
                else if (InputManager.IsLeftShiftDown())
                {
                    Debug.WriteLine("Sprint");
                    SwitchAnimation("Sprint");
                    AnimationFlip();
                }
                else if (direction.Length() != 0)
                {
                    SwitchAnimation("Walk");
                    AnimationFlip();
                }
            }
            base.Update(gameTime);
        }
        private void AnimationFlip()
        {
            currentDirection = direction.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
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
                IsAlive = false;
                //  GameManager.RemoveGameObject(this);
               // SwitchAnimation("Dying");
            }
            Debug.WriteLine(Health);
        }
        public override void OnCollision(GameObject gameObject)
        {
            if (!IsAlive) return;
            if (gameObject is EnemyController)
            {
                // var enemsy = (EnemyController)gameObject;
                if (!IsImmune)
                {
                    Debug.WriteLine("I get called");
                    var flash = new FlashEffect(ResourceManager.GetEffect("FlashEffect"), 2f, this, Color.White);
                    GameManager.AddFlashEffect(flash);
                    IsImmune = true;
                    flash.OnFlashing += ImmuneHandler;
                    //YEs memory leak, so what??? Or is it?
                    TakeDamage(1);
                }
            }
        }
        public void ImmuneHandler(bool immune)
        {
            IsImmune = immune;
        }
    }
}
