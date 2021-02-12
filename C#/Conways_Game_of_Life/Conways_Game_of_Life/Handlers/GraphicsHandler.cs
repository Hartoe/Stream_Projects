using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conways_Game_of_Life
{
    static class GraphicsHandler
    {
        public static SpriteBatch GameSprites;
        public static SpriteBatch GUI;

        static GraphicsDeviceManager Graphics;
        static GraphicsDevice GraphicsDevice;
        static ContentManager Content;

        public static void Init(GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice, ContentManager content)
        {
            Graphics = graphics;
            GraphicsDevice = graphicsDevice;
            Content = content;
        }

        public static Texture2D Load_Sprite(string path)
        {
            return Content.Load<Texture2D>(path);
        }

        public static SpriteFont Load_Font(string path)
        {
            return Content.Load<SpriteFont>(path);
        }

        public static void Set_Sprite_Batch()
        {
            GameSprites = new SpriteBatch(GraphicsDevice);
            GUI = new SpriteBatch(GraphicsDevice);
        }

        public static void Begin()
        {
            GameSprites.Begin(transformMatrix: Camera.Get_Transform());
            GUI.Begin();
        }

        public static void End()
        {
            GameSprites.End();
            GUI.End();
        }

        public static void Draw_Sprite(SpriteBatch layer, Texture2D sprite, Vector2 position, Vector2? scale = null, Color? color = null)
        {
            Vector2 real_scale;
            Color real_color;
            if (scale != null)
                real_scale = (Vector2)scale;
            else
                real_scale = Vector2.One;
            if (color != null)
                real_color = (Color)color;
            else
                real_color = Color.White;
            layer.Draw(sprite, position, new Rectangle(0,0,sprite.Width,sprite.Height), real_color, 0f, Vector2.Zero, real_scale, SpriteEffects.None, 0);
        }

        public static void Draw_String(SpriteBatch layer, SpriteFont font, Vector2 position, string text, Color color)
        {
            layer.DrawString(font, text, position, color);
        }
    }
}
