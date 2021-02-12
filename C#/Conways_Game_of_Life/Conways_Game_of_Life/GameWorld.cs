using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conways_Game_of_Life
{
    class GameWorld
    {
        public static bool Paused = true;

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

        public void Clear_Objects()
        {
            foreach (GameObject obj in add_objects)
                Remove_Object(obj);
            add_objects.Clear();
            foreach (GameObject obj in objects)
                Remove_Object(obj);
        }

        public void Update(GameTime gameTime)
        {
            if (InputHandler.Key_Pressed(Microsoft.Xna.Framework.Input.Keys.Space))
                Paused = !Paused;

            foreach (GameObject obj in add_objects)
            {
                objects.Add(obj);
            }
            add_objects.Clear();

            foreach (GameObject obj in remove_objects)
            {
                objects.Remove(obj);
            }
            remove_objects.Clear();

            foreach (GameObject obj in objects)
                obj.Update(gameTime);
        }

        public void Draw()
        {
            foreach (GameObject obj in objects)
                obj.Draw();
        }
    }
}
