using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolutionary_Game
{
    class GameObject
    {
        public Vector2 position;

        protected GraphicsHandler graphicsHandler;
        protected GameWorld gameWorld;

        public GameObject(Vector2 position, GraphicsHandler graphicsHandler, GameWorld gameWorld)
        {
            this.position = position;
            this.graphicsHandler = graphicsHandler;
            this.gameWorld = gameWorld;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw()
        {
        }
    }
}
