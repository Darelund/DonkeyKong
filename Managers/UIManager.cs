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
    public class UIManager
    {
        private static List<UIElement> _MainMenuElements = new List<UIElement>();
        private static List<UIElement> _PlayingElements = new List<UIElement>();
        private static List<UIElement> _PauseElements = new List<UIElement>();
        private static List<UIElement> _GameOverElements = new List<UIElement>();
        private static List<UIElement> _VictoryElements = new List<UIElement>();

        public static void LoadContent()
        {
            _MainMenuElements.Add(new StaticBackground(ResourceManager.GetTexture("MainMenu"), Vector2.Zero, Color.White, 1f, 1f));
            _MainMenuElements.Add(new Button(ResourceManager.GetSpriteFont("GameText"), (Color.White, Color.LightBlue, Color.DarkBlue), new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2), "Play", 1f, 0.1f));
            _MainMenuElements.Add(new AnimatedSpriteUI(ResourceManager.GetTexture("DonkeyKongMainMenu1"), new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2 - 200), new Point(0, 0), new Point(92, 110), new Point(4, 0), Color.White, 1f, 100));

            _PlayingElements.Add(new StaticBackground(ResourceManager.GetTexture("Background1"), new Vector2(-200, 0), Color.White, 1, 1f));

            _GameOverElements.Add(new StaticBackground(ResourceManager.GetTexture("loose"), new Vector2(0, 0), Color.White, 1, 1f));
            _GameOverElements.Add(new Button(ResourceManager.GetSpriteFont("GameText"), (Color.White, Color.Red, Color.DarkRed), new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2 + 100), "Play Again?"));
        }
        public static void Update(GameTime gameTime)
        {
            switch (GameManager.CurrentGameState)
            {
                case GameManager.GameState.MainMenu:
                    foreach (UIElement element in _MainMenuElements)
                    {
                        element.Update(gameTime);
                    }
                    break;
                case GameManager.GameState.Playing:
                    break;
                case GameManager.GameState.Pause:
                    break;
                case GameManager.GameState.GameOver:
                    foreach (UIElement element in _GameOverElements)
                    {
                        element.Update(gameTime);
                    }
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
                    foreach (UIElement element in _MainMenuElements)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameManager.GameState.Playing:
                    foreach (UIElement element in _PlayingElements)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameManager.GameState.Pause:
                    break;
                case GameManager.GameState.GameOver:
                    foreach (UIElement element in _GameOverElements)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameManager.GameState.Victory:
                    break;
            }
        }
    }
}
