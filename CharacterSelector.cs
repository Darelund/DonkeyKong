using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class CharacterSelector
    {
        private static List<UIElement> _SelectCharacterElements;
        private static TextInputManager _textInputManager;
        static CharacterSelector()
        {
            _SelectCharacterElements = new List<UIElement>
            {
                new UIText(ResourceManager.GetSpriteFont("GameText"), "Select a character", new Vector2(GameManager.Window.ClientBounds.Width / 2, 50), Color.White, 0.8f, Vector2.Zero, 0.9f),
                new UIImage(ResourceManager.GetTexture("mario-pauline-transparent"), new Vector2(330, 240), Color.White, 6, Vector2.Zero, new Rectangle(256, 1, 16, 16), 0.9f),
                new UIImage(ResourceManager.GetTexture("mario-pauline-transparent"), new Vector2(530, 240), Color.White, 6, Vector2.Zero, new Rectangle(256, 18, 16, 16), 0.9f),
                new Button(ResourceManager.GetSpriteFont("GameText"), (Color.White, Color.LightBlue, Color.DarkBlue), new Vector2(GameManager.Window.ClientBounds.Width / 2 - 100, GameManager.Window.ClientBounds.Height / 2), Vector2.Zero, 1, SelectCharacter, "Select", 1f, 0.1f),
                new Button(ResourceManager.GetSpriteFont("GameText"), (Color.White, Color.LightBlue, Color.DarkBlue), new Vector2(GameManager.Window.ClientBounds.Width / 2 + 100, GameManager.Window.ClientBounds.Height / 2), Vector2.Zero, 2, SelectCharacter, "Select", 1f, 0.1f)
            };
            _textInputManager = new TextInputManager("Enter your name", new Vector2(GameManager.Window.ClientBounds.Width / 2 - 200, GameManager.Window.ClientBounds.Height / 2 - 100), new Vector2(GameManager.Window.ClientBounds.Width / 2 - 150, GameManager.Window.ClientBounds.Height / 2));
        }
        public static void Update(GameTime gameTime)
        {
            if(!_textInputManager.IsInputComplete)
            {
                _textInputManager.Update();
            }
            else
            {
                foreach (UIElement element in _SelectCharacterElements)
                {
                    element.Update(gameTime);
                }

            }
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            if (!_textInputManager.IsInputComplete)
            {
                _textInputManager.Draw(spriteBatch);
            }
            else
            {
                foreach (UIElement element in _SelectCharacterElements)
                {
                    element.Draw(spriteBatch);
                }
            }
        }

        public static void SelectCharacter(object selectedOption)
        {
            int startScore = 0;
            int startLevel = 0;
            HighScore.UpdateScore(_textInputManager.InputText, startScore, startLevel);
            GameManager.Name = _textInputManager.InputText;
            if (selectedOption is int selection)
            {
                switch (selection)
                {
                    case 1:
                       GameFiles.Character.CHARACTER = "Content/MarioCharacter.txt";
                        break;
                    case 2:
                        GameFiles.Character.CHARACTER = "Content/PaulineCharacter.txt";
                        break;
                    default:
                        break;
                }
            }
            LevelManager.NextLevel(true);
            //GameManager.ChangeGameState(GameManager.GameState.Playing);
        }
    }
}
