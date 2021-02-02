using System;

namespace BattleRoyale
{
    class GameObject
    {
        protected ConsoleColor bcolor, fcolor;
        protected string symbol;
        public int health { get; protected set;}

        public GameObject()
        {
            bcolor = Console.BackgroundColor;
            fcolor = Console.ForegroundColor;
            symbol = " ";
            health = 4;
        }

        public void Draw()
        {
            ConsoleColor old_bcolor = Console.BackgroundColor;
            ConsoleColor old_fcolor = Console.ForegroundColor;
            Console.BackgroundColor = bcolor;
            Console.ForegroundColor = fcolor;
            Console.Write(symbol);
            Console.BackgroundColor = old_bcolor;
            Console.ForegroundColor = old_fcolor;
        }

        public void TakeDamage()
        {
            health--;
        }
    }
}
