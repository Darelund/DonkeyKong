using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class AnimationClip
    {
        enum PlayMode { Play, Pause };
        Rectangle[] srcRects;
        PlayMode playMode = PlayMode.Play;
        float animTime = 0.0f;
        float speed;
        public AnimationClip(Rectangle[] srcRects, float speed)
        {
            this.srcRects = srcRects;
            this.speed = speed;
        }
        public void SetPlay() { playMode = PlayMode.Play; }
        public void SetPause() { playMode = PlayMode.Pause; }
        public void SetSpeed(float speed) { this.speed = speed; }
        public void Update(float dt)
        {
            if (playMode == PlayMode.Pause) return;
            animTime += dt * speed;
        }
        public Rectangle GetCurrentSourceRectangle()
        {
            int rect_index = (int)animTime % srcRects.Length;
            return srcRects[rect_index];
        }
    }
}
