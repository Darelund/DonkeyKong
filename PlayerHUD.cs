using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DonkeyKong
{
    public class PlayerHUD
    {
        private UIText _LifeText;
        private UIImage _lifeImage;
        private int hearts;

        public UIText _scoreText;


        public PlayerHUD()
        {
            _LifeText = new UIText(ResourceManager.GetSpriteFont("GameText"), "Lifes: ", new Vector2(50, 20), Color.White, 0.8f, Vector2.Zero, 0.9f);
            _lifeImage = new UIImage(ResourceManager.GetTexture("stuff_mod_transparent"), new Vector2(80, 0), Color.White, 3, Vector2.Zero, new Rectangle(0, 200, 30, 30), 0.9f);
          //  hearts = (int)PlayerController.Instance.Health;
            ScoreManager.OnScoreChanged += OnScoreChanged;

            _scoreText = new UIText(ResourceManager.GetSpriteFont("GameText"), $"Points: {ScoreManager.PlayerScore}", new Vector2(350, 20), Color.White, 0.8f, Vector2.Zero, 0.9f);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _LifeText.Draw(spriteBatch);
            for (int i = 0; i < hearts; i++)
            {
                Vector2 heartPosition = _lifeImage.Position + new Vector2((i * 50), 0);
                _lifeImage.Draw(spriteBatch, heartPosition);
            }
            _scoreText.Draw(spriteBatch);
        }
        public void Update(GameTime gameTime)
        {
            hearts = (int)PlayerController.Instance.Health;
        }
        private void OnScoreChanged()
        {
            _scoreText.UpdateText($"Points: {ScoreManager.PlayerScore}");
        }
    }
}
