using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolutionary_Game
{
    class GameWorld
    {
        public int Population => objects.OfType<Creature>().Count();
        List<GameObject> objects, add_objects, remove_objects;

        public GameWorld()
        {
            objects = new List<GameObject>();
            add_objects = new List<GameObject>();
            remove_objects = new List<GameObject>();
        }

        public void Add_Object(GameObject obj)
        {
            add_objects.Add(obj);
        }

        public void Remove_Object(GameObject obj)
        {
            remove_objects.Add(obj);
        }

        public void Update(GameTime gameTime)
        {
            foreach (GameObject obj in add_objects)
                objects.Add(obj);
            add_objects.Clear();

            foreach (GameObject obj in remove_objects)
                objects.Remove(obj);
            remove_objects.Clear();

            foreach (GameObject obj in objects)
                obj.Update(gameTime);
        }

        public void Draw()
        {
            foreach (GameObject obj in objects)
                obj.Draw();
        }

        public void Reset()
        {
            add_objects.Clear();
            foreach (GameObject obj in objects)
                Remove_Object(obj);
        }

        public Tile Get_Creature_Tile(Creature c)
        {
            foreach (Tile t in objects.OfType<Tile>())
            {
                if (Creature_In_Tile(c, t))
                    return t;
            }

            return null;
        }

        public bool Tile_Contains_Creature(Tile t)
        {
            foreach (Creature c in objects.OfType<Creature>())
            {
                if (Creature_In_Tile(c, t))
                    return true;
            }

            return false;
        }

        public bool Creature_In_Tile(Creature c, Tile t)
        {
            return (c.position.X > t.position.X &&
                    c.position.X < t.position.X + Tile.Size &&
                    c.position.Y > t.position.Y &&
                    c.position.Y < t.position.Y + Tile.Size);
        }

        public bool Creature_In_Range(Creature mid, float range)
        {
            foreach (Creature c in objects.OfType<Creature>())
            {
                if (c == mid) continue;

                if ((mid.position - c.position).Length() <= range)
                    return true;
            }

            return false;
        }

        public Creature Get_Closest_Creature(Creature mid, float range)
        {
            float closest = range;
            Creature closest_c = mid;

            foreach (Creature c in objects.OfType<Creature>())
            {
                if ((mid.position - c.position).Length() <= closest)
                {
                    closest = (mid.position - c.position).Length();
                    closest_c = c;
                }
            }

            return closest_c;
        }

        public void Calculate_Average_Values()
        {
            int health = 0;
            int hunger = 0;
            int rep_need = 0;
            int thresh = 0;

            foreach (Creature c in objects.OfType<Creature>())
            {
                health += c.max_health;
                hunger += c.max_hunger;
                rep_need += c.max_rep;
                thresh += c.thresh;
            }

            Console.WriteLine($"Creature Population: {Population}");
            Console.WriteLine($"Average Values:\nHealth: {health / Population},\nHunger: {hunger / Population},\n" +
                $"Reproduction: {rep_need / Population},\nThreshold: {thresh / Population}");
        }
    }
}
