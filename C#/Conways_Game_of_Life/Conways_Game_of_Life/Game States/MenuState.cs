using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conways_Game_of_Life
{
    class MenuState : IGameState
    {
        public List<GameObject> Objects { get; set; }
        public GameWorld world { get; set; }

        GameStateManager gameStateManager;
        Button exit, play, play2;

        public MenuState(GameWorld world, GameStateManager gameStateManager)
        {
            this.world = world;
            this.gameStateManager = gameStateManager;
            play = new Button(new Vector2(280, 50), "Classic");
            play2 = new Button(new Vector2(280, 200), "Dual");
            exit = new Button(new Vector2(280, 350), "Exit");

            Set_Items();
        }

        public void Update(GameTime gameTime)
        {
            world.Update(gameTime);
            if (exit.activated)
                Environment.Exit(0);
            if (play.activated)
                gameStateManager.Set_State("classic");
            if (play2.activated)
                gameStateManager.Set_State("dual");
        }

        public void Draw()
        {
            world.Draw();
        }

        public void Reset()
        {
            Camera.Reset();
            world.Clear_Objects();
            Set_Items();
            foreach (GameObject obj in Objects)
                world.Add_Object(obj);
        }

        private void Set_Items()
        {
            Objects = new List<GameObject>();
            Objects.Add(exit);
            Objects.Add(play);
            Objects.Add(play2);
            Objects.Add(new Background("Sprites/Background/main"));
        }
    }
}
