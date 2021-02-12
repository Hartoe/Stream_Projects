using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conways_Game_of_Life
{
    static class InputHandler
    {
        public static Point Mouse_Position => current_mouse_state.Position;
        static MouseState current_mouse_state, previous_mouse_state;
        static KeyboardState current_keyboard_state, previous_keyboard_state;

        public static void Init()
        {
            current_keyboard_state = previous_keyboard_state = Keyboard.GetState();
            current_mouse_state = previous_mouse_state = Mouse.GetState();
        }

        public static void Update()
        {
            previous_mouse_state = current_mouse_state;
            current_mouse_state = Mouse.GetState();

            previous_keyboard_state = current_keyboard_state;
            current_keyboard_state = Keyboard.GetState();
        }

        public static bool Left_Mouse_Clicked()
            => previous_mouse_state.LeftButton == ButtonState.Released
                && current_mouse_state.LeftButton == ButtonState.Pressed;

        public static bool Left_Mouse_Down()
            => current_mouse_state.LeftButton == ButtonState.Pressed;

        public static bool Right_Mouse_Down()
            => current_mouse_state.RightButton == ButtonState.Pressed;

        public static bool Key_Pressed(Keys key)
            => previous_keyboard_state.IsKeyUp(key)
                && current_keyboard_state.IsKeyDown(key);

        public static bool Key_Down(Keys key)
            => current_keyboard_state.IsKeyDown(key);

        public static bool Scroll_Up()
            => current_mouse_state.ScrollWheelValue > previous_mouse_state.ScrollWheelValue;

        public static bool Scroll_Down()
            => current_mouse_state.ScrollWheelValue < previous_mouse_state.ScrollWheelValue;
    }
}
