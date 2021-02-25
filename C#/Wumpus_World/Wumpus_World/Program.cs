using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus_World
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MainGame game = new MainGame())
            {
                game.Run();
            }
        }
    }
}
