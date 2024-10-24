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
        private int _startTargets = 10;
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
                tile.SwitchTile(ResourceManager.GetTexture("empty"));
            }
        }
        public override bool CheckLevelCompletion()
        {
            return _removedTargets >= _startTargets;
        }

        public override void Update()
        {
          //  throw new NotImplementedException();
        }
    }
}
