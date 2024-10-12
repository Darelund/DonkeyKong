using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class FlashEffect
    {
        private Effect _flashEffect;
        private SpriteBatch _spriteBatch;

        private float _flashTimer;
        private float _flashTime;

        private GameObject _flashGameObject;
        private Color _color;

        private bool _isActive = true;

        public FlashEffect(SpriteBatch spriteBatch, float flashTime, GameObject flashGameObject, Color color)
        {
            _flashEffect = ResourceManager.GetEffect("Effect/FlashEffect");
            _spriteBatch = spriteBatch;
            _flashTime = flashTime;
            _flashGameObject = flashGameObject;
            _color = color;
        }

        public void Update(GameTime gameTime)
        {
            if (!_isActive) return;
            _flashTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(_flashTimer < _flashTime)
            {
                SwitchDrawEffect();
            }
            else if(_flashTimer >= _flashTime)
            {
                _isActive = false;
            }
        }

        /// <summary>
        /// Switches the current SpriteBatch.Draw method to one that draws the flash effect
        /// </summary>
        /// <param name="drawPos"></param>
        /// <param name="colorOverlay"></param>
        public void SwitchDrawEffect()
        {
            // switch spritebatch to overlayColor shader
            _flashEffect.Parameters["overlayColor"].SetValue(_color.ToVector4());
            RestartSpriteBatch(_flashEffect);

            // _spriteBatch.Draw(ResourceManager.GetTexture("SuperMarioFront"), drawPos, Color.White);
            _flashGameObject.Draw(_spriteBatch);

            // switch spritebatch off shader
            RestartSpriteBatch();
        }

        private void RestartSpriteBatch(Effect effect = null)
        {
            if (GameManager.SpriteBatchActive)
                EndSpriteBatch();

            _spriteBatch.Begin(effect: effect);
            GameManager.SpriteBatchActive = true;
        }


        private void EndSpriteBatch()
        {
            _spriteBatch.End();
            GameManager.SpriteBatchActive = false;
        }
    }
}
