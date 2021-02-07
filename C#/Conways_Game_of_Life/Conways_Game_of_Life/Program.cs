using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conways_Game_of_Life
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var game = new MainGame())
                game.Run();
        }
    }
}
