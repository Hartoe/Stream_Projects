using System;

namespace BattleRoyale
{
    class HardAI : AI
    {
        public HardAI()
        {
        }

        public GameObject[,] Update(Player player, GameObject[,] grid)
        {
            if(Has_Neighbours(player, grid))
            {
                var pos = Get_Neighbour(player, grid);
                Player target = (Player) grid[pos.Item1, pos.Item2];

                if(player.health <= 2)
                {
                    if (player.health >= target.health)
                    {
                        grid = Attack(pos.Item1, pos.Item2, grid);
                    }
                    else
                    {
                        int new_x = player.X + (player.X - pos.Item1);
                        int new_y = player.Y + (player.Y - pos.Item2);
                        if (new_x < grid.GetLength(0) && new_x >= 0 && new_y < grid.GetLength(1) && new_y >= 0 && !(grid[new_x, new_y] is Player))
                        {
                            if (Contains_Health(new_x, new_y, grid))
                            {
                                player.Heal();
                            }
                            var res = Move(player, new_x, new_y, grid);
                            player = res.Item1;
                            grid = res.Item2;
                        }
                        else
                        {
                            grid = Attack(pos.Item1, pos.Item2, grid);
                        }
                    }
                }
                else
                {
                    grid = Attack(pos.Item1, pos.Item2, grid);
                }
            }
            else
            {
                if (player.health <= 2)
                {
                    if (Is_Any_Health(grid))
                    {
                        var health_pos = Get_Nearest_Object(player, grid, typeof(Health));
                        var res = Move_Towards(player, health_pos.Item1, health_pos.Item2, grid);
                        player = res.Item1;
                        grid = res.Item2;
                    }
                    else
                    {
                        grid = Move_Random(player, grid);
                    }
                }
                else
                {
                    var prey_pos = Get_Nearest_Object(player, grid, typeof(Player));
                    var res = Move_Towards(player, prey_pos.Item1, prey_pos.Item2, grid);
                    player = res.Item1;
                    grid = res.Item2;
                }
            }

            return grid;
        }

        public Tuple<int, int> Get_Nearest_Object(Player player, GameObject[,] grid, Type obj)
        {
            int cx = 0, cy = 0;
            int closest = 1000000;

            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (x == player.X && y == player.Y)
                        continue;

                    if (grid[x,y].GetType() == obj)
                    {
                        int dif = Math.Abs(player.X - x) + Math.Abs(player.Y - y);
                        if (dif < closest)
                        {
                            closest = dif;
                            cx = x;
                            cy = y;
                        }
                    }
                }
            }

            return Tuple.Create(cx, cy);
        }

        public Tuple<Player, GameObject[,]> Move_Towards(Player player, int x, int y, GameObject[,] grid)
        {
            if (Math.Abs(x-player.X) > Math.Abs(y-player.Y))
            {
                int new_x = player.X + Math.Sign(x - player.X);
                if (Contains_Health(new_x, player.Y, grid))
                {
                    player.Heal();
                }
                return Move(player, new_x, player.Y, grid);
            }
            else
            {
                int new_y = player.Y + Math.Sign(y - player.Y);
                if (Contains_Health(player.X, new_y, grid))
                {
                    player.Heal();
                }
                return Move(player, player.X, new_y, grid);
            }
        }

        public bool Is_Any_Health(GameObject[,] grid)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x,y] is Health)
                        return true;
                }
            }

            return false;
        }

        public GameObject[,] Attack(int x, int y, GameObject[,] grid)
        {
            Player target = (Player) grid[x, y];
            target.TakeDamage();
            grid[x, y] = target;
            return grid;
        }

        public Tuple<Player, GameObject[,]> Move(Player player, int x, int y, GameObject[,] grid)
        {
            grid[player.X, player.Y] = new None();
            player.X = x;
            player.Y = y;
            grid[player.X, player.Y] = player;
            return Tuple.Create(player, grid);
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
                        var res = Move(player, player.X+1, player.Y, grid);
                        player = res.Item1;
                        grid = res.Item2;
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
                        var res = Move(player, player.X-1, player.Y, grid);
                        player = res.Item1;
                        grid = res.Item2;
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
                        var res = Move(player, player.X, player.Y+1, grid);
                        player = res.Item1;
                        grid = res.Item2;
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
                        var res = Move(player, player.X, player.Y-1, grid);
                        player = res.Item1;
                        grid = res.Item2;
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
