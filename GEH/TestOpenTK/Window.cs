﻿using System;
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
      
        public static List<Entity> entities = new List<Entity>();
        public Window(GameWindowFlags flag) : base(512, 512, new GraphicsMode(32, 24, 0, 4),"s", flag, DisplayDevice.Default,0,0,GraphicsContextFlags.Default)
        {
        
            Load += Window_Load;
            UpdateFrame += Window_UpdateFrame;
            RenderFrame += Window_RenderFrame;
            Closing += Window_Closing;   
            FocusedChanged += Window_FocusedChanged;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
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
                
                Thread.Sleep(100);
            }
            if (Keyboard.GetState().IsKeyDown(Key.E))
            {
               
                Thread.Sleep(100);
            }
            
        }

        private void Window_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Viewport(0,0,Width,Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            //foreach (var item in Scene.entities)
            //{
             
            //    item.VusialEntity();
               

            //}
           
            SwapBuffers();
        }

        private void Window_Load(object sender, EventArgs e)
        {
            GL.PointSize(5f);
          //  CursorGrabbed = true;
           
            
          
           
         GL.ClearColor(0.2f, 0.6f, 1, 1);
            SwapBuffers();      
            CursorVisible = false;
        }
    }
}
