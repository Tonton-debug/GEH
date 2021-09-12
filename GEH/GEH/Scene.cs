using GEH.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEH
{
  public static   class Scene
    {
     public static  List<Entity> entities = new List<Entity>();
        public static void AddEntity(Entity get)
        {
            entities.Add(get);
        }
       
    }
}
