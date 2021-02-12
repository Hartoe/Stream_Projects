using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conways_Game_of_Life
{
    class Background : GameObject
    {
        Texture2D sprite;

        public Background(string path) : base(Vector2.Zero)
        {
            sprite = GraphicsHandler.Load_Sprite(path);
        }

        public override void Draw()
        {
            GraphicsHandler.Draw_Sprite(GraphicsHandler.GameSprites, sprite, position);
        }
    }
}
