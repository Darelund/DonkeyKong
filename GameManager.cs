using Microsoft.Xna.Framework;
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

        private static GameState _currentGameState = GameState.Playing;

       

        public static void Update(GameTime gameTime)
        {
            switch (_currentGameState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.Playing:
                    InputManager.Update();
                    foreach (var gameObject in _gameObjects)
                    {
                        gameObject.Update(gameTime);
                    }

                    for (int i = 0; i < _flashEffects.Count; i++)
                    {
                        if (!_flashEffects[i].IsActive)
                            _flashEffects.RemoveAt(i);
                        else
                            _flashEffects[i].Update(gameTime);
                    }
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                    break;
              
            }
           
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            switch (_currentGameState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.Playing:
                    spriteBatch.Begin();
                    Level.Draw(spriteBatch);

                    foreach (var gameObject in _gameObjects)
                    {
                        bool isFlashing = false;

                        foreach (var effect in _flashEffects)
                        {
                           if(effect.IsActiveOnObject(gameObject))
                            {
                                effect.ApplyDrawEffect(spriteBatch);
                                isFlashing = true;
                                break;
                            }
                        }
                        gameObject.Draw(spriteBatch);

                        if (isFlashing)
                        {
                            spriteBatch.End();
                            spriteBatch.Begin();
                        }
                    }
                    spriteBatch.End();
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                    break;
            }
        }
        public static void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public static void AddFlashEffect(FlashEffect flashEffect)
        {
            _flashEffects.Add(flashEffect);
        }
        public static void ChangeGameState(GameState newGameState )
        {
            if(newGameState == _currentGameState) return;
            _currentGameState = newGameState;
        }
    }
}
