using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Threading;
using GEH;
namespace TestOpenTK
{
    class Program
    {
     
        static void Main(string[] args)
        {
         
           
         
          
            Scene scene = new Scene("1");
           
            List<GameObject2> games = new List<GameObject2>();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(i+"/"+ 5);
                GameObject2 gameObject2 = new GameObject2();
                games.Add(gameObject2);
             //   Shader shader = games.Last().GetComponent(new Shader()) as Shader;
              
            }
            foreach (var item in games)
            {
                scene.AddEntityToScene(item);
            }
            Scene.LoadScene(0);


            Scene.CreateAndRunWindow(1000, 1000);
           
          
          


        }
       
       
    }
}
