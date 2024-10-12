using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class GameManager
    {
        public enum GameState
        {
            MainMenu,
            Playing,
            Pause,
            GameOver
        }
        private static List<GameObject> _gameObjects = new List<GameObject>();
        private static List<FlashEffect> _flashEffects = new List<FlashEffect>();

        private static GameState _currentGameState = GameState.MainMenu;
        public static bool SpriteBatchActive { get; set; } = false;
        private static SpriteBatch _spriteBatch;

        public void LoadContent(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }
        public static void Update(SpriteBatch spriteBatch)
        {
            switch (_currentGameState)
            {
                case GameState.MainMenu:
                    if(!SpriteBatchActive)
                    {
                        _spriteBatch.Begin();
                        SpriteBatchActive = true;
                    }





                    _spriteBatch.End();
                    break;
                case GameState.Playing:
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                    break;
            }
        }
        public static void ChangeGameState(GameState newGameState )
        {
            if(newGameState == _currentGameState) return;
            _currentGameState = newGameState;
        }
    }
}
