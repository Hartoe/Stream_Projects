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

        public static void Draw_Sprite(SpriteBatch layer, Texture2D sprite, Vector2 position)
        {
            layer.Draw(sprite, position, Color.White);
        }
    }
}
