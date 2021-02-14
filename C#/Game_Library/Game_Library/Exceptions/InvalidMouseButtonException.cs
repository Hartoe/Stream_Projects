using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Exceptions
{
    class InvalidMouseButtonException : Exception
    {
        public InvalidMouseButtonException(string button)
            : base($"{button} is an invalid mouse button!")
        {

        }
    }
}
