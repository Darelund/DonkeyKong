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
    public class ScoreManager
    {
        private static int _playerScore = 0;
        public static UIText text = new UIText(ResourceManager.GetSpriteFont("GameText"), $"Points: {_playerScore:000.0000}", new Vector2(350, 0), Color.White, 0.8f, Vector2.Zero, 0.9f);
        public static void UpdateScore(int points)
        {
            _playerScore += points;
            Debug.WriteLine(points);
            text.UpdateText($"Points: {_playerScore}");
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            text.Draw(spriteBatch);
        }
        public void Update(GameTime gameTime)
        {
        }
    }
}
