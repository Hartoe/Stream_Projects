using Game_Library.Handlers;
using Game_Library.Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Game_Objects.GUI
{
    public class Button : GameObject
    {
        public bool Activated { get; private set; }

        Sprite current_sprite;
        Sprite sprite, hover, click;
        Text text;
        GraphicsHandler graphics;
        InputHandler input;
        Rectangle bounds;

        public Button(Vector2 position, Sprite sprite, Sprite hover, Sprite click, Text text, InputHandler input,
            GraphicsHandler graphics, string id = "") : base(position, id)
        {
            Activated = false;
            this.graphics = graphics;
            this.sprite = sprite;
            this.hover = hover;
            this.click = click;
            current_sprite = sprite;
            bounds = new Rectangle((int)position.X, (int)position.Y, current_sprite.sprite.Width, current_sprite.sprite.Height);
            this.text = text;
            current_sprite.position = position;
            sprite.position = position;
            hover.position = position;
            click.position = position;
            text.position = position;
        }

        public override void Update(GameTime gameTime)
        {
            bounds = new Rectangle((int)position.X, (int)position.Y, current_sprite.sprite.Width, current_sprite.sprite.Height);
            current_sprite.position = position;
            sprite.position = position;
            hover.position = position;
            click.position = position;
            text.position = position;
            current_sprite = sprite;
            if (bounds.Contains(input.Mouse_Position))
            {
                current_sprite = hover;
                Activated = false;
                if (input.Mouse_Hold(MouseButton.Left))
                {
                    current_sprite = click;
                    Activated = true;
                }
            }
        }

        public override void Draw()
        {
            graphics.Draw_Sprite(graphics.GUI, current_sprite);
            graphics.Draw_Text(graphics.GUI, text);
        }
    }
}
