using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conways_Game_of_Life
{
    abstract class GameObject
    {
        public Vector2 position;

        public GameObject(Vector2 position)
        {
            this.position = position;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw()
        {
        }
    }
}
