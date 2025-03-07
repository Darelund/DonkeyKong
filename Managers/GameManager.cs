﻿using Microsoft.Xna.Framework;
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
            SelectCharacter,
            Playing,
            Pause,
            GameOver,
            Victory,
            Restart,
            Exit
        }
        public static List<GameObject> GameObjects { get; set; } = new List<GameObject>();
        public static List<GameObject> GetGameObjects => GameObjects;
        private static List<FlashEffect> _flashEffects = new List<FlashEffect>();

        public static GameState CurrentGameState { get; private set; } = GameState.MainMenu;

        public static GameWindow Window;
        public static ContentManager Content;
        public static GraphicsDevice Device;
        public static string Name { get; set; }
        private static SceneSwitcher _sceneSwitcher;


        public static event Action<Color, GameState> OnPlaying, OnMainMenu, OnGameOver, OnWin, OnPause;

        public static void SetUp(GameWindow window, ContentManager content, GraphicsDevice device)
        {

            Window = window;
            Content = content;
            Device = device;
            _sceneSwitcher = new SceneSwitcher(Window, Device);
        }
        public static void ContentLoad()
        {
            UIManager.LoadContent();
            AudioManager.LoadContent();
            HighScore.LoadScores();
            LevelManager.CreateLevels();
            LevelManager.SpecificLevel(0, false);
        }


        public static void Update(GameTime gameTime)
        {
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    InputManager.Update();
                    LevelManager.Update(gameTime);
                    UIManager.Update(gameTime);
                    foreach (var gameObject in GameObjects)
                    {
                        gameObject.Update(gameTime);
                    }
                    break;
                case GameState.SelectCharacter:
                    InputManager.Update();
                    CharacterSelector.Update(gameTime);
                    break;
                case GameState.Playing:
                    InputManager.Update();
                    UIManager.Update(gameTime);
                    LevelManager.Update(gameTime);
                    foreach (var gameObject in GameObjects)
                    {
                        gameObject.Update(gameTime);

                        if (gameObject is PlayerController)
                        {
                            var player = gameObject as PlayerController;
                            if (player.Health <= 0)
                            {
                                OnGameOver?.Invoke(Color.Black, GameState.GameOver);
                            }

                            if (LevelManager.GetCurrentLevel.LevelCompleted)
                            {
                                OnWin?.Invoke(Color.Green, GameState.Victory);
                            }
                        }
                    }
                    for (int i = 0; i < _flashEffects.Count; i++)
                    {
                        if (!_flashEffects[i].IsActive)
                            _flashEffects.RemoveAt(i);
                        else
                            _flashEffects[i].Update(gameTime);
                    }
                    CollisionManager.CheckCollision();
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                    InputManager.Update();
                    UIManager.Update(gameTime);
                    break;
                case GameState.Victory:
                    LevelManager.NextLevel(true);
                  //  LevelManager.SpecificLevel(5, true);
                    break;
                case GameState.Restart:
                    LevelManager.Restart();
                    break;
                case GameState.Exit:
                    break;
            }
            _sceneSwitcher.Update(gameTime);
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointWrap);
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    LevelManager.Draw(spriteBatch);
                    UIManager.Draw(spriteBatch);
                    foreach (var gameObject in GameObjects)
                    {
                        gameObject.Draw(spriteBatch);
                    }
                    break;
                case GameState.SelectCharacter:
                    CharacterSelector.Draw(spriteBatch);
                    break;
                case GameState.Playing:
                   
                    LevelManager.Draw(spriteBatch);
                    UIManager.Draw(spriteBatch);

                    foreach (var gameObject in GameObjects)
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
                            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointWrap);
                        }
                    }
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                    UIManager.Draw(spriteBatch);
                    HighScore.DisplayScores(spriteBatch);
                    break;
                case GameState.Victory:
                    UIManager.Draw(spriteBatch);
                    break;
                case GameState.Restart:
                    break;
                case GameState.Exit:
                    break;
            }
            _sceneSwitcher.Draw(spriteBatch);
            spriteBatch.End();
        }
        //public static void AddGameObject(GameObject gameObject)
        //{
        //    _gameObjects.Add(gameObject);
        //}
        //public static void RemoveGameObject(GameObject gameObject)
        //{
        //    _gameObjects.Remove(gameObject);
        //}

        public static void AddFlashEffect(FlashEffect flashEffect)
        {
            _flashEffects.Add(flashEffect);
        }
        public static void ChangeGameState(object passedState)
        {
            if (passedState is GameState newState)
            {
                if (newState == CurrentGameState) return;
                CurrentGameState = newState;
            }
        }
    }
}
