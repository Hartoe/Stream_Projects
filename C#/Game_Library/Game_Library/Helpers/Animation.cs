using Game_Library.Handlers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Helpers
{
    public class Animation
    {
        public Sprite sprite;
        public bool Finished { get; private set; }

        GraphicsHandler graphics;
        Point frame_size;
        Timer timer;
        float width, height;
        bool loop;
        int x_frame, y_frame;

        public Animation(Sprite sprite, Point frame_size, GraphicsHandler graphics, float speed = 0.2f, bool loop = false)
        {
            Finished = false;
            this.graphics = graphics;
            this.sprite = sprite;
            this.loop = loop;
            timer = new Timer(speed, true);
            this.frame_size = frame_size;
            width = sprite.sprite.Width / frame_size.X;
            height = sprite.sprite.Height / frame_size.Y;
            sprite.source = new Rectangle(0, 0, (int)width, (int)height);
            x_frame = y_frame = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (loop || !Finished)
            {
                timer.Update(gameTime);
                if (timer.Triggered)
                {
                    if (x_frame + 1 == frame_size.X)
                    {
                        if (y_frame + 1 == frame_size.Y)
                        {
                            if (!loop)
                                Finished = true;
                        }
                        y_frame = (y_frame + 1) % frame_size.Y;
                    }
                    x_frame = (x_frame + 1) % frame_size.X;

                    sprite.source = new Rectangle(x_frame * (int)width, y_frame * (int)height, (int)width, (int)height);
                }
            }
        }

        public void Draw(SpriteBatch layer)
        {
            graphics.Draw_Sprite(layer, sprite);
        }
    }
}
