using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEH
{
    
    sealed class EntityException : Exception
    {
        public EntityException(string message) : base(message)
        {

        }
    }
}
