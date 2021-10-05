using GEH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
namespace GameDLL
{
    class MainScene:Scene
    {
        public MainScene()
        {
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                AddPrefabsInScene(new Prefab(new Platform(), new Vector3(random.Next(0, 10), random.Next(0, 10), -10), new Vector3(0, 0, 0), new Vector3(1, 1, 1)));
            }
            //AddPrefabsInScene(new Prefab(new Platform(), new Vector3(1, 1, -10), new Vector3(0, 0, 0), new Vector3(5, 4, 1)));
            //AddPrefabsInScene(new Prefab(new Platform(), new Vector3(5, 0, -10), new Vector3(0, 0, 0), new Vector3(2, 2, 1)));
            //AddPrefabsInScene(new Prefab(new Platform(), new Vector3(7, 3, -10), new Vector3(0, 0, 0), new Vector3(1, 5, 1)));
        }
    }
}
