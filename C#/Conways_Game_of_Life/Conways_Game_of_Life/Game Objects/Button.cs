using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Conways_Game_of_Life
{
    class Button : GameObject
    {
        public bool activated;

        string text;
        Texture2D current_sprite, idle_sprite, clicked_sprite;
        SpriteFont font;

        public Button(Vector2 position, string text) : base(position)
        {
            this.text = text;
            activated = false;

            idle_sprite = GraphicsHandler.Load_Sprite("Sprites/Button/button_idle");
            clicked_sprite = GraphicsHandler.Load_Sprite("Sprites/Button/button_clicked");
            current_sprite = idle_sprite;

            font = GraphicsHandler.Load_Font("Fonts/Button/default");
        }

        public override void Update(GameTime gameTime)
        {
            if (Mouse_On_Button() && InputHandler.Left_Mouse_Down())
            {
                activated = true;
                current_sprite = clicked_sprite;
            }
            else
            {
                activated = false;
                current_sprite = idle_sprite;
            }
        }

        public override void Draw()
        {
            GraphicsHandler.Draw_Sprite(GraphicsHandler.GUI, current_sprite, position);
            Vector2 text_pos = (position + new Vector2(current_sprite.Width, current_sprite.Height)/2) - font.MeasureString(text)/2;

            GraphicsHandler.Draw_String(GraphicsHandler.GUI, font, text_pos, text, Color.Black);
        }

        private bool Mouse_On_Button()
            => InputHandler.Mouse_Position.X > position.X
            && InputHandler.Mouse_Position.X < position.X + current_sprite.Width
            && InputHandler.Mouse_Position.Y > position.Y
            && InputHandler.Mouse_Position.Y < position.Y + current_sprite.Height;
    }
}
