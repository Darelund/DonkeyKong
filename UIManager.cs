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
        private static List<Button> _buttons = new List<Button>();

        public static void LoadContent()
        {
            _buttons.Add(new Button(ResourceManager.GetSpriteFont("GameText"), (Color.White, Color.LightBlue, Color.DarkBlue), new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2), "Play"));
        }
        public static void Update(GameTime gameTime)
        {
            switch (GameManager.CurrentGameState)
            {
                case GameManager.GameState.MainMenu:
                    _buttons[0].Update(gameTime);
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
                    _buttons[0].Draw(spriteBatch);
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
