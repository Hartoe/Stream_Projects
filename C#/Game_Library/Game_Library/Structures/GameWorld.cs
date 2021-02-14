using Game_Library.Game_Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Structures
{
    public class GameWorld
    {
        List<GameObject> game_objects, add_objects, remove_objects; 

        public GameWorld()
        {
            Reset();
        }

        public void Add_Object(GameObject obj)
        {
            add_objects.Add(obj);
        }

        public void Remove_Object(GameObject obj)
        {
            remove_objects.Add(obj);
        }

        public void Clear_Objects()
        {
            add_objects.Clear();
            foreach (GameObject obj in game_objects)
                Remove_Object(obj);
        }

        public void Set_Objects(List<GameObject> objects)
        {
            Clear_Objects();
            foreach (GameObject obj in objects)
                Add_Object(obj);
        }

        public GameObject Find_Object(string id)
        {
            var items = game_objects.Where(o => o.id == id);
            return items.FirstOrDefault();
        }

        public List<GameObject> Filter_Objects(GameObject type)
        {
            return game_objects.Where(o => o.GetType() == type.GetType()).ToList();
        }

        public void Update(GameTime gameTime)
        {
            foreach (GameObject obj in add_objects)
                game_objects.Add(obj);
            add_objects.Clear();

            foreach (GameObject obj in remove_objects)
                game_objects.Remove(obj);
            remove_objects.Clear();

            foreach (GameObject obj in game_objects)
                obj.Update(gameTime);
        }

        public void Draw()
        {
            foreach (GameObject obj in game_objects)
                obj.Draw();
        }

        public void Reset()
        {
            game_objects = new List<GameObject>();
            add_objects = new List<GameObject>();
            remove_objects = new List<GameObject>();
        }
    }
}
