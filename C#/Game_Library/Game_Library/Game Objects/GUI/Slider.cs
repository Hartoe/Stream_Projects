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
    public class Slider : GameObject
    {
        public float value { get; private set; }
        Sprite bar, indicator;
        Rectangle bounds;
        GraphicsHandler graphics;
        InputHandler input;

        public Slider(Vector2 position, Sprite bar, Sprite indicator,
            GraphicsHandler graphics, InputHandler input, string id = "") : base(position, id)
        {
            value = 1f;
            this.graphics = graphics;
            this.input = input;
            this.bar = bar;
            this.indicator = indicator;
            this.bar.position = position;
            this.indicator.position = position;
            this.bar.origin = new Vector2(0, bar.sprite.Height/2);
            this.indicator.origin = new Vector2(indicator.sprite.Width / 2, indicator.sprite.Height/2);
            bounds = new Rectangle((int)(this.indicator.position.X - this.indicator.origin.X),
                (int)(this.indicator.position.Y - this.indicator.origin.Y),
                indicator.sprite.Width, indicator.sprite.Height);
        }

        public override void Update(GameTime gameTime)
        {
            bounds = new Rectangle((int)(indicator.position.X - indicator.origin.X),
                (int)(indicator.position.Y - indicator.origin.Y),
                indicator.sprite.Width, indicator.sprite.Height);

            if (bounds.Contains(input.Mouse_Position) && input.Mouse_Hold(MouseButton.Left))
            {
                indicator.position.X = input.Mouse_Position.X;
                if (indicator.position.X < bar.position.X)
                    indicator.position.X = bar.position.X;
                if (indicator.position.X > bar.position.X + bar.sprite.Width)
                    indicator.position.X = bar.position.X + bar.sprite.Width;
            }

            value = (indicator.position.X - bar.position.X) / bar.sprite.Width;
        }

        public override void Draw()
        {
            graphics.Draw_Sprite(graphics.GUI, bar);
            graphics.Draw_Sprite(graphics.GUI, indicator);
        }
    }
}
