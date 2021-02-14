using Game_Library.Exceptions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Structures
{
    public class GameStateManager
    {
        public GameState Current_State { get; private set; }
        Dictionary<string, GameState> states;

        public GameStateManager()
        {
            states = new Dictionary<string, GameState>();
        }

        public void Add_State(string name, GameState state)
        {
            if (!states.ContainsKey(name))
                states.Add(name, state);
            else
                throw new DuplicateStateException(name);
        }

        public void Remove_State(string name)
        {
            if (states.ContainsKey(name))
                states.Remove(name);
            else
                throw new StateNotFoundException(name);
        }

        public void Set_State(string name)
        {
            if (states.ContainsKey(name))
            {
                Current_State = states[name];
                Current_State.Reset();
            }
            else
                throw new StateNotFoundException(name);
        }

        public GameState Get_State(string name)
        {
            if (states.ContainsKey(name))
                return states[name];
            else
                throw new StateNotFoundException(name);
        }

        public void Update(GameTime gameTime)
        {
            if (Current_State != null)
                Current_State.Update(gameTime);
        }

        public void Draw()
        {
            if (Current_State != null)
                Current_State.Draw();
        }

        public void Reset()
        {
            Current_State = null;
            states = new Dictionary<string, GameState>();
        }
    }
}
