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

            ResourceManager.LoadResources(Content, "SuperMarioFront,bridgeLadder,bridge,empty,ladder,MainMenu,DonkeyKongMainMenu1,DonkeyKongMainMenu2,mario-pauline-transparent", "", "", "GameText", "FlashEffect");
            var tilesLevel1 = new List<(char TileName, Texture2D tileTexture, bool notWalkable)>
            {
                ('B', ResourceManager.GetTexture("bridge"), true),
                ('b', ResourceManager.GetTexture("bridgeLadder"), false),
                ('L', ResourceManager.GetTexture("ladder"), false),
                ('-', ResourceManager.GetTexture("empty"), false)
            };

            LevelManager.AddLevel("Content/GameFiles", new Vector2(120, 120), tilesLevel1);

            GameManager.AddGameObject(new PlayerController(ResourceManager.GetTexture("mario-pauline-transparent"), new Vector2(120, 160), 10, new Point(0, 0), new Point(17, 17), new Point(3, 1), Color.White, 0f, 3, 0f, new Vector2(8.5f, 8.5f), 100));
            GameManager.ContentLoad();
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
}
