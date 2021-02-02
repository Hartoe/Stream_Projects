using System;

namespace BattleRoyale
{
    class MediumAI : AI
    {
        public MediumAI()
        {
        }

        public GameObject[,] Update(Player player, GameObject[,] grid)
        {
            if (Has_Neighbours(player, grid))
            {
                if(player.health <= 2)
                {
                    var pos = Get_Neighbour(player, grid);
                    Player target = (Player) grid[pos.Item1, pos.Item2];
                    if (target.health <= player.health)
                    {
                        grid = Attack(pos.Item1, pos.Item2, grid);
                    }
                    else
                    {
                        int new_x = player.X + (player.X - pos.Item1);
                        int new_y = player.Y + (player.Y - pos.Item2);
                        if (new_x < grid.GetLength(0) && new_x >= 0 && new_y < grid.GetLength(1) && new_y >= 0)
                        {
                            if (!(grid[new_x, new_y] is Player))
                            {
                                grid[player.X, player.Y] = new None();
                                player.X = new_x;
                                player.Y = new_y;
                                grid[player.X, player.Y] = player;
                            }
                            else
                            {
                                grid = Attack(pos.Item1, pos.Item2, grid);
                            }
                        }
                        else
                        {
                            grid = Attack(pos.Item1, pos.Item2, grid);
                        }
                    }
                }
                else
                {
                    var pos = Get_Neighbour(player, grid);
                    grid = Attack(pos.Item1, pos.Item2, grid);
                }
            }
            else
            {
                if (player.health < 4)
                {
                    bool up = false, down = false, left = false, right = false;
                    if (player.X+1 < grid.GetLength(0))
                    {
                        right = grid[player.X+1, player.Y] is Health;
                    }
                    if (player.X-1 >= 0)
                    {
                        left = grid[player.X-1, player.Y] is Health;
                    }
                    if (player.Y+1 < grid.GetLength(1))
                    {
                        down = grid[player.X, player.Y+1] is Health;
                    }
                    if (player.Y-1 >= 0)
                    {
                        up = grid[player.X, player.Y-1] is Health;
                    }

                    if (right)
                    {
                        grid[player.X, player.Y] = new None();
                        player.X++;
                        player.Heal();
                        grid[player.X, player.Y] = player;
                    }
                    else if (left)
                    {
                        grid[player.X, player.Y] = new None();
                        player.X--;
                        player.Heal();
                        grid[player.X, player.Y] = player;
                    }
                    else if (up)
                    {
                        grid[player.X, player.Y] = new None();
                        player.Y--;
                        player.Heal();
                        grid[player.X, player.Y] = player;
                    }
                    else if (down)
                    {
                        grid[player.X, player.Y] = new None();
                        player.Y++;
                        player.Heal();
                        grid[player.X, player.Y] = player;
                    }
                    else
                    {
                        grid = Move_Random(player, grid);
                    }
                }
                else
                {
                    grid = Move_Random(player, grid);
                }
            }

            return grid;
        }

        public GameObject[,] Attack(int x, int y, GameObject[,] grid)
        {
            Player target = (Player) grid[x, y];
            target.TakeDamage();
            grid[x, y] = target;
            return grid;
        }

        public GameObject[,] Move_Random(Player player, GameObject[,] grid)
        {
            int dir = Program.Random.Next(4);

            switch(dir)
            {
                // Right
                case 0:
                    if (player.X+1 < grid.GetLength(0))
                    {
                        if (Contains_Health(player.X+1, player.Y, grid))
                        {
                            player.Heal();
                        }
                        grid[player.X, player.Y] = new None();
                        player.X++;
                        grid[player.X, player.Y] = player;
                    }
                    break;
                // Left
                case 1:
                    if (player.X-1 >= 0)
                    {
                        if (Contains_Health(player.X-1, player.Y, grid))
                        {
                            player.Heal();
                        }
                        grid[player.X, player.Y] = new None();
                        player.X--;
                        grid[player.X, player.Y] = player;
                    }
                    break;
                // Down
                case 2:
                    if (player.Y+1 < grid.GetLength(1))
                    {
                        if (Contains_Health(player.X, player.Y+1, grid))
                        {
                            player.Heal();
                        }
                        grid[player.X, player.Y] = new None();
                        player.Y++;
                        grid[player.X, player.Y] = player;
                    }
                    break;
                // Up
                case 3:
                    if (player.Y-1 >= 0)
                    {
                        if (Contains_Health(player.X, player.Y-1, grid))
                        {
                            player.Heal();
                        }
                        grid[player.X, player.Y] = new None();
                        player.Y--;
                        grid[player.X, player.Y] = player;
                    }
                    break;
            }
            
            return grid;
        }

        public bool Contains_Health(int x, int y, GameObject[,] grid)
        {
            return grid[x,y] is Health;
        }

        public Tuple<int, int> Get_Neighbour(Player player, GameObject[,] grid)
        {
            if (player.X+1 < grid.GetLength(0))
            {
                if (grid[player.X+1, player.Y] is Player)
                {
                    return Tuple.Create(player.X+1, player.Y);
                }
            }
            if (player.X-1 >= 0)
            {
                if (grid[player.X-1, player.Y] is Player)
                {
                    return Tuple.Create(player.X-1, player.Y);
                }
            }
            if (player.Y+1 < grid.GetLength(1))
            {
                if (grid[player.X, player.Y+1] is Player)
                {
                    return Tuple.Create(player.X, player.Y+1);
                }
            }
            if (player.Y-1 >= 0)
            {
                if (grid[player.X, player.Y-1] is Player)
                {
                    return Tuple.Create(player.X, player.Y-1);
                }
            }

            return Tuple.Create(0, 0);

        }

        public bool Has_Neighbours(Player player, GameObject[,] grid)
        {
            bool left = false, right = false, up = false, down = false;
            
            if (player.X+1 < grid.GetLength(0))
            {
                right = grid[player.X+1, player.Y] is Player;
            }
            if (player.X-1 >= 0)
            {
                left = grid[player.X-1, player.Y] is Player;
            }
            if (player.Y+1 < grid.GetLength(1))
            {
                down = grid[player.X, player.Y+1] is Player;
            }
            if (player.Y-1 >= 0)
            {
                up = grid[player.X, player.Y-1] is Player;
            }

            return left || right || up || down;
        }
    }
}
