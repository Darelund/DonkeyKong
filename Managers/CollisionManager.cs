using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public static class CollisionManager
    {
        public static List<GameObject> _collidables => GameManager.GetGameObjects;
        public static void CheckCollision()
        {
            for (int i = 0; i < _collidables.Count; i++)
            {
                for (int j = i + 1; j < _collidables.Count; j++)
                {
                    if (_collidables[i].Collision.Intersects(_collidables[j].Collision))
                    {
                        _collidables[i].OnCollision(_collidables[j]);
                    }
                }
            }
            
        }
    }
}
