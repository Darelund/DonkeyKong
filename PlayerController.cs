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
        public Vector2 direction { get; private set; }
        private float _sprintSpeed = 50;
        private float _fallSpeed = 50f;
        protected override float Speed
        {
            get
            {
                if (InputManager.IsLeftShiftDown() && LevelManager.GetCurrentLevel.IsGrounded(Position))
                {
                    return normalSpeed + _sprintSpeed;
                }
                else if (!LevelManager.GetCurrentLevel.IsGrounded(Position) && !LevelManager.GetCurrentLevel.IsTileLadder(Position))
                {
                    return normalSpeed + _fallSpeed;
                }
                else
                {
                    return normalSpeed;
                }
            }
        }
        private bool moving = false;
        private float _health = 3;
        public float Health
        {
            get => _health;
        }
        public bool IsImmune { get; private set; } = false;
        private bool _attacking = false;
        //private bool _jumping = false;
        private const string _attackAnim = "Die";
        private const string _dieAnim = "Attack";
        private const string _idleAnim = "Idle";
        private const string _walkAnim = "Walk";
        private const string _sprintAnim = "Sprint";
        private const string _climbAnim = "Climb";



        public Inventory Inventory { get; set; }


      

        private List<Vector2> positions;
        private static PlayerController _instance;
        public static PlayerController Instance
        {
            get
            {  if (_instance != null)
                    return _instance;
                else
                    return null;
            }
        }

        public PlayerController(Texture2D texture, Vector2 position, float speed, Color color, float rotation, float size, float layerDepth, Vector2 origin, Dictionary<string, AnimationClip> animationClips) : base(texture, position, speed, color, rotation, size, layerDepth, origin, animationClips)
        {
            _instance = this;
            Inventory = new Inventory(this);
        }
        public override void Update(GameTime gameTime)
        {
            Inventory.Update(gameTime);



           if (IsActive())
            {
                if (!moving)
                {
                    ChangeDirection(InputManager.GetMovement());
                }
                else
                {
                    Position += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;


                    if (Vector2.Distance(Position, destination) < 1)
                    {
                        Position = destination;
                        moving = false;
                    }
                }
            }
                HandleAnimation(gameTime);

            if(InputManager.DebugButton())
            {
                foreach (var item in Inventory.Items)
                {
                    Debug.WriteLine($"Type: {item.Type}");
                }
            }
           
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Inventory.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
        private void HandleAnimation(GameTime gameTime)
        {
            if (!IsActive())
            {
                SwitchAnimation(_dieAnim);
            }
            else
            {
                if(InputManager.LeftClick() && !_attacking)
                {
                    _attacking = true;
                    SwitchAnimation(_attackAnim);
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
                    SwitchAnimation(_idleAnim);
                }
                else if (direction.X != 0)
                {
                    SwitchAnimation(_walkAnim);
                    AnimationFlip();
                }
                else if (InputManager.IsLeftShiftDown())
                {
                    SwitchAnimation(_sprintAnim);
                    AnimationFlip();
                }
                else if (LevelManager.GetCurrentLevel.IsTileLadder(Position) && LevelManager.GetCurrentLevel.IsTileLadder(Position - new Vector2(0, 40)))
                {
                    SwitchAnimation(_climbAnim);
                    if(_currentClip.HasLoopedOnce())
                    {
                        FlipClimbing();
                    }
                }
                else if(IsTopOfLadderReached())
                {
                    SwitchAnimation(_idleAnim);
                }
                else
                {
                    SwitchAnimation(_idleAnim);
                }
            }
            base.Update(gameTime);
        }
        private bool IsTopOfLadderReached()
        {
            return LevelManager.GetCurrentLevel.IsTileWalkable(Position) && LevelManager.GetCurrentLevel.IsTileLadder(Position - new Vector2(0, 40));
        }
        private void AnimationFlip()
        {
            if (direction.X != 0) currentDirection = direction.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        }
        private void FlipClimbing()
        {

            currentDirection = currentDirection == SpriteEffects.None ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }
        public void ChangeDirection(Vector2 dir)
        {

            direction = dir;
            float tileSize = 40.0f;
            Vector2 newDestination = Position + direction * tileSize;

            if (!(LevelManager.GetCurrentLevel.IsGrounded(Position)) && !(LevelManager.GetCurrentLevel.IsTileLadder(Position)))
            {
                direction = new Vector2(0, 1);
                newDestination = Position + direction * tileSize;
                destination = newDestination;
                moving = true;
            }
            else if (direction.Y != 0)
            {
                if(LevelManager.GetCurrentLevel.IsTileLadder(newDestination))
                {
                    destination = newDestination;
                    moving = true;
                }
            }
            else if (LevelManager.GetCurrentLevel.IsTileWalkable(newDestination))
            {
                destination = newDestination;
                moving = true;
            }
        }
        public void TakeDamage(int amount)
        {
            //Maybe add thís back later
           // if(!IsImmune)
            _health -= amount;
            if(_health <= 0)
            {
                _isActive = false;
                HighScore.UpdateScore(GameManager.Name, ScoreManager.PlayerScore, LevelManager.LevelIndex);
                AudioManager.PlaySoundEffect("DeathSound");
                ScoreManager.ResetScore();
            }
            Debug.WriteLine(_health);
        }
        public override void OnCollision(GameObject gameObject)
        {
            if (!IsActive()) return;
            if (gameObject is EnemyController)
            {
                // var enemsy = (EnemyController)gameObject;
                if (!IsImmune)
                {
                    Debug.WriteLine("Taking damage");
                    float volume = 0.5f;
                    AudioManager.PlaySoundEffect("FlameDamage", volume);
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
