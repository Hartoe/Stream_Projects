using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conways_Game_of_Life
{
    interface IGameState
    {
        List<GameObject> Objects { get; set; }
        GameWorld world { get; set; }

        void Reset();
        void Update(GameTime gameTime);
        void Draw();
    }
}
