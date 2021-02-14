using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Helpers
{
    public class Timer
    {
        public bool Triggered { get; private set; }

        float current_time, duration;
        bool repeat;

        public Timer(float duration, bool repeat)
        {
            Triggered = false;
            this.repeat = repeat;
            this.duration = duration;
            current_time = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (current_time == 0)
                Triggered = false;

            current_time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (current_time >= duration)
            {
                Triggered = true;
                if (repeat)
                    current_time = 0;
            }
        }
    }
}
