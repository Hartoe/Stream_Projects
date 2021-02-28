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
    public class TextField : GameObject
    {
        Sprite sprite;
        Text text;
        float width;
        GraphicsHandler graphics;

        public TextField(Vector2 position, Sprite sprite, Text text, float width,
            GraphicsHandler graphics, string id = "") : base (position, id)
        {
            this.sprite = sprite;
            this.text = text;
            this.width = width;
            this.graphics = graphics;
            this.sprite.position = position;
            this.text.position = position;
            Calculate_Text();
        }

        private void Calculate_Text()
        {
            string final = "";
            foreach (string s in text.text.Split(' '))
            {
                if (text.font.MeasureString(final + " " + s).X > width)
                    final += $"\n{s}";
                else
                    final += $" {s}";
            }
            text.text = final;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw()
        {
            graphics.Draw_Sprite(graphics.GUI, sprite);
            graphics.Draw_Text(graphics.GUI, text);
        }
    }
}
