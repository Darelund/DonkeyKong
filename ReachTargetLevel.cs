using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class ReachTargetLevel : Level
    {
        private Vector2 _targetPosition;
        private char _targetName;
        private float _distanceFromTarget;
        public ReachTargetLevel(char name, float distanceFromTarget = 30)
        {
            _targetName = name;
            _distanceFromTarget = distanceFromTarget;
        }
        public void SetTarget()
        {
           foreach (var tile in _tiles)
            {
                if (tile.Name == _targetName)
                {
                    _targetPosition = tile.Pos;
                }
            }
        }
        public override bool CheckLevelCompletion()
        {
            return Vector2.Distance(PlayerController.Instance.Position, _targetPosition) < _distanceFromTarget;
        }
    }
}
