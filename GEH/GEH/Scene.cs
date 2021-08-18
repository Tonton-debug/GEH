using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Threading;
using System.IO;
namespace GEH
{
    public sealed class Scene
    {
        public string NameScene { get; private set; }
        public List<Entity> _entitiesInScene { get; private set; }
          public static GameWindow _mainWindow;
        private static Thread _mainThreadForWorkWindow;
      
        private static Scene _activeScene;
        private static List<Scene> _allSceneInProject = new List<Scene>();
        public delegate void GetWidthAndHeight(int height, int width);
        public static event GetWidthAndHeight GetWidthAndHeightEvent;
        public static void LoadScene<T>(T get)
        {
            if (get is string)
            {
                string name = get as string;
                _activeScene = _allSceneInProject.Find((b) => b.NameScene == name);

            } else if (get is int?)
            {
                int sceneId = (int)(get as int?);
                _activeScene = _allSceneInProject[sceneId];
            }
            else
            {
                throw new SceneException("Failed convert " + get.GetType() + " to scene");
            }

        }
        public static Scene FindScene<T>(T get)
        {
            if (get is string)
            {
                string name = get as string;
                return _allSceneInProject.Find((b) => b.NameScene == name);

            }
            else if (get is int?)
            {
                int sceneId = (int)(get as int?);
                return _allSceneInProject[sceneId];
            }
            else
            {
                throw new SceneException("Failed convert " + get.GetType() + " to scene");

            }

        }
        public static void DeleteScene<T>(T get)
        {
            if (get is string)
            {
                string name = get as string;
                Scene sceneForRemove = _allSceneInProject.Find((b) => b.NameScene == name);
                _allSceneInProject.Remove(sceneForRemove);
            }
            else if (get is int?)
            {
                int sceneId = (int)(get as int?);
                _allSceneInProject.RemoveAt(sceneId);
            }
            else
            {
                throw new SceneException("Failed delete scene");
            }

        }
        public static void CreateAndRunWindow(int width = 500, int height = 500, int tps = 30, int fps = 30)
        {
            GetWidthAndHeightEvent += Camera.GetWidthAndHeight;
            _mainThreadForWorkWindow = new Thread(() => { RunWindowInOtherThread(width, height, tps, fps); });
            _mainThreadForWorkWindow.Start();


        }
        private static void RunWindowInOtherThread(int width = 500, int height = 500, int tps = 60, int fps = 60)
        {
            _mainWindow = new GameWindow(width, height);
            _mainWindow.UpdateFrame += MainWindow_UpdateFrame;
            _mainWindow.RenderFrame += MainWindow_RenderFrame;
            _mainWindow.Resize += _mainWindow_Resize;
            _mainWindow.Run(tps, fps);


        }

        private static void _mainWindow_Resize(object sender, EventArgs e)
        {
            GetWidthAndHeightEvent.Invoke(_mainWindow.Height, _mainWindow.Width);
            GL.Viewport(0, 0, _mainWindow.Width, _mainWindow.Height);
        }

        private static void MainWindow_RenderFrame(object sender, FrameEventArgs e)
        {   
                GL.ClearColor(0.5f, 0.2f, 1, 1);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.Enable(EnableCap.DepthTest);
            foreach (var item in _activeScene._entitiesInScene)
            {
                
                item.VusialEntity();
                GL.DrawElements(BeginMode.Triangles, item.Points.Count, DrawElementsType.UnsignedInt, 0);
                GL.DisableVertexAttribArray(0);
                GL.DisableVertexAttribArray(1);
            }

            _mainWindow.SwapBuffers();
        }
      
        private static void MainWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
           

        }
       private static bool  ExitsSceneWithName(string nameScene)
        {
            return _allSceneInProject.Find((b) => b.NameScene == nameScene) is Scene;
        }
        public void AddEntityToScene(Entity get)
        {
          
            _entitiesInScene.Add(get);
        }
        public void DeleteEntityToScene(Entity get)
        {
            _entitiesInScene.Remove(get);
        }
        public Scene(string getNameScene)
        {
            NameScene = getNameScene;
            if (ExitsSceneWithName(getNameScene))
            {
                throw new SceneException("This scene already exist");
            }
            _allSceneInProject.Add(this);
            _entitiesInScene = new List<Entity>();

        }
        
      
    }
}
