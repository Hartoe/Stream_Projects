using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conways_Game_of_Life
{
    class GameStateManager
    {
        public IGameState CurrentState { get; private set; }
        private Dictionary<string, IGameState> states;

        public GameStateManager()
        {
            states = new Dictionary<string, IGameState>();
        }

        public void Add_State(string name, IGameState state)
        {
            states.Add(name, state);
        }

        public void Set_State(string name)
        {
            CurrentState = states[name];
            CurrentState.Reset();
        }

        public void Remove_State(string name)
        {
            states.Remove(name);
        }

        public string Get_State()
        {
            return states.FirstOrDefault(x => x.Value == CurrentState).Key;
        }

        public void Update(GameTime gameTime)
        {
            if (CurrentState != null)
                CurrentState.Update(gameTime);
        }

        public void Draw()
        {
            if (CurrentState != null)
                CurrentState.Draw();
        }
    }
}
