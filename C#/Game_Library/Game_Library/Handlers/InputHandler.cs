using Game_Library.Exceptions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Handlers
{
    public enum MouseButton
    {
        Left,
        Right,
        Middle
    }

    public class InputHandler
    {
        public Point Mouse_Position => current_mouse_state.Position;
        public float Scroll_Value => current_mouse_state.ScrollWheelValue;

        public KeyboardState current_keyboard_state, previous_keyboard_state;
        public MouseState current_mouse_state, previous_mouse_state;

        public InputHandler()
        {
            current_keyboard_state = previous_keyboard_state = Keyboard.GetState();
            current_mouse_state = previous_mouse_state = Mouse.GetState();
        }

        public void Update(GameTime gameTime)
        {
            previous_keyboard_state = current_keyboard_state;
            current_keyboard_state = Keyboard.GetState();

            previous_mouse_state = current_mouse_state;
            current_mouse_state = Mouse.GetState();
        }

        #region Keyboard Input

        public bool Key_Pressed(Keys key)
            => current_keyboard_state.IsKeyDown(key) && previous_keyboard_state.IsKeyUp(key);
        public bool Key_Released(Keys key)
            => current_keyboard_state.IsKeyUp(key) && previous_keyboard_state.IsKeyDown(key);
        public bool Key_Hold(Keys key)
            => current_keyboard_state.IsKeyDown(key);
        public bool Key_Up(Keys key)
            => current_keyboard_state.IsKeyUp(key);

        public Keys[] Get_Pressed_Keys()
            => current_keyboard_state.GetPressedKeys().Where(k => Key_Pressed(k)).ToArray();

        #endregion

        #region Mouse Input

        public bool Mouse_Pressed(MouseButton button)
        {
            switch(button)
            {
                case MouseButton.Left:
                    return current_mouse_state.LeftButton == ButtonState.Pressed
                        && previous_mouse_state.LeftButton == ButtonState.Released;
                case MouseButton.Right:
                    return current_mouse_state.RightButton == ButtonState.Pressed
                        && previous_mouse_state.RightButton == ButtonState.Released;
                case MouseButton.Middle:
                    return current_mouse_state.MiddleButton == ButtonState.Pressed
                        && previous_mouse_state.MiddleButton == ButtonState.Released;
                default:
                    throw new InvalidMouseButtonException(button.ToString());
            }
        }
        public bool Mouse_Released(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return current_mouse_state.LeftButton == ButtonState.Released
                        && previous_mouse_state.LeftButton == ButtonState.Pressed;
                case MouseButton.Right:
                    return current_mouse_state.RightButton == ButtonState.Released
                        && previous_mouse_state.RightButton == ButtonState.Pressed;
                case MouseButton.Middle:
                    return current_mouse_state.MiddleButton == ButtonState.Released
                        && previous_mouse_state.MiddleButton == ButtonState.Pressed;
                default:
                    throw new InvalidMouseButtonException(button.ToString());
            }
        }
        public bool Mouse_Hold(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return current_mouse_state.LeftButton == ButtonState.Pressed;
                case MouseButton.Right:
                    return current_mouse_state.RightButton == ButtonState.Pressed;
                case MouseButton.Middle:
                    return current_mouse_state.MiddleButton == ButtonState.Pressed;
                default:
                    throw new InvalidMouseButtonException(button.ToString());
            }
        }
        public bool Mouse_Up(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return current_mouse_state.LeftButton == ButtonState.Released;
                case MouseButton.Right:
                    return current_mouse_state.RightButton == ButtonState.Released;
                case MouseButton.Middle:
                    return current_mouse_state.MiddleButton == ButtonState.Released;
                default:
                    throw new InvalidMouseButtonException(button.ToString());
            }
        }

        public bool Scroll_Up()
            => current_mouse_state.ScrollWheelValue > previous_mouse_state.ScrollWheelValue;
        public bool Scroll_Down()
            => current_mouse_state.ScrollWheelValue < previous_mouse_state.ScrollWheelValue;

        #endregion
    }
}
