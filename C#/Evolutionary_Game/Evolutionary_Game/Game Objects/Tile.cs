using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolutionary_Game
{
    class Tile : GameObject
    {
        public static int Size { get; private set; }

        public Texture2D sprite;
        public uint food_level;

        uint max_food;
        Rectangle source;
        float time;

        public Tile(Vector2 position, GraphicsHandler graphicsHandler, GameWorld gameWorld)
            : base(position, graphicsHandler, gameWorld)
        {
            max_food = 3;
            food_level = max_food;
            sprite = graphicsHandler.Load_Sprite("Sprites/Land/tiles");
            Size = (int)(sprite.Width / (max_food + 1));
            source = new Rectangle(0, 0, Size, sprite.Height);
            time = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (food_level < 0)
                food_level = 0;

            if (food_level > 0 && food_level < max_food)
            {
                if (!gameWorld.Tile_Contains_Creature(this))
                {
                    food_level++;
                    if (food_level > max_food)
                        food_level = max_food;
                }
            }
            else if (food_level <= 0)
            {
                time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (time > 3)
                {
                    if (!gameWorld.Tile_Contains_Creature(this))
                    {
                        food_level++;
                        time = 0;
                    }
                }
            }

            int new_x = (int) ((max_food - food_level) * Size);
            source.X = new_x;
        }

        public override void Draw()
        {
            graphicsHandler.Draw_Sprite(GraphicsHandler.Background, sprite, position, source: source);
        }
    }
}
