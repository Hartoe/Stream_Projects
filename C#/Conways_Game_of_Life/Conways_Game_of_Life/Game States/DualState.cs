using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conways_Game_of_Life
{
    class DualState : IGameState
    {
        public List<GameObject> Objects { get; set; }
        public GameWorld world { get; set; }

        GameStateManager gameStateManager;
        Point size;
        int cell_width;

        public DualState(GameWorld world, GameStateManager gameStateManager, Point size, int cell_width)
        {
            this.world = world;
            this.gameStateManager = gameStateManager;
            this.size = size;
            this.cell_width = cell_width;
            Set_Items(size, cell_width);
        }

        public void Update(GameTime gameTime)
        {
            if (InputHandler.Key_Pressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                gameStateManager.Set_State("menu");
            world.Update(gameTime);
        }

        public void Draw()
        {
            world.Draw();
        }

        public void Reset()
        {
            Camera.Reset();
            world.Clear_Objects();
            Set_Items(size, cell_width);
            foreach (GameObject obj in Objects)
                world.Add_Object(obj);
        }

        public void Set_Items(Point size, int cell_width)
        {
            Objects = new List<GameObject>();

            Objects.Add(new Grid(Vector2.Zero, size.X, size.Y, cell_width, false));
        }
    }
}
