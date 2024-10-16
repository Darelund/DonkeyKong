using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class FlashEffect
    {
        public event Action<bool> OnFlashing;
        private Effect _flashEffect;

        private float _flashTimer;
        private float _flashTime;

        private GameObject _flashGameObject;
        private Color _color;

        private float _blinkFrequency;
        private bool _isFlashing;
        public bool IsActive { get; private set; }

        public FlashEffect(Effect flashEffect, float flashTime, GameObject flashGameObject, Color color, float blinkFrequency = 0.2f)
        {
            _flashEffect = flashEffect;
            _flashTime = flashTime;
            _flashGameObject = flashGameObject;
            _color = color;
            _blinkFrequency = blinkFrequency;

            IsActive = true;
            _isFlashing = true;

            OnFlashing?.Invoke(true);
        }

        public void Update(GameTime gameTime)
        {
            if (!IsActive) return;
            _flashTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(_flashTimer >= _flashTime)
            {
                IsActive = false;
                OnFlashing?.Invoke(false);
                return;
            }
            //TODO Can probably take away 2, maybe
            _isFlashing = (_flashTimer % _blinkFrequency < _blinkFrequency / 2);
        }
        public bool IsActiveOnObject(GameObject gameObject)
        {
            return _flashGameObject == gameObject && IsActive;
        }
        public void ApplyDrawEffect(SpriteBatch spriteBatch)
        {
            if (_isFlashing)
            {
                _flashEffect.Parameters["overlayColor"].SetValue(_color.ToVector4());
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.BackToFront, effect: _flashEffect, blendState: BlendState.AlphaBlend);
            }
        }
    }
}
