using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conways_Game_of_Life
{
    static class Camera
    {
        public static Vector2 Position;
        public static Vector2 Bound;
        public static Viewport View;
        public static float Zoom { get; set; } = 0.5f;

        public static void Init(Viewport view, Point size, int cell_width)
        {
            Position = Vector2.Zero;
            Bound = new Vector2(size.X * cell_width, size.Y * cell_width);
            View = view;
        }

        public static void Update()
        {
            if (InputHandler.Scroll_Down())
            {
                Zoom -= 0.1f;
                if (Zoom < 0.2f)
                    Zoom = 0.2f;
            }
            if (InputHandler.Scroll_Up())
            {
                Zoom += 0.1f;
                if (Zoom > 6)
                    Zoom = 6;
            }

            int speed = 5;

            if (InputHandler.Key_Down(Microsoft.Xna.Framework.Input.Keys.Left))
                Position.X -= speed / Zoom;
            if (InputHandler.Key_Down(Microsoft.Xna.Framework.Input.Keys.Right))
                Position.X += speed / Zoom;
            if (InputHandler.Key_Down(Microsoft.Xna.Framework.Input.Keys.Down))
                Position.Y += speed / Zoom;
            if (InputHandler.Key_Down(Microsoft.Xna.Framework.Input.Keys.Up))
                Position.Y -= speed / Zoom;

            if (Position.X < 0)
                Position.X = 0;
            if (Position.X > Bound.X - View.Width / Zoom)
                Position.X = Bound.X - View.Width / Zoom;
            if (Position.Y < 0)
                Position.Y = 0;
            if (Position.Y > Bound.Y - View.Height / Zoom)
                Position.Y = Bound.Y - View.Height / Zoom;
        }

        public static Matrix Get_Transform()
        {
            return Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 1)) * Matrix.CreateScale(Zoom, Zoom, 1);
        }
    }
}
