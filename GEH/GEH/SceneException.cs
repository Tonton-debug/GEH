using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEH
{
    sealed class  SceneException:Exception
    {
        public SceneException(string message) : base(message)
        {

        }
    }
}
