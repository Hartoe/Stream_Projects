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
        public bool red, classic;
        bool destroy, create, mouse_in;
        Texture2D current_sprite, dead_sprite, alive_sprite, red_sprite, blue_sprite;

        public Cell(Vector2 position, bool classic) : base(position)
        {
            dead_sprite = GraphicsHandler.Load_Sprite("Sprites/Cell/cell_dead");
            alive_sprite = GraphicsHandler.Load_Sprite("Sprites/Cell/cell_alive");
            red_sprite = GraphicsHandler.Load_Sprite("Sprites/Cell/cell_red");
            blue_sprite = GraphicsHandler.Load_Sprite("Sprites/Cell/cell_blue");
            current_sprite = dead_sprite;
            alive = destroy = create = false;
            mouse_in = false;
            red = true;
            this.classic = classic;
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
                if (classic)
                {
                    Classic_Placement();
                }
                else
                {
                    Dual_Placement();
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
            {
                if (classic)
                {
                    current_sprite = alive_sprite;
                }
                else
                {
                    if (red)
                        current_sprite = red_sprite;
                    else
                        current_sprite = blue_sprite;
                }
            }
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

        private void Classic_Placement()
        {
            if (Mouse_In_Cell() && !mouse_in)
            {
                if (InputHandler.Left_Mouse_Down())
                {
                    alive = true;
                    destroy = false;
                    create = false;
                    mouse_in = true;
                }
                if (InputHandler.Right_Mouse_Down())
                {
                    alive = false;
                    destroy = false;
                    create = false;
                    mouse_in = true;
                }
            }

            if (!Mouse_In_Cell())
                mouse_in = false;
        }

        private void Dual_Placement()
        {
            if (Mouse_In_Cell() && !mouse_in)
            {
                if (InputHandler.Left_Mouse_Down())
                {
                    if (InputHandler.Key_Down(Microsoft.Xna.Framework.Input.Keys.LeftShift))
                        red = true;
                    else
                        red = false;

                    alive = true;
                    destroy = false;
                    create = false;
                    mouse_in = true;
                }
                if (InputHandler.Right_Mouse_Down())
                {
                    alive = false;
                    destroy = false;
                    create = false;
                    mouse_in = true;
                }
            }

            if (!Mouse_In_Cell())
                mouse_in = false;
        }
    }
}
