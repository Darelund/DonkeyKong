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
        public static List<GameObject> _collisionObjects;
        public static void CheckCollision(PlayerController player)
        {
            foreach (GameObject collisionObj in _collisionObjects)
            {
                if(collisionObj != player)
                {
                    if (collisionObj.CheckCollision(player))
                    {
                        //Should I make it so that enemies can damage each other?
                        if (!player.IsImmune)
                        {
                            var flash = new FlashEffect(ResourceManager.GetEffect("FlashEffect"), 1f, player, Color.White);
                            GameManager.AddFlashEffect(flash);
                            //YEs memory leak, so what??? Or is it?
                            flash.OnFlashing += player.ImmuneHandler;
                            player.TakeDamage(1);
                        }
                        break;
                    }





                    ////Really dirty code here, just testing
                    //var enemy = collisionObj as EnemyController;
                    //if(enemy != null)
                    //{
                    //    var player = obj as PlayerController;
                    //    if (enemy.HasCollided(player.Collision))
                    //    {
                    //        if(!player.IsImmune)
                    //        {
                    //            var flash = new FlashEffect(ResourceManager.GetEffect("FlashEffect"), 1f, player, Color.White);
                    //            GameManager.AddFlashEffect(flash);
                    //            //YEs memory leak, so what???
                    //            flash.OnFlashing += player.ImmuneHandler;
                    //            player.TakeDamage(1);
                    //        }
                    //        break;
                    //    }
                    //}
                }
            }
        }
        public static void AddCollisionObject(GameObject obj)
        {
            _collisionObjects.Add(obj);
        }
    }
}
