using GEH.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEH
{
    public abstract class Scene
    {
        private List<Prefab> prefabsInScene = new List<Prefab>();
        public List<Prefab> GetAllPrefabsInScene()
        {
            return prefabsInScene;
        }
        protected void AddPrefabsInScene(Prefab prefab)
        {
            prefabsInScene.Add(prefab);
        }
       

    }
}
