using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Game_Objects
{
    public abstract class GameObject
    {
        public Vector2 position;
        public string id;

        public GameObject(Vector2 position, string id = "")
        {
            this.position = position;
            this.id = id;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw();
    }
}
