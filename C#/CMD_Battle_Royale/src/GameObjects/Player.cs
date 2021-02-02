using System;

namespace BattleRoyale
{
    class Player : GameObject
    {
        public Board game;
        public string name { get; private set; }
        public int X, Y;
        private bool alive;
        private AI ai;

        public Player(string name, Board game, int ai)
        {
            this.game = game;
            this.name = name;
            bcolor = ConsoleColor.Green;
            symbol = name.ToUpper()[0].ToString();
            health = 4;
            alive = true;
            switch(ai)
            {
                case 0:
                    this.ai = new EasyAI();
                    break;
                case 1:
                    this.ai = new MediumAI();
                    break;
                case 2:
                    this.ai = new HardAI();
                    break;
            }
        }

        public bool Is_Alive()
        {
            return alive;
        }

        public void Revive()
        {
            alive = true;
            health = 4;
            Update_Color();
        }

        public void Heal()
        {
            health++;
            if (health > 4)
                health = 4;
            game.Decrease_Health_Count();
        }

        public GameObject[,] Update(GameObject[,] grid)
        {
            Update_Color();

            if (health <= 0)
            {
                alive = false;
                game.Decrease_Player_Count();
                grid[X, Y] = new None();
            }
            else
            {
                grid = ai.Update(this, grid);
            }
            return grid;
        }

        public void Update_Color()
        {
            switch(health)
            {
                case 4:
                    bcolor = ConsoleColor.Green;
                    break;
                case 3:
                    bcolor = ConsoleColor.Yellow;
                    break;
                case 2:
                    bcolor = ConsoleColor.Red;
                    break;
                default:
                    bcolor = ConsoleColor.DarkRed;
                    break;
            }
        }
    }
}
