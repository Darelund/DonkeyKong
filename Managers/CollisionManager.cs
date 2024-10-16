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
        private static List<GameObject> _collisionObjects = new List<GameObject>();
        public static void CheckCollision(GameObject obj)
        {
            foreach (GameObject collisionObj in _collisionObjects)
            {
                if(collisionObj != obj)
                {
                    //Really dirty code here, just testing
                    var enemy = collisionObj as EnemyController;
                    if(enemy != null)
                    {
                        var player = obj as PlayerController;
                        if (enemy.HasCollided(player.Collision))
                        {
                            if(!player.IsImmune)
                            {
                                var flash = new FlashEffect(ResourceManager.GetEffect("FlashEffect"), 1f, player, Color.White);
                                GameManager.AddFlashEffect(flash);
                                //YEs memory leak, so what???
                                flash.OnFlashing += player.ImmuneHandler;
                                player.TakeDamage(1);
                            }
                            break;
                        }
                    }
                }
            }
        }
        public static void AddCollisionObject(GameObject obj)
        {
            _collisionObjects.Add(obj);
        }
    }
}
