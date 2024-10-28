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

        private const string _fontUsed = "GameText";

        //MainMenu
        private const string _maintex1 = "MainMenu_transparent";
        private const string _maintex2 = "DonkeyKongMainMenu1_transparent";
        private const string _maintex3 = "DonkeyKongMainMenu2_transparent";
        private static readonly Vector2 _staticBackgroundPosition = new Vector2(GameManager.Window.ClientBounds.Width / 2, 200);
        private static readonly Vector2 _staticBackgroundOrigin = new Vector2(ResourceManager.GetTexture(_maintex1).Width / 2, ResourceManager.GetTexture(_maintex1).Height / 2);
        private static readonly Color _mainColor1 = Color.White;
        private static readonly Color _mainColor2 = Color.LightBlue;
        private const float _mainSize1 = 0.90f;
        private const float _mainSize2 = 0.95f;
        private const float _mainlayerDepth = 0.2f;

        //Playing
        private const string _backgroundtex = "Background1";

        //Pause

        //GameOver
        private const string _playingtex = "loose";

        //Victory
        private static readonly (Color color1, Color color2, Color color3) _buttonColors = (Color.Green, Color.Red, Color.DarkRed);
        private static readonly Vector2 _playingPos = new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2 + 100);
        private static readonly Vector2 _playingOrigin = Vector2.Zero;
        private const GameManager.GameState _victoryState = GameState.Victory;
        private static readonly Action<object> _victory = GameManager.ChangeGameState;
        private const string _victoryText = "Victory";



        public static void LoadContent()
        {
            _MainMenuElements = new List<UIElement>();
            _PlayingElements = new List<UIElement>();
            _PauseElements = new List<UIElement>();
            _GameOverElements = new List<UIElement>();
            _VictoryElements = new List<UIElement>();
            _PlayerHUD = new PlayerHUD();

            _MainMenuElements.Add(new StaticBackground(ResourceManager.GetTexture(_maintex1), _staticBackgroundPosition, _mainColor1, _mainSize1, _staticBackgroundOrigin, _mainlayerDepth));
            _MainMenuElements.Add(new StaticBackground(ResourceManager.GetTexture(_maintex1), _staticBackgroundPosition, _mainColor2, _mainSize2, _staticBackgroundOrigin, _mainlayerDepth));
            _MainMenuElements.Add(new Button(ResourceManager.GetSpriteFont(_fontUsed), (Color.White, Color.LightBlue, Color.DarkBlue), new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2), Vector2.Zero, GameManager.GameState.SelectCharacter, GameManager.ChangeGameState, "Play", 1f, 0.1f));
            _MainMenuElements.Add(new AnimatedSpriteUI(ResourceManager.GetTexture(_maintex2), new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2 - 350), new Point(0, 0), new Point(92, 110), new Point(4, 0), Color.White, 1f, Vector2.Zero, 100));
            _MainMenuElements.Add(new AnimatedSpriteUI(ResourceManager.GetTexture(_maintex3), new Vector2(GameManager.Window.ClientBounds.Width / 2 - 165, GameManager.Window.ClientBounds.Height / 2 - 365), new Point(0, 0), new Point(80, 110), new Point(4, 0), Color.White, 1f, Vector2.Zero, 150));

            _PlayingElements.Add(new StaticBackground(ResourceManager.GetTexture(_backgroundtex), new Vector2(-200, 0), Color.White, 1, Vector2.Zero, 1f));


            _GameOverElements.Add(new StaticBackground(ResourceManager.GetTexture(_playingtex), new Vector2(0, 0), Color.White, 1, Vector2.Zero, 1f));
            _GameOverElements.Add(new Button(ResourceManager.GetSpriteFont(_fontUsed), (Color.White, Color.Red, Color.DarkRed), new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2 + 300), Vector2.Zero, GameManager.GameState.Restart, GameManager.ChangeGameState, "Play Again?"));

            _VictoryElements.Add(new Button(ResourceManager.GetSpriteFont(_fontUsed), _buttonColors, _playingPos, _playingOrigin, _victoryState, _victory, _victoryText));
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
            }
        }
    }
}
