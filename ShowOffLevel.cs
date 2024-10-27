using Microsoft.Xna.Framework;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class ShowOffLevel : Level
    {
       
        public ShowOffLevel()
        {
        }
        public override void SetTarget()
        {
           
        }
        public override void Update(GameTime gameTime)
        {
            //debug.writeline(vector2.distance(playercontroller.instance.position, _targetposition));
            //debug.writeline(playercontroller.instance.position);
        }
        public override bool CheckLevelCompletion()
        {
            return false;
        }
        //public override void UnloadLevel()
        //{
        //    LevelCompleted = false;
        //    base.UnloadLevel();
        //}
    }
}
