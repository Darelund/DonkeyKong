using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class Button
    {
        //enum States
        //{
        //    Default,
        //    Hover,
        //    Pressed
        //}
        private SpriteFont _font;
        (Color defaultColor, Color hoverColor, Color pressedColor) _colors;
        private Color _currentColor;
        private Vector2 _position;
        private string _text;
        private float _size;
        private float _rotation;

        private bool _hasBeenPressed = false;
        private float _pressedTimer;
        private float _pressedDuration = 0.2f;
        private Vector2 _origin;
        private float _layerDepth = 0;
        int centerOffset = 2;

        public Rectangle Collision
        {
            get
            {
                int centerCollisionOffset = (int)(_font.MeasureString(_text).X * _size) / centerOffset;
                return new Rectangle((int)_position.X - centerCollisionOffset, (int)_position.Y - centerCollisionOffset, (int)(_font.MeasureString(_text).X * _size), (int)(_font.MeasureString(_text).Y * _size));
            }
        }


        public Button(SpriteFont font, (Color defaultColor, Color hoverColor, Color pressedColor) colors, Vector2 pos, string text = "PlaceHolder", float size = 1f, float rotation = 0)
        {
            _font = font;
            _colors = colors;
            _position = pos;
            _text = text;
            _size = size;
            _rotation = 0;
            _currentColor = colors.defaultColor;
            _origin = new Vector2((int)(_font.MeasureString(_text).X * _size) / centerOffset, (int)(_font.MeasureString(_text).Y * _size) / centerOffset);
        }



        public void Update(GameTime gameTime)
        {
            if (_pressedTimer > 0)
            {
                _pressedTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_pressedTimer <= 0)
                {
                    _pressedTimer = 0; 
                    GameManager.ChangeGameState(GameManager.GameState.Playing);
                }
            }
            if (Collision.Intersects(InputManager.MouseOver()))
            {
                _currentColor = _colors.hoverColor;
                if (InputManager.LeftClick() && !_hasBeenPressed)
                {
                    _currentColor = _colors.pressedColor;
                    _pressedTimer = _pressedDuration;
                    _hasBeenPressed = true;

                }
            }
            else
            {
                _currentColor = _colors.defaultColor;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _text, _position, _currentColor, _rotation, _origin, _size, SpriteEffects.None, _layerDepth);
        }
    }
}
