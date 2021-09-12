using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEH.Entities;
namespace GEH.Components
{
    public abstract class Component
    {
        public bool IsEnabled;
        protected Entity MainEntity;
        protected virtual void CheckIsEnabledComponent()
        {
            if (!IsEnabled)
                throw new ComponentException("This component not enabled");
        }
    }
    public sealed class ComponentException : Exception
    {
        public ComponentException(string ex) : base(ex)
        {

        }
    }
}
