using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolutionary_Game
{
    class Creature : GameObject
    {
        Color color;
        Texture2D sprite;
        Vector2 origin;
        public int max_health, max_hunger, max_rep, thresh;
        int health, hunger, rep_need, mutation_value;
        float length, range;

        public Creature(Vector2 position, GraphicsHandler graphicsHandler, GameWorld gameWorld, int health,
            int hunger, int rep_need, int thresh, Color color) : base(position, graphicsHandler, gameWorld)
        {
            sprite = graphicsHandler.Load_Sprite("Sprites/Creature/idle");
            origin = new Vector2(sprite.Width/2, sprite.Height/2);

            max_health = health;
            this.health = max_health;

            max_hunger = hunger;
            this.hunger = max_hunger;

            max_rep = rep_need;
            this.rep_need = max_rep;

            this.thresh = thresh;
            this.color = color;

            length = Tile.Size/2;
            mutation_value = 15;
            range = Tile.Size;
        }

        public override void Update(GameTime gameTime)
        {
            Update_Values();

            if (hunger < thresh || rep_need < thresh)
            {
                if (hunger <= rep_need)
                {
                    Act_On_Hunger();
                }
                else
                {
                    Act_On_Reproduction();
                }
            }
            else
            {
                Roam();
            }
        }

        public override void Draw()
        {
            graphicsHandler.Draw_Sprite(GraphicsHandler.Foreground, sprite, position, color: color, origin: origin);
        }

        private void Update_Values()
        {
            if (health < 0)
                gameWorld.Remove_Object(this);
            health--;

            hunger--;
            if (hunger <= 0)
            {
                hunger = 0;
                health -= max_hunger*3;
            }

            rep_need--;
            if (rep_need < 0)
                rep_need = 0;
        }

        private void Roam()
        {
            float angle = MainGame.Random.Next(360);
            angle = (float)(angle * Math.PI / 180);
            Vector2 dir = new Vector2((float)(length * Math.Cos(angle)), (float)(length * Math.Sin(angle)));
            position += dir;
            if (position.X < 0)
                position.X = 1;
            if (position.Y < 0)
                position.Y = 1;
            if (position.X > MainGame.WindowSize.X)
                position.X = MainGame.WindowSize.X-1;
            if (position.Y > MainGame.WindowSize.Y)
                position.Y = MainGame.WindowSize.Y-1;
        }

        private void Act_On_Hunger()
        {
            Tile tile = gameWorld.Get_Creature_Tile(this);

            if (tile != null)
            {
                if (tile.food_level > 0)
                {
                    tile.food_level--;
                    hunger = max_hunger;
                }
                else
                {
                    Roam();
                }
            }
        }

        private void Act_On_Reproduction()
        {
            if (gameWorld.Creature_In_Range(this, range) && gameWorld.Population <= 500)
            {
                Creature other = gameWorld.Get_Closest_Creature(this, range);
                rep_need = max_rep;
                Creature child = Create_New_Child(other);
                gameWorld.Add_Object(child);
            }
            else
            {
                Roam();
            }
        }

        private Creature Create_New_Child(Creature other)
        {
            int child_health, child_hunger, child_rep, child_thresh;

            int health_chance = MainGame.Random.Next(2);
            if (health_chance == 0)
                child_health = max_health;
            else
                child_health = other.max_health;

            int hunger_chance = MainGame.Random.Next(2);
            if (hunger_chance == 0)
                child_hunger = max_hunger;
            else
                child_hunger = other.max_hunger;

            int rep_chance = MainGame.Random.Next(2);
            if (rep_chance == 0)
                child_rep = max_rep;
            else
                child_rep = other.max_rep;

            int thresh_chance = MainGame.Random.Next(2);
            if (thresh_chance == 0)
                child_thresh = thresh;
            else
                child_thresh = other.thresh;

            child_health = Mutate(child_health);
            child_hunger = Mutate(child_hunger);
            child_rep = Mutate(child_rep);
            child_thresh = Mutate(child_thresh);

            Color child_color;
            int color_chance = MainGame.Random.Next(2);
            if (color_chance == 0)
                child_color = color;
            else
                child_color = other.color;

            return new Creature(position, graphicsHandler, gameWorld, child_health, child_hunger, child_rep, child_thresh, child_color);
        }

        public int Mutate(int value)
        {
            int chance = MainGame.Random.Next(9);
            if (chance == 0)
            {
                return Math.Abs(value + MainGame.Random.Next(-mutation_value, mutation_value));
            }
            return value;
        }
    }
}
