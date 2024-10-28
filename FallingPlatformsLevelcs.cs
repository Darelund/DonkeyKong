using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class FallingPlatformsLevelcs : Level
    {
        private int _startTargets = 0;
        private int _removedTargets;
        private const char _TARGETNAME = 'T';

        private List<Tile> _fallingPlatformsThatWillChangeTexture = new List<Tile>(); // Tiles that will fall
        private List<Tile> _CopyOffallingPlatforms = new List<Tile>(); // Tiles that will fall
        private const char _FALLINGPLATFORMSName = 'F';
        private bool _platformsFalling = false;
        private float falltimer = 0;
        private float fallTime = 7;
        public FallingPlatformsLevelcs()
        {
            _removedTargets = 0;

            TileSteppedOnHandler += HandleTileSteppedOn;
        }
        public override void SetTarget()
        {
            foreach (var tile in _tiles)
            {
                if (tile.Name == _TARGETNAME)
                {
                    _startTargets++;
                    //  _targetPosition = tile.Pos;
                }
                else if(tile.Name == _FALLINGPLATFORMSName)
                {
                    _fallingPlatformsThatWillChangeTexture.Add(tile);
                    _CopyOffallingPlatforms.Add((Tile)tile.Clone());
                }
            }
            Debug.WriteLine($"Platform count: {_fallingPlatformsThatWillChangeTexture.Count}");
        }
        private void HandleTileSteppedOn(Tile tile)
        {
            if (tile.Name == _TARGETNAME)
            {
                _removedTargets++;
                tile.Name = '_';
                tile.SwitchTile(ResourceManager.GetTexture("empty"));
                AudioManager.PlaySoundEffect("HardPop");
            }

            if (_removedTargets >= _startTargets && !_platformsFalling)
            {
                _platformsFalling = true;
                foreach (var fallingPlatform in _fallingPlatformsThatWillChangeTexture)
                {
                    fallingPlatform.Name = '_';
                    fallingPlatform.Type = TileType.Walkable;
                    fallingPlatform.SwitchTile(ResourceManager.GetTexture("empty"));
                }
                foreach (var fallingPlatform in _CopyOffallingPlatforms)
                {
                    fallingPlatform._state = TileState.Dynamic;
                }
            }
        }
        public override bool CheckLevelCompletion()
        {
            return falltimer > fallTime;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_platformsFalling)
            {
                foreach (var tile in _CopyOffallingPlatforms)
                {
                    tile.Draw(spriteBatch);
                }
            }
            base.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
          foreach(var tile in _fallingPlatformsThatWillChangeTexture)
            {
                tile.Update(gameTime);
            }
            if (_platformsFalling)
            {
                foreach (var tile in _CopyOffallingPlatforms)
                {
                    tile.Update(gameTime);
                }
            }
            if (_platformsFalling)
            {
                falltimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
        public override void UnloadLevel()
        {
            LevelCompleted = false;
            base.UnloadLevel();
        }
    }
}
