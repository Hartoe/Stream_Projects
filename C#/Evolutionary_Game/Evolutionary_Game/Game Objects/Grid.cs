using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Evolutionary_Game
{
    class Grid : GameObject
    {
        Tile[,] tiles;

        public Grid(Vector2 position, GraphicsHandler graphicsHandler, GameWorld gameWorld,
            Point size) : base(position, graphicsHandler, gameWorld)
        {
            tiles = new Tile[size.X,size.Y];
            Initialize_Tiles();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw()
        {
        }

        private void Initialize_Tiles()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    tiles[x, y] = new Tile(new Vector2(x * Tile.Size, y * Tile.Size), graphicsHandler, gameWorld);
                    gameWorld.Add_Object(tiles[x, y]);
                }
            }
        }
    }
}
