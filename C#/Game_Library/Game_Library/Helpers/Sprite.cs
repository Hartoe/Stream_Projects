using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Helpers
{
    public class Sprite
    {
        public Texture2D sprite;
        public Vector2 position, origin, scale;
        public Rectangle source;
        public Color color;
        public float rotation, layerDepth;
        public SpriteEffects effect;

        public Sprite(Texture2D sprite, Vector2? position = null, Color? color = null, Rectangle? source = null,
            float rotation = 0, Vector2? origin = null, Vector2? scale = null, SpriteEffects effect = SpriteEffects.None,
            float layerDepth = 1)
        {
            this.sprite = sprite;
            this.rotation = rotation;
            this.effect = effect;
            this.layerDepth = layerDepth;

            this.position = Vector2.Zero;
            if (position != null) { this.position = (Vector2)position; }
            this.color = Color.White;
            if (color != null) { this.color = (Color)color; }
            this.source = new Rectangle(0, 0, sprite.Width, sprite.Height);
            if (source != null) { this.source = (Rectangle)source; }
            this.origin = Vector2.Zero;
            if (origin != null) { this.origin = (Vector2)origin; }
            this.scale = Vector2.One;
            if (scale != null) { this.scale = (Vector2)scale; }
        }
    }
}
