using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Conways_Game_of_Life
{
    class StatusBar : GameObject
    {
        Texture2D red_bar, blue_bar;
        Grid grid;
        float scale;

        public StatusBar(Vector2 position, Grid grid) : base(position)
        {
            this.grid = grid;
            scale = 0.5f;
            red_bar = GraphicsHandler.Load_Sprite("Sprites/Bar/red_bar");
            blue_bar = GraphicsHandler.Load_Sprite("Sprites/Bar/blue_bar");
        }

        public override void Update(GameTime gameTime)
        {
            (int total, int red) count = Count_Cells();
            if (count.total != 0)
                scale = (float)count.red / (float)count.total;
        }

        public override void Draw()
        {
            GraphicsHandler.Draw_Sprite(GraphicsHandler.GUI, blue_bar, position, new Vector2(1, 0.6f), Color.White*0.5f);
            GraphicsHandler.Draw_Sprite(GraphicsHandler.GUI, red_bar, position, new Vector2(scale, 0.6f), Color.White*0.5f);
        }

        public (int, int) Count_Cells()
        {
            (int total, int red) result = (0,0);

            for (int x = 0; x < grid.cells.GetLength(0); x++)
            {
                for (int y = 0; y < grid.cells.GetLength(1); y++)
                {
                    if (grid.cells[x, y].alive)
                    {
                        result.total++;
                        if (grid.cells[x, y].red)
                            result.red++;
                    }
                }
            }

            return result;
        }
    }
}
