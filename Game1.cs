using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace DonkeyKong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        PlayerController _playerController;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
        
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ResourceManager.LoadResources(Content, "SuperMarioFront,bridgeLadder,bridge,empty,ladder", "", "", "", "Effect/FlashEffect");
            _playerController = new PlayerController(ResourceManager.GetTexture("SuperMarioFront"), new Vector2(200, 200), 10, new Point(0, 3), new Point(0, 3), new Point(0, 3), Color.White);
            Level.CreateLevel("Content/GameFiles");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _playerController.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            _spriteBatch.Begin();
            _playerController.Draw(_spriteBatch);
            Level.Draw(_spriteBatch);
            _spriteBatch.End();
          

         

            base.Draw(gameTime);
        }
      
    }
}
