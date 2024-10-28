using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
namespace DonkeyKong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private const string _textures = "SuperMarioFront,bridgeLadder,bridge,empty,ladder,MainMenu,DonkeyKongMainMenu1,DonkeyKongMainMenu2,mario-pauline-transparent,enemy_spritesheet-1,Background1,bridgeLeft,bridgeRight,largebridge,SuperMarioIdle,loose,FlagPole,stuff_mod_transparent,sprint,MainMenu_transparent,DonkeyKongMainMenu1_transparent,DonkeyKongMainMenu2_transparent,Consumable,DK_mod_mario,DK_mod_mario_transparent";
        private const string _sounds = "DeathSound,HardPop,FlameDamage,CoinPickupSound";
        private const string _music = "BackgroundMusic";
        private const string _font = "GameText";
        private const string _effects = "FlashEffect";


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
            int fixedWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / widthOffset;

            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.PreferredBackBufferWidth = fixedWidth;
            _graphics.ApplyChanges();



            GameManager.SetUp(Window, Content, GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ResourceManager.LoadResources(Content, _textures, _sounds, _music, _font, _effects);
          
           
            GameManager.ContentLoad();

            //Testing purposes
          //  DebugRectangle.Init(GraphicsDevice, 17, 17);
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
