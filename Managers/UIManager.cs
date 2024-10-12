using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class UIManager
    {
        private static List<UIElement> _UIElements = new List<UIElement>();

        public static void LoadContent()
        {
            _UIElements.Add(new StaticBackground(ResourceManager.GetTexture("MainMenu"), Vector2.Zero, Color.White, 1f));
            _UIElements.Add(new Button(ResourceManager.GetSpriteFont("GameText"), (Color.White, Color.LightBlue, Color.DarkBlue), new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2), "Play"));
            _UIElements.Add(new AnimatedSpriteUI(ResourceManager.GetTexture("DonkeyKongMainMenu1"), new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2 - 200), new Point(0, 0), new Point(92, 110), new Point(4, 0), Color.White, 1f, 100));
        }
        public static void Update(GameTime gameTime)
        {
            switch (GameManager.CurrentGameState)
            {
                case GameManager.GameState.MainMenu:
                    _UIElements[0].Update(gameTime);
                    _UIElements[1].Update(gameTime);
                    _UIElements[2].Update(gameTime);
                    break;
                case GameManager.GameState.Playing:
                    break;
                case GameManager.GameState.Pause:
                    break;
                case GameManager.GameState.GameOver:
                    break;
                case GameManager.GameState.Victory:
                    break;
            }
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            switch (GameManager.CurrentGameState)
            {
                case GameManager.GameState.MainMenu:
                    _UIElements[0].Draw(spriteBatch);
                    _UIElements[1].Draw(spriteBatch);
                    _UIElements[2].Draw(spriteBatch);
                    break;
                case GameManager.GameState.Playing:
                    break;
                case GameManager.GameState.Pause:
                    break;
                case GameManager.GameState.GameOver:
                    break;
                case GameManager.GameState.Victory:
                    break;
            }
        }
    }
}
