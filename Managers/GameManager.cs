using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            GameOver,
            Victory
        }
        private static List<GameObject> _gameObjects = new List<GameObject>();
        private static List<FlashEffect> _flashEffects = new List<FlashEffect>();

        public static GameState CurrentGameState { get; private set; } = GameState.MainMenu;

        public static GameWindow Window;
        public static ContentManager Content;
        public static void Initialize(GameWindow window, ContentManager content)
        {
            Window = window;
            Content = content;
        }
        public static void ContentLoad()
        {
            CollisionManager._collisionObjects = _gameObjects;
            UIManager.LoadContent();
        }


        public static void Update(GameTime gameTime)
        {
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    InputManager.Update();
                    UIManager.Update(gameTime);
                    break;
                case GameState.Playing:
                    InputManager.Update();
                    UIManager.Update(gameTime);
                    foreach (var gameObject in _gameObjects)
                    {
                        gameObject.Update(gameTime);
                        if (gameObject is PlayerController)
                        {
                            var player = (PlayerController)gameObject;
                            // Store the Position in a local variable
                            Vector2 position = player.Position;

                            // Pass the local variable as ref
                            PhysicsManager.ApplyPhysics(gameTime, ref position);

                            // Assign the modified value back to the property
                            player.Position = position;
                        }
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
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointWrap);
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    UIManager.Draw(spriteBatch);
                    break;
                case GameState.Playing:
                   
                    LevelManager.Draw(spriteBatch);
                    UIManager.Draw(spriteBatch);

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
                        //Should this really be here?
                        if(gameObject is PlayerController)
                        {
                            var player = (PlayerController)gameObject;
                            CollisionManager.CheckCollision(player);
                        }
                        gameObject.Draw(spriteBatch);

                        if (isFlashing)
                        {
                            spriteBatch.End();
                            spriteBatch.Begin(SpriteSortMode.BackToFront);
                        }
                    }
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                    break;
            }
            spriteBatch.End();
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
            if(newGameState == CurrentGameState) return;
            CurrentGameState = newGameState;
        }
    }
}
