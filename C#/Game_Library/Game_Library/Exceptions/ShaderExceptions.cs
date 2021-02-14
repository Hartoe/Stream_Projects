using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Exceptions
{
    class DuplicateShaderGroupException : Exception
    {
        public DuplicateShaderGroupException(string name)
            : base($"Shader group {name} already existsed in the Dictionary!")
        {
        }
    }

    class ShaderGroupNotFoundException : Exception
    {
        public ShaderGroupNotFoundException(string name)
            : base($"Shader group {name} was not found in the Dictionary!")
        {
        }
    }
}
