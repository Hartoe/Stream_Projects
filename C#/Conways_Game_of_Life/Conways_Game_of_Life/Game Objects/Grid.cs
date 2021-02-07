using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conways_Game_of_Life
{
    class Grid : GameObject
    {
        Cell[,] cells;
        float time, tick_speed;

        public Grid(Vector2 position, int width, int height, int cell_height) : base(position)
        {
            cells = Init_Grid(width, height, cell_height);
            time = 0;
            tick_speed = 0.5f;
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.Key_Pressed(Microsoft.Xna.Framework.Input.Keys.OemPlus))
            {
                tick_speed -= 0.1f;
                if (tick_speed < 0.1f)
                {
                    tick_speed = 0.1f;
                }
            }

            if (InputHandler.Key_Pressed(Microsoft.Xna.Framework.Input.Keys.OemMinus))
            {
                tick_speed += 0.1f;
                if (tick_speed > 1.5f)
                {
                    tick_speed = 1.5f;
                }
            }

            time += (float) gameTime.ElapsedGameTime.TotalSeconds;
            if (time >= tick_speed)
            {
                if (!GameWorld.Paused)
                    Check_Cell_State(gameTime);
                time = 0;
            }

            Update_Cell_State(gameTime);
        }

        public override void Draw()
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    cells[x, y].Draw();
                }
            }
        }

        private Cell[,] Init_Grid(int width, int height, int cell_width)
        {
            Cell[,] res = new Cell[width, height];

            for (int x = 0; x < res.GetLength(0); x++)
            {
                for (int y = 0; y < res.GetLength(1); y++)
                {
                    res[x, y] = new Cell(new Vector2(x * cell_width, y * cell_width));
                }
            }

            return res;
        }

        private void Check_Cell_State(GameTime gameTime)
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    int num = Get_Num_Neighbours(x, y);
                    if (cells[x,y].alive)
                    {
                        if (num < 2 || num > 3)
                            cells[x, y].Destroy_Cell();
                    }
                    else
                    {
                        if (num == 3)
                            cells[x, y].Create_Cell();
                    }
                }
            }
        }

        private void Update_Cell_State(GameTime gameTime)
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    cells[x, y].Update(gameTime);
                }
            }
        }

        private int Get_Num_Neighbours(int x, int y)
        {
            int result = 0;

            for (int xx = -1; xx < 2; xx++)
            {
                for (int yy = -1; yy < 2; yy++)
                {
                    if (xx == 0 && yy == 0)
                        continue;

                    if (x+xx < 0 || x+xx >= cells.GetLength(0) || y+yy < 0 || y+yy >= cells.GetLength(1)) continue;

                    if (cells[x+xx, y+yy].alive)
                        result++;
                }
            }

            return result;
        }
    }
}
