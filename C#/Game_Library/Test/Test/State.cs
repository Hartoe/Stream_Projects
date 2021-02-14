using Game_Library.Handlers;
using Game_Library.Structures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class State : GameState
    {
        GameWorld world;
        GraphicsHandler graphicsHandler;
        ContentManager Content;
        InputHandler input;
        Camera camera;

        public State(GameWorld world, Camera camera, GraphicsHandler graphicsHandler, ContentManager Content, InputHandler input)
        {
            this.world = world;
            this.input = input;
            this.camera = camera;
            this.Content = Content;
            this.graphicsHandler = graphicsHandler;
            Init_Objects();
        }

        public override void Update(GameTime gameTime)
        {
            world.Update(gameTime);
        }

        public override void Draw()
        {
            world.Draw();
        }

        public override void Init_Objects()
        {
            base.Init_Objects();
            
        }

        public override void Reset()
        {
            base.Reset();
            Player p = new Player(Vector2.Zero, Content.Load<Texture2D>("player"), input, graphicsHandler, "player");
            camera.focus = p;
            world.Add_Object(p);
        }
    }
}
