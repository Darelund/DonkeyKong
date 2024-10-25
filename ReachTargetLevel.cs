using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class ReachTargetLevel : Level
    {
        private Vector2 _targetPosition;
        private const char _TARGETNAME = 'T';
        private float _distanceFromTarget;
        public ReachTargetLevel(float distanceFromTarget = 30)
        {
            _distanceFromTarget = distanceFromTarget;
        }
        public override void SetTarget()
        {
           foreach (var tile in _tiles)
            {
                if (tile.Name == _TARGETNAME)
                {
                    _targetPosition = tile.Pos;
                }
            }
        }
        public override void Update()
        {
            //Debug.WriteLine(Vector2.Distance(PlayerController.Instance.Position, _targetPosition));
            //Debug.WriteLine(PlayerController.Instance.Position);
        }
        public override bool CheckLevelCompletion()
        {
            return Vector2.Distance(PlayerController.Instance.Position, _targetPosition) < _distanceFromTarget;
        }
        public override void UnloadLevel()
        {
            LevelCompleted = false;
            base.UnloadLevel();
        }
    }
}
