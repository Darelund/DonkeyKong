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
        private float _extraspeed = 50;
        private float _speed => InputManager.IsLeftShiftDown() ? Speed + _extraspeed  : Speed;
        private bool moving = false;
        public float Health { get; private set; } = 3;
        public bool HasWon { get; set; } = false;
        public bool IsImmune { get; private set; } = false;
        private bool _attacking = false;

        private static PlayerController _instance;
        public static PlayerController Instance
        {
            get
            {
                return _instance;
            }
        }

        public PlayerController(Texture2D texture, Vector2 position, float speed, Color color, float rotation, float size, float layerDepth, Vector2 origin, Dictionary<string, AnimationClip> animationClips) : base(texture, position, speed, color, rotation, size, layerDepth, origin, animationClips)
        {
            _instance = this;
        }
        public override void Update(GameTime gameTime)
        {
           if(IsActive())
            {
                if (!moving)
                {
                    ChangeDirection(InputManager.GetMovement());
                }
                else
                {
                    Position += direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;


                    if (Vector2.Distance(Position, destination) < 1)
                    {
                        Position = destination;
                        moving = false;
                    }
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
            if (!IsActive())
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
                    }
                }
                else if (direction.Length() == 0)
                {
                    SwitchAnimation("Idle");
                }
                else if (InputManager.IsLeftShiftDown())
                {
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

            if (LevelManager.GetCurrentLevel.IsTileWalkable(newDestination))
            {
                destination = newDestination;
                moving = true;
            }
        }
        public void TakeDamage(int amount)
        {
            //Maybe add thís back later
           // if(!IsImmune)
            Health -= amount;
            if(Health <= 0)
            {
                _isActive = false;
            }
            Debug.WriteLine(Health);
        }
        public override void OnCollision(GameObject gameObject)
        {
            if (!IsActive()) return;
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
