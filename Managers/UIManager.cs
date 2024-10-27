using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DonkeyKong.GameManager;

namespace DonkeyKong
{
    public class UIManager
    {
        private static List<UIElement> _MainMenuElements;
        private static List<UIElement> _PlayingElements;
        private static List<UIElement> _PauseElements;
        private static List<UIElement> _GameOverElements;
        private static List<UIElement> _VictoryElements;
        private static PlayerHUD _PlayerHUD;

        //MainMenu

        //Playing

        //Pause

        //GameOver

        //Victory

        public static void LoadContent()
        {
            _MainMenuElements = new List<UIElement>();
            _PlayingElements = new List<UIElement>();
            _PauseElements = new List<UIElement>();
            _GameOverElements = new List<UIElement>();
            _VictoryElements = new List<UIElement>();
            _PlayerHUD = new PlayerHUD();

            _MainMenuElements.Add(new StaticBackground(ResourceManager.GetTexture("MainMenu_transparent"), new Vector2(GameManager.Window.ClientBounds.Width / 2, 200), Color.White, 0.9f, new Vector2(ResourceManager.GetTexture("MainMenu_transparent").Width /2, ResourceManager.GetTexture("MainMenu_transparent").Height /2), 0.2f));
            _MainMenuElements.Add(new StaticBackground(ResourceManager.GetTexture("MainMenu_transparent"), new Vector2(GameManager.Window.ClientBounds.Width / 2, 200), Color.LightBlue, 0.95f, new Vector2(ResourceManager.GetTexture("MainMenu_transparent").Width / 2, ResourceManager.GetTexture("MainMenu_transparent").Height / 2), 0.2f));
            _MainMenuElements.Add(new Button(ResourceManager.GetSpriteFont("GameText"), (Color.White, Color.LightBlue, Color.DarkBlue), new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2), Vector2.Zero, GameManager.GameState.SelectCharacter, GameManager.ChangeGameState, "Play", 1f, 0.1f));
            _MainMenuElements.Add(new AnimatedSpriteUI(ResourceManager.GetTexture("DonkeyKongMainMenu1_transparent"), new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2 - 350), new Point(0, 0), new Point(92, 110), new Point(4, 0), Color.White, 1f, Vector2.Zero, 100));
            _MainMenuElements.Add(new AnimatedSpriteUI(ResourceManager.GetTexture("DonkeyKongMainMenu2_transparent"), new Vector2(GameManager.Window.ClientBounds.Width / 2 - 165, GameManager.Window.ClientBounds.Height / 2 - 365), new Point(0, 0), new Point(80, 110), new Point(4, 0), Color.White, 1f, Vector2.Zero, 150));

            _PlayingElements.Add(new StaticBackground(ResourceManager.GetTexture("Background1"), new Vector2(-200, 0), Color.White, 1, Vector2.Zero, 1f));


            _GameOverElements.Add(new StaticBackground(ResourceManager.GetTexture("loose"), new Vector2(0, 0), Color.White, 1, Vector2.Zero, 1f));
            _GameOverElements.Add(new Button(ResourceManager.GetSpriteFont("GameText"), (Color.White, Color.Red, Color.DarkRed), new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2 + 300), Vector2.Zero, GameManager.GameState.Playing, GameManager.ChangeGameState, "Play Again?"));

            _VictoryElements.Add(new Button(ResourceManager.GetSpriteFont("GameText"), (Color.Green, Color.Red, Color.DarkRed), new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2 + 100), Vector2.Zero, GameManager.GameState.Victory, GameManager.ChangeGameState, "Victory"));
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
                case GameState.SelectCharacter:
                    break;
                case GameManager.GameState.Playing:
                    foreach (UIElement element in _PlayingElements)
                    {
                        element.Update(gameTime);
                    }
                    _PlayerHUD.Update(gameTime);
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
                //case GameManager.GameState.Restart:
                //    break;
                //case GameManager.GameState.Exit:
                //    break;
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
                case GameState.SelectCharacter:
                    break;
                case GameManager.GameState.Playing:
                    foreach (UIElement element in _PlayingElements)
                    {
                        element.Draw(spriteBatch);
                    }
                    _PlayerHUD.Draw(spriteBatch);
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
                    foreach (UIElement element in _VictoryElements)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                //case GameManager.GameState.Restart:
                //    break;
                //case GameManager.GameState.Exit:
                //    break;
            }
        }
    }
}
