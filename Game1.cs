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

            ResourceManager.LoadResources(Content, "SuperMarioFront,bridgeLadder,bridge,empty,ladder,MainMenu,DonkeyKongMainMenu1,DonkeyKongMainMenu2,mario-pauline-transparent,enemy_spritesheet-1,Background1,bridgeLeft,bridgeRight,largebridge", "", "", "GameText", "FlashEffect");
            var tilesLevel1 = new List<(char TileName, Texture2D tileTexture, bool notWalkable, Color tileColor)>
            {
                ('B', ResourceManager.GetTexture("bridge"), true, Color.Brown),
                 ('L', ResourceManager.GetTexture("largebridge"), true, Color.Brown),
                ('b', ResourceManager.GetTexture("bridgeLadder"), false, Color.DarkGreen),
                ('l', ResourceManager.GetTexture("ladder"), false, Color.DarkGreen),
                ('-', ResourceManager.GetTexture("empty"), false, Color.White)
            };

            LevelManager.AddLevel("Content/GameFiles", new Vector2(120, 120), tilesLevel1);

            float offsetBy17BecauseOfSprite = 17;

            Rectangle[] walkRects = new Rectangle[]
            {
            new Rectangle(0*16 + 1, 1, 16, 16),
            new Rectangle(1*16 + 2, 1, 16, 16),
            new Rectangle(2*16 + 3, 1, 16, 16)
            };
            var playerClips = new Dictionary<string, AnimationClip>()
            {
                {"Walking", new AnimationClip(walkRects, 7f)}
            };
            GameManager.AddGameObject(new PlayerController(ResourceManager.GetTexture("mario-pauline-transparent"), new Vector2(280 + offsetBy17BecauseOfSprite, 120 + offsetBy17BecauseOfSprite), 10, Color.White, 0f, 3, 0f, new Vector2(8.5f, 8.5f), playerClips));

            //TODO Maybe an enemy manager? Need to handle this shit somehow
            //WHY DOES IT CREATE A BLACK BLOCK????
            //I "fixed" it by moving tiles layerdepth to 1, but why did adding enemies cause that??? Did that even fix that or did I just "hide" it?
           // GameManager.AddGameObject(new EnemyController(ResourceManager.GetTexture("enemy_spritesheet-1"), new Vector2(120, 174 + 40), 10, new Point(0, 0), new Point(50, 26), new Point(3, 1), Color.White, 0f, 1, 0.5f, Vector2.Zero, 100));
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
