using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Exceptions
{
    class StateNotFoundException : Exception
    {
        public StateNotFoundException()
        {
        }
        public StateNotFoundException(string name)
            : base($"State {name} was not present in Game State Manager!")
        {
        }
    }

    class DuplicateStateException : Exception
    {
        public DuplicateStateException()
        {
        }
        public DuplicateStateException(string name)
            : base($"State {name} already exsists in Game State Manager!")
        {
        }
    }
}
