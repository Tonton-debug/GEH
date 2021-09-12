using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Threading;
namespace Game
{
    class Program
    {
     
        static void Main(string[] args)
        {


           
            using (Window window = new Window(0))
            {
                window.Run(60, 60);
            }
           


            //List<GameObject2> gameObject2 = new List<GameObject2>();
            //for (int i = 0; i < 500; i++)
            //{
            //    gameObject2.Add(new GameObject2());
            //}



            //foreach (var item in gameObject2)
            //{
            //    scene.AddEntityToScene(item);
            //}









        }
       
       
    }
}
