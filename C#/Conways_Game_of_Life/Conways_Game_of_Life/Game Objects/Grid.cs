using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conways_Game_of_Life
{
    class Grid : GameObject
    {
        public Cell[,] cells;
        SpriteFont font;
        float time, tick_speed;
        bool draw_indicator, classic;
        StatusBar bar;

        public Grid(Vector2 position, int width, int height, int cell_height, bool classic) : base(position)
        {
            this.classic = classic;
            cells = Init_Grid(width, height, cell_height);
            time = 0;
            tick_speed = 0.5f;
            font = GraphicsHandler.Load_Font("Fonts/Button/default");
            draw_indicator = true;
            bar = new StatusBar(new Vector2(0,450), this);
        }

        public override void Update(GameTime gameTime)
        {
            if (!classic)
                bar.Update(gameTime);

            if (InputHandler.Key_Pressed(Microsoft.Xna.Framework.Input.Keys.Q))
                draw_indicator = !draw_indicator;

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

            if (InputHandler.Key_Pressed(Microsoft.Xna.Framework.Input.Keys.R))
            {
                Reset_Grid();
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
            if (!classic)
                bar.Draw();

            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    cells[x, y].Draw();
                }
            }

            if (draw_indicator)
            {
                if (GameWorld.Paused)
                {
                    GraphicsHandler.Draw_String(GraphicsHandler.GUI, font, Vector2.Zero, "Paused", Color.Green);
                }
                else
                {
                    GraphicsHandler.Draw_String(GraphicsHandler.GUI, font, Vector2.Zero, "Playing...", Color.Red);
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
                    res[x, y] = new Cell(new Vector2(x * cell_width, y * cell_width), classic);
                }
            }

            return res;
        }

        private void Check_Cell_State(GameTime gameTime)
        {
            if (classic)
            {
                Check_Classic();
            }
            else
            {
                Check_Dual();
            }
        }

        private void Check_Dual()
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    (int red, int blue) num = Get_Seperate_Neighbours(x, y);
                    if (cells[x, y].alive)
                    {
                        if (cells[x, y].red)
                        {
                            if (num.blue > num.red)
                                cells[x, y].Destroy_Cell();
                            if (num.red < 2 || num.red > 3 || (num.red+num.blue) > 3)
                                cells[x, y].Destroy_Cell();
                        }
                        else
                        {
                            if (num.red > num.blue)
                                cells[x, y].Destroy_Cell();
                            if (num.blue < 2 || num.blue > 3 || (num.red + num.blue) > 3)
                                cells[x, y].Destroy_Cell();
                        }
                    }
                    else
                    {
                        if (num.red == 3 && num.red > num.blue)
                        {
                            cells[x, y].red = true;
                            cells[x, y].Create_Cell();
                        }
                        else if (num.blue == 3 && num.blue > num.red)
                        {
                            cells[x, y].red = false;
                            cells[x, y].Create_Cell();
                        }
                    }
                }
            }
        }

        private void Check_Classic()
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    int num = Get_Num_Neighbours(x, y);
                    if (cells[x, y].alive)
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

        private (int, int) Get_Seperate_Neighbours(int x, int y)
        {
            (int red, int blue) result = (0, 0);

            for (int xx = -1; xx < 2; xx++)
            {
                for (int yy = -1; yy < 2; yy++)
                {
                    if (xx == 0 && yy == 0)
                        continue;

                    if (x + xx < 0 || x + xx >= cells.GetLength(0) || y + yy < 0 || y + yy >= cells.GetLength(1)) continue;

                    if (cells[x + xx, y + yy].alive)
                    {
                        if (cells[x + xx, y + yy].red)
                            result.red++;
                        else
                            result.blue++;
                    }
                }
            }

            return result;
        }

        private void Reset_Grid()
        {
            foreach (Cell c in cells)
                c.Destroy_Cell();
        }
    }
}
