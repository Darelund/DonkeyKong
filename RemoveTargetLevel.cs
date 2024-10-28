using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class RemoveTargetLevel : Level
    {
        private int _startTargets = 0;
        private int _removedTargets;
        private char _TARGETNAME = 'T';

        public RemoveTargetLevel()
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
            }
        }
        private void HandleTileSteppedOn(Tile tile)
        {
            if(tile.Name == _TARGETNAME)
            {
                _removedTargets++;
                tile.Name = '_';
                tile.SwitchTile(ResourceManager.GetTexture("empty"));
                AudioManager.PlaySoundEffect("HardPop");
            }
        }
        public override bool CheckLevelCompletion()
        {
            return _removedTargets >= _startTargets;
        }

        public override void Update(GameTime gameTime)
        {
          //  throw new NotImplementedException();
        }
        public override void UnloadLevel()
        {
            LevelCompleted = false;
            base.UnloadLevel();
        }
    }
}
