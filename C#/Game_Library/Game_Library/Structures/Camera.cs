using Game_Library.Game_Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Structures
{
    public class Camera
    {
        public Vector2 position;
        public float zoom;
        public float rotation;
        public GameObject focus;
        public Point bounds;

        bool bounded;

        public Camera(Vector2 position, bool bounded = false, GameObject focus = null)
        {
            this.position = position;
            this.focus = focus;
            this.bounded = bounded;
            this.zoom = 1;
            this.rotation = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (focus != null)
                position = focus.position;

            if (bounded)
                Check_Bounds();
        }

        public void Check_Bounds()
        {
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > bounds.X)
                position.X = bounds.X;
            if (position.Y > bounds.Y)
                position.Y = bounds.Y;
        }

        public Matrix Get_Transform()
            => Matrix.CreateTranslation(-position.X, -position.Y, 1)
             * Matrix.CreateRotationZ(rotation)
             * Matrix.CreateScale(zoom, zoom, 1);

        public Matrix Get_Background_Transform()
            => Get_Transform();
    }
}
