using Game_Library.Handlers;
using Game_Library.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Game_Objects.GUI
{
    public class EntryField : GameObject
    {
        Sprite sprite;
        Text text;
        GraphicsHandler graphics;
        InputHandler input;
        int max;
        Keys[] pressed_keys;
        string characters;
        bool capital;

        public EntryField(Vector2 position, Sprite sprite, Text text,
            GraphicsHandler graphics, InputHandler input, int max = 0, string id = "") : base(position, id)
        {
            this.sprite = sprite;
            this.text = text;
            this.graphics = graphics;
            this.input = input;
            this.max = max;
            capital = false;

            sprite.position = position;
            text.position = position;

            characters = "abcdefghijklmnopqrstuvwxyz1234567890~!@#$%^&*()_+=-[]{}:;\"'\\/?.>,<";
        }

        public override void Update(GameTime gameTime)
        {
            pressed_keys = input.current_keyboard_state.GetPressedKeys().Where(x => input.Key_Pressed(x)).ToArray();

            foreach (Keys k in pressed_keys)
            {
                if (k == Keys.Back)
                {
                    if (text.text.Length - 1 >= 0)
                        text.text = text.text.Substring(0, text.text.Length - 1);
                }
                else if (characters.Contains(k.ToString().ToLower()))
                {
                    if (pressed_keys.Contains(Keys.LeftShift) || pressed_keys.Contains(Keys.RightShift))
                        capital = true;
                    else
                        capital = false;

                    if (capital)
                        text.text += k.ToString().ToUpper();
                    else
                        text.text += k.ToString().ToLower();

                    if (max != 0)
                        text.text = text.text.Substring(0, max);
                }
                
            }
        }

        public string Get_Entry()
        {
            return text.text;
        }

        public override void Draw()
        {
            graphics.Draw_Sprite(graphics.GUI, sprite);
            graphics.Draw_Text(graphics.GUI, text);
        }
    }
}
