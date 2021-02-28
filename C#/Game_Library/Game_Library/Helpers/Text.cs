using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Helpers
{
    public class Text : RenderObject
    {
        public SpriteFont font;
        public string text;
        public Vector2 position;
        public Color color;
        public float rotation;
        public Vector2 origin;
        public Vector2 scale;
        public SpriteEffects effect;
        public float layerDepth;

        public Text(SpriteFont font, string text, Vector2? position = null, Color? color = null,
            float rotation = 0, Vector2? origin = null, Vector2? scale = null, SpriteEffects effect = SpriteEffects.None,
            float layerDepth = 1)
        {
            this.font = font;
            this.text = text;
            this.rotation = rotation;
            this.effect = effect;
            this.layerDepth = layerDepth;

            this.position = Vector2.Zero;
            if (position != null) { this.position = (Vector2)position; }
            this.color = Color.Black;
            if (color != null) { this.color = (Color)color; }
            this.origin = Vector2.Zero;
            if (origin != null) { this.origin = (Vector2)origin; }
            this.scale = Vector2.One;
            if (scale != null) { this.scale = (Vector2)scale; }
        }
    }
}
