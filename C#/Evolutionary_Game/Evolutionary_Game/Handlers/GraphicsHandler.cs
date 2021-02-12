using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolutionary_Game
{
    class GraphicsHandler
    {
        public static SpriteBatch Background { get; private set; }
        public static SpriteBatch Foreground { get; private set; }
        public static SpriteBatch GUI { get; private set; }

        ContentManager Content;

        public GraphicsHandler(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            this.Content = Content;

            Background = new SpriteBatch(GraphicsDevice);
            Foreground = new SpriteBatch(GraphicsDevice);
            GUI = new SpriteBatch(GraphicsDevice);
        }

        public Texture2D Load_Sprite(string path)
        {
            return Content.Load<Texture2D>(path);
        }

        public void Begin()
        {
            Background.Begin();
            Foreground.Begin();
            GUI.Begin();
        }

        public void End()
        {
            Background.End();
            Foreground.End();
            GUI.End();
        }

        public void Draw_Sprite(SpriteBatch layer, Texture2D sprite, Vector2 position,
            Color? color = null, Rectangle? source = null, float rotation = 0, Vector2? origin = null,
            Vector2? scale = null)
        {
            Color real_color = Color.White;
            if (color != null) { real_color = (Color)color; }
            Rectangle real_source = new Rectangle(0, 0, sprite.Width, sprite.Height);
            if (source != null) { real_source = (Rectangle)source; }
            Vector2 real_origin = Vector2.Zero;
            if (origin != null) { real_origin = (Vector2)origin; }
            Vector2 real_scale = Vector2.One;
            if (scale != null) { real_scale = (Vector2)scale; }

#pragma warning disable CS0618 // Type or member is obsolete
            layer.Draw(sprite, position: position, sourceRectangle: real_source, color: real_color,
                rotation: rotation, origin: real_origin, scale: real_scale);
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
