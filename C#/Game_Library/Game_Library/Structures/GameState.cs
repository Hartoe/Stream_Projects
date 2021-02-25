using Game_Library.Game_Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Structures
{
    public abstract class GameState
    {
        protected List<GameObject> objects;

        public GameState()
        {
            Init_Objects();
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw();

        public virtual void Reset()
        {
            Init_Objects();
        }

        public virtual void Init_Objects()
        {
            objects = new List<GameObject>();
        }
    }
}
