using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Conways_Game_of_Life
{
    class Cell : GameObject
    {
        public bool alive { get; private set; }
        bool destroy, create;
        Texture2D current_sprite, dead_sprite, alive_sprite;

        public Cell(Vector2 position) : base(position)
        {
            dead_sprite = GraphicsHandler.Load_Sprite("Sprites/Cell/cell_dead");
            alive_sprite = GraphicsHandler.Load_Sprite("Sprites/Cell/cell_alive");
            current_sprite = dead_sprite;
            alive = destroy = create = false;
        }

        public void Destroy_Cell()
        {
            destroy = true;
        }

        public void Create_Cell()
        {
            create = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (GameWorld.Paused)
            {
                if (InputHandler.Left_Mouse_Clicked() && Mouse_In_Cell())
                {
                    alive = !alive;
                    destroy = false;
                    create = false;
                }
            }

            if (destroy)
            {
                alive = false;
                destroy = false;
            }
            if (create)
            {
                alive = true;
                create = false;
            }

            if (alive)
                current_sprite = alive_sprite;
            else
                current_sprite = dead_sprite;
        }

        public override void Draw()
        {
            GraphicsHandler.Draw_Sprite(GraphicsHandler.GameSprites, current_sprite, position);
        }

        private bool Mouse_In_Cell()
        {
            Vector2 mouse_pos = InputHandler.Mouse_Position.ToVector2();
            Vector2 scale_pos = Vector2.Transform(position, Camera.Get_Transform());
            return (mouse_pos.X > scale_pos.X
                && mouse_pos.Y > scale_pos.Y
                && mouse_pos.X < scale_pos.X + current_sprite.Width * Camera.Zoom
                && mouse_pos.Y < scale_pos.Y + current_sprite.Height * Camera.Zoom);
        }
    }
}
