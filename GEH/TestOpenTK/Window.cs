using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using GEH.Entities;
using GEH.Components;
using GEH;
namespace Game
{
    class Window:GameWindow
    {
        private Shader shader = new Shader(@"shader.vert", @"shader.frag");
        public static List<Entity> entities = new List<Entity>();
        public Window(int count ) : base(512, 512, new GraphicsMode(32, 24, 0, 4),"s",GameWindowFlags.Fullscreen,DisplayDevice.Default,0,0,GraphicsContextFlags.Default)
        {
        
            Load += Window_Load;
            UpdateFrame += Window_UpdateFrame;
            RenderFrame += Window_RenderFrame;
           
            FocusedChanged += Window_FocusedChanged;
          
        }

        private void Window_FocusedChanged(object sender, EventArgs e)
        {
           
        }

       

        
        
        private void Window_UpdateFrame(object sender, FrameEventArgs e)
        {
            if (Keyboard.GetState().IsKeyDown(Key.Escape))
            {
                Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Key.Q))
            {
                Scene.AddEntity(new Cube(true));
            }
            if (Keyboard.GetState().IsKeyDown(Key.E))
            {
                Scene.AddEntity(new Cube(false));
            }
            foreach (var item in Scene.entities)
            {        
                item.Update();
            }    
        }

        private void Window_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Viewport(0,0,Width,Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            foreach (var item in Scene.entities)
            {
             
                item.VusialEntity();
                shader.UpdateUniform(item.ModelMatrix * Matrix4.CreatePerspectiveFieldOfView(1.3f, (float)Width / (float)Height, 1.0f, 100.0f));
                shader.Use();
                GL.DrawElements(PrimitiveType.Triangles, item.GetPoints().ToArray().Length, DrawElementsType.UnsignedInt, 0);

                GL.DisableVertexAttribArray(0);
                GL.DisableVertexAttribArray(1);
            }
           
            SwapBuffers();
        }

        private void Window_Load(object sender, EventArgs e)
        {
            GL.PointSize(5f);
          //  CursorGrabbed = true;
           
            shader.LoadShaders();
          
            Scene.AddEntity(new MainCamera());
            Scene.AddEntity(new Player());
            Scene.AddEntity(new Platform());
         GL.ClearColor(0.2f, 0.6f, 1, 1);
            SwapBuffers();      
            CursorVisible = false;
        }
    }
}
