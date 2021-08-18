using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEH;
using OpenTK;
using OpenTK.Graphics.OpenGL;
namespace TestOpenTK
{
    class GameObject2 : Entity
    {
        public GameObject2() : base("" +
            "0,7    #  0,7   #0;" +
            "0,7    #-0,7    #0;" +
            "-0,7   #-0,7    #0;" +
            " - 0,7  #0,7    #0",
            "0#1#3;1#2#3",
            "1    #  0,5   #1;" +
            "0,5    #0,2    #1;" +
            "0,2   #0,5    #1;" +
            "0,2  #0,5    #1",
            @"C:\shader.vert", @"C:\shader.frag")
        {
            Shader shader = new Shader();
            AddComponent(shader);
     
            Random random = new Random();
            Matrix4 Scale = Matrix4.CreateScale(random.Next(1, 5), random.Next(1, 5), random.Next(1, 5));
         Matrix4  RotateZ= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(random.Next(0, 90)));
            Matrix4 TrasformMatrix = Matrix4.CreateTranslation(0, 0, random.Next(-50, -10));
            shader.AddUniform("all", Scale*TrasformMatrix *Camera.projection* RotateZ);
        }

       

    }
}
