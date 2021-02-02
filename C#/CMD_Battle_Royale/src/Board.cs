using System;
using System.Collections.Generic;
using System.Threading;

namespace BattleRoyale
{
    class Board
    {
        private string name;
        private bool autotick;
        private int tickspeed;
        private int ai;

        private int size;
        private GameObject[,] grid;
        private List<Player> players;
        private int player_count;
        private int max_health;
        private int health_count;
        private List<int> empty_spaces;

        public Board(string name, bool autotick, int tickspeed, int ai)
        {
            this.name = name;
            this.autotick = autotick;
            this.tickspeed = tickspeed;
            this.ai = ai;

            players = new List<Player>();
        }

        public void Start_New_Game()
        {
            Console.Clear();
            Console.Write("====================\n" + 
                          "Initializing game...\n" +
                          "Autotick: " + autotick.ToString() + " (" + tickspeed.ToString() + ")\n" +
                          "AI Difficulty: " + ai.ToString() + "\n" + 
                          "====================\n" +
                          "\n" +
                          "Enter the size of the game board: (min 3, max 12)\n" +
                          name + "> ");
            string input = Console.ReadLine().ToLower();
            try
            {
                size = Math.Min(Math.Max(3, int.Parse(input)), 12);
            }
            catch
            {
                Console.WriteLine("Value " + input + " is not a valid number!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Start_New_Game();
                return;
            }
            grid = new GameObject[size,size];
            max_health = size - 2;
            Get_Players();
        }

        public void Get_Players()
        {
            Console.Clear();
            Console.Write("=================\n" + 
                          "Autotick: " + autotick.ToString() + "(" + tickspeed.ToString() + ")\n" +
                          "AI Difficulty: " + ai.ToString() + "\n" +
                          "Size: " + size.ToString() + "\n" +
                          "Players (max " + (size*size).ToString() + "): " + players.Count.ToString() + "\n" +
                          "=================\n" +
                          "\n" +
                          "Enter a player name to add a player.\n" +
                          "Enter 'done' or leave blank to start.\n" +
                          name + "> ");
            string input = Console.ReadLine();
            if (input == "" || input.ToLower() == "done")
            {
                if (players.Count <= 0)
                {
                    Console.WriteLine("There must be atleast one player!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Get_Players();
                }
                else
                {
                    Console.WriteLine("Ready to start game!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    player_count = players.Count;
                    Init_Grid();
                }
            }
            else
            {
                players.Add(new Player(input, this, ai));
                if (players.Count == size * size)
                {
                    Console.WriteLine("Max players reached! Ready to start game!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    player_count = players.Count;
                    Init_Grid();
                    return;
                }
                Get_Players();
            }
        }

        public void Init_Grid()
        {
            empty_spaces = new List<int>();
            for (int i = 0; i < size*size; i++)
            {
                empty_spaces.Add(i);
            }

            foreach (Player p in players)
            {
                int i = Program.Random.Next(empty_spaces.Count);
                int space = empty_spaces[i];
                int x = space % size;
                int y = (int) (space / size);
                grid[x,y] = p;
                p.X = x;
                p.Y = y;
                empty_spaces.RemoveAt(i);
            }

            foreach (int space in empty_spaces)
            {
                int x = space % size;
                int y = (int) (space / size);
                grid[x,y] = new None();
            }
            health_count = 0;

            Game_Loop();
        }

        public void Game_Loop()
        {
            bool first = true;
            while(!Is_Game_Finished())
            {
                Console.Clear();
                if (!first)
                {
                    Update();
                }
                Draw();
                if (first)
                {
                    first = false;
                }
                
                Spawn_Health();

                if (autotick)
                {
                    Thread.Sleep(tickspeed);
                }
                else
                {
                    Console.ReadKey();
                }
            }

            Game_End();
        }

        public void Spawn_Health()
        {
            if (health_count < max_health)
            {
                if(Program.Random.Next(3) == 0)
                {
                    int i = Program.Random.Next(empty_spaces.Count);
                    int space = empty_spaces[i];
                    int x = space % size;
                    int y = (int) (space  / size);
                    if(grid[x,y] is None)
                    {
                        grid[x,y] = new Health();
                        health_count++;
                    }
                }
            }
        }

        public void Update()
        {
            foreach (Player p in players)
            {
                if (p.Is_Alive())
                {
                    grid = p.Update(grid);
                }
            }
        }

        public void Draw()
        {
            // Draw Game Board
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    Console.Write("[");
                    grid[x,y].Draw();
                    Console.Write("]");
                }
                Console.Write("\n");
            }
            
            ConsoleColor old_fcolor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--Alive--");
            foreach(Player p in players)
            {
                if (p.Is_Alive())
                {
                    Console.WriteLine(p.name);
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--Dead--");
            foreach(Player p in players)
            {
                if (!p.Is_Alive())
                {
                    Console.WriteLine(p.name);
                }
            }
            Console.ForegroundColor = old_fcolor;
        }

        public void Game_End()
        {
            Console.Clear();
            Console.WriteLine("CONGRATULATIONS! " + Get_Winner().name + " HAS WON THE GAME!");
            Console.WriteLine("Do you want to play again? (yes/no)");
            Console.Write(name + "> ");
            string input = Console.ReadLine().ToLower();
            if (input == "yes")
            {
                foreach (Player p in players)
                {
                    p.Revive();
                }

                player_count = players.Count;
                grid = new GameObject[size,size];
                Init_Grid();
            }
            else if (input == "no")
            {
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Please write 'yes' or 'no'!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Game_End();
                return;
            }
        }

        public Player Get_Winner()
        {
            foreach(Player p in players)
            {
                if (p.Is_Alive())
                    return p;
            }

            return players[0];
        }

        public bool Is_Game_Finished()
        {
            return player_count == 1;
        }

        public void Decrease_Player_Count()
        {
            player_count--;
        }

        public void Decrease_Health_Count()
        {
            health_count--;
        }
    }
}
