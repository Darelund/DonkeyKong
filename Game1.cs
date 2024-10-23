using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace DonkeyKong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //PlayerController _playerController;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            int heightOffset = 200;
            int widthOffset = 2;
            int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - heightOffset;
            int fixedWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / widthOffset; // Define a fixed width like Space Invaders

            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.PreferredBackBufferWidth = fixedWidth;
            _graphics.ApplyChanges();

            GameManager.Initialize(Window, Content);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ResourceManager.LoadResources(Content, "SuperMarioFront,bridgeLadder,bridge,empty,ladder,MainMenu,DonkeyKongMainMenu1,DonkeyKongMainMenu2,mario-pauline-transparent,enemy_spritesheet-1,Background1,bridgeLeft,bridgeRight,largebridge,SuperMarioIdle", "", "", "GameText", "FlashEffect");
            var tilesLevel1 = new List<(char TileName, Texture2D tileTexture, TileType type, Color tileColor)>
            {
                ('B', ResourceManager.GetTexture("bridge"), TileType.NonWalkable, Color.Brown),
                 ('L', ResourceManager.GetTexture("largebridge"), TileType.NonWalkable, Color.Brown),
                ('b', ResourceManager.GetTexture("bridgeLadder"), TileType.Ladder, Color.DarkGreen),
                ('l', ResourceManager.GetTexture("ladder"), TileType.Ladder, Color.DarkGreen),
                ('-', ResourceManager.GetTexture("empty"), TileType.Walkable, Color.White)
            };

            LevelManager.AddLevel("Content/GameFiles", new Vector2(120, 120), tilesLevel1);

            float offsetBy17BecauseOfSprite = 17;

            Rectangle[] idleRectsPlayer = new Rectangle[]
          {
            new Rectangle(1*16, 0, 16, 16),
          };
            Rectangle[] walkRectsPlayer = new Rectangle[]
            {
            new Rectangle(0*16 + 1, 1, 16, 16),
            new Rectangle(1*16 + 2, 1, 16, 16),
            new Rectangle(2*16 + 3, 1, 16, 16)
            };
            Rectangle[] sprintRectsPlayer = new Rectangle[]
           {
            new Rectangle(0*16 + 1, 1, 16, 16),
            new Rectangle(1*16 + 2, 1, 16, 16),
            new Rectangle(2*16 + 3, 1, 16, 16)
           };
            Rectangle[] attackRectsPlayer = new Rectangle[]
          {
             new Rectangle(3*16 + 4, 1, 16, 16),
            new Rectangle(4*16 + 5, 1, 16, 16),
            new Rectangle(5*16 + 6, 1, 16, 16),
            new Rectangle(6*16 + 7, 1, 16, 16),
            new Rectangle(7*16 + 8, 1, 16, 16),
            new Rectangle(8*16 + 9, 1, 16, 16),
          };

            Rectangle[] deathRectsPlayer = new Rectangle[]
           {
            new Rectangle(15*16 + 16, 1, 16, 16),
            new Rectangle(16*16 + 17, 1, 16, 16),
            new Rectangle(17*16 + 18, 1, 16, 16),
             new Rectangle(18*16 + 19, 1, 16, 16),
            new Rectangle(19*16 + 20, 1, 16, 16),
           };
            var playerClips = new Dictionary<string, AnimationClip>()
            {
                {"Idle", new AnimationClip(idleRectsPlayer, 7f)},
                {"Walk", new AnimationClip(walkRectsPlayer, 7f)},
                {"Sprint", new AnimationClip(walkRectsPlayer, 10f)},
                {"Attack", new AnimationClip(attackRectsPlayer, 7f)},
                {"Die", new AnimationClip(deathRectsPlayer, 4f)}
            };

            Rectangle[] walkRectsEnemy = new Rectangle[]
            {
            new Rectangle(0*50, 0, 50, 26),
            new Rectangle(1*50, 0, 50, 26),
            new Rectangle(2*50, 0, 50, 26)
            };
            var enemyClips = new Dictionary<string, AnimationClip>()
            {
                {"Walking", new AnimationClip(walkRectsEnemy, 7f)}
            };
            GameManager.AddGameObject(new PlayerController(ResourceManager.GetTexture("mario-pauline-transparent"), new Vector2(320 + offsetBy17BecauseOfSprite, 120 + offsetBy17BecauseOfSprite), 10, Color.White, 0f, 3, 0f, new Vector2(8, 8), playerClips));

            //TODO Maybe an enemy manager? Need to handle this shit somehow
            //WHY DOES IT CREATE A BLACK BLOCK????
            //I "fixed" it by moving tiles layerdepth to 1, but why did adding enemies cause that??? Did that even fix that or did I just "hide" it?
            GameManager.AddGameObject(new EnemyController(ResourceManager.GetTexture("enemy_spritesheet-1"), new Vector2(280, 174 + 120), 10, Color.White, 0f, 1, 0.5f, Vector2.Zero, enemyClips));
            //GameManager.AddGameObject(new EnemyController(ResourceManager.GetTexture("enemy_spritesheet-1"), new Vector2(320, 254 + 40), 10, new Point(0, 0), new Point(50, 26), new Point(3, 1), Color.White, 0f, 1, 0f, Vector2.Zero, 100));
            //GameManager.AddGameObject(new EnemyController(ResourceManager.GetTexture("enemy_spritesheet-1"), new Vector2(120, 334 + 40), 10, new Point(0, 0), new Point(50, 26), new Point(3, 1), Color.White, 0f, 1, 0f, Vector2.Zero, 100));
            GameManager.ContentLoad();

            DebugRectangle.Init(GraphicsDevice, 17, 17);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GameManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GameManager.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }

    /*
     * LBBBBBBBBBBbBBBBBBBR
       L----------l-------R
       LBbBBBBBBBBBBBBBbBBR
       L-l-------------l--R
       LBBBBBBBBBBBBbBBBBBR
       L------------l-----R
       LBBBBbBBBBBBBBBBBBBR
       L----l-------------R
       LBBBBBBBBBBbBBBBBBBR
       L----------l-------R
       LBBBBBBBBBBBBBBBBBBR
     * 
     */
}
