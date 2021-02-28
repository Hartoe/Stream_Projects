using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Exceptions
{
    class SoundExceptions : Exception
    {
        public SoundExceptions(string name)
            : base($"Could not find the sound file for {name}!")
        {
        }
    }
}
