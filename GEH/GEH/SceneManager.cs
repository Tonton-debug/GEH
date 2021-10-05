using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEH
{
  public static class SceneManager
    {
        private static Scene ActiveScene;
        public static void LoadScene(Scene scene)
        {
            ActiveScene = scene;
        }
        public static Scene GetActiveScene()
        {
            return ActiveScene;
        }
    }
}
