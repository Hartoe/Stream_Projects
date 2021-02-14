using Game_Library.Game_Objects;
using Game_Library.Handlers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Player : GameObject
    {
        public Texture2D sprite;
        GraphicsHandler graphicsHandler;
        InputHandler input;

        public Player(Vector2 position, Texture2D sprite, InputHandler input, GraphicsHandler graphicsHandler, string id = "") : base(position, id)
        {
            this.sprite = sprite;
            this.graphicsHandler = graphicsHandler;
            this.input = input;
        }

        public override void Update(GameTime gameTime)
        {
            if (input.Key_Hold(Microsoft.Xna.Framework.Input.Keys.Up))
                position.Y -= 5;
            if (input.Key_Hold(Microsoft.Xna.Framework.Input.Keys.Down))
                position.Y += 5;
            if (input.Key_Hold(Microsoft.Xna.Framework.Input.Keys.Left))
                position.X -= 5;
            if (input.Key_Hold(Microsoft.Xna.Framework.Input.Keys.Right))
                position.X += 5;
        }

        public override void Draw()
        {
            graphicsHandler.Get_Group("main").Unbind("player");
            graphicsHandler.Draw_Sprite(graphicsHandler.Get_Group("main"), "player", sprite, position);
        }
    }
}
