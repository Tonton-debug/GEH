using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameEngineHrundelGUI
{
    public partial class MainWindow : Window, ICommand
    {
        private List<GEH.Prefab> _allPrefabsInCurrentScene = new List<GEH.Prefab>();
        private Camera _mainCamera = new Camera();
        private Vector2 _lastPosMouse = Vector2.Zero;
        private float _lastValueWheel = 0;
        private bool _isFirstMoveMouse = true;
        private string path = "";
        private bool _activeWindow = false;
        public MainWindow()
        {
            InitializeComponent();
           
        }
        
        void ICommand.Exit()
        {
            Close();
        }
        private void VusialGlControl()
        {

        }
        private void glControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            GL.ClearColor(new Color4(0.631f, 0.6f, 0.227f, 1f));
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            if (path != "")
            {
                if (OpenTK.Input.Mouse.GetState()[OpenTK.Input.MouseButton.Left])
                {
                    MoveCamera(new Vector2(OpenTK.Input.Mouse.GetState().X, OpenTK.Input.Mouse.GetState().Y));
                }
                MoveCamera(OpenTK.Input.Mouse.GetState().ScrollWheelValue);
                foreach (var item in _allPrefabsInCurrentScene)
                {
                    GEH.Entities.Entity entity = item.Entity;
                    int VertexBufferObject;
                    int ColorBufferObject;
                    int VertexArrayObject;
                    GL.GenBuffers(1, out VertexBufferObject);
                    GL.GenBuffers(1, out ColorBufferObject);
                    GL.GenBuffers(1, out VertexArrayObject);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
                    GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(entity.GetVertex().Length * Vector3.SizeInBytes), entity.GetVertex(), BufferUsageHint.StaticDraw);
                    GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, ColorBufferObject);
                    GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(entity.GetColors().Length * Vector3.SizeInBytes), entity.GetColors(), BufferUsageHint.StaticDraw);
                    GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, true, 0, 0);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                    GL.BindBuffer(BufferTarget.ElementArrayBuffer, VertexArrayObject);
                    GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(entity.GetPoints().ToArray().Length * sizeof(int)), entity.GetPoints().ToArray(), BufferUsageHint.StaticDraw);
                    GL.EnableVertexAttribArray(0);
                    GL.EnableVertexAttribArray(1);
                   
                    Matrix4 result = _mainCamera.GetViewMatrix() * Matrix4.CreateScale(item.StartScale) * Matrix4.CreateRotationX(item.StartRotate.X) * Matrix4.CreateRotationZ(item.StartRotate.Z) * Matrix4.CreateRotationY(item.StartRotate.Y) * Matrix4.CreateTranslation(item.StartPosition);
                    entity.UpdateUniformInShader(result);
                    
                    GL.DrawElements(PrimitiveType.Triangles, entity.GetPoints().ToArray().Length, DrawElementsType.UnsignedInt, 0);
                    entity.DeleteAllByffers();
                }
            }
            glControl.SwapBuffers();
            if (_activeWindow)
            {
                glControl.Invalidate();
            }
           
        }


        void ICommand.RunDLL(string path)
        {
            this.path = path;
            Assembly assembly = Assembly.LoadFrom(path);
            Type[] types = assembly.GetTypes();
            Type scene = types.First((t) => t.BaseType.Name == "Scene");
            object sceneObj = Activator.CreateInstance(scene);
            MethodInfo methodInfo = scene.GetMethod("GetAllPrefabsInScene");
            // Draw objects here
           
            _allPrefabsInCurrentScene = (List<GEH.Prefab>)methodInfo.Invoke(sceneObj, new object[] { });
            //foreach (var item2 in )
            //{
            //    object obj = Activator.CreateInstance(item2);
            //    allEntitys.Add(obj);
            //}
        }

        private void WindowsFormsHost_Initialized(object sender, EventArgs e)
        {
            this.glControl.MakeCurrent();

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Console.Height = e.NewSize.Height - 13;
        }

        private void glControl_Load(object sender, EventArgs e)
        {
          
        }
        private void MoveCamera(float wheelValue)
        {
            float deltaWheel = wheelValue - _lastValueWheel;
            _mainCamera.Move(0,deltaWheel,0);
            _lastValueWheel = wheelValue;
            LogInfo.Text += deltaWheel;
        }
        private void MoveCamera(Vector2 position)
        {
            Vector2 positionMouse = new Vector2(-(position.X / 100), position.Y / 100);
            
            if (_isFirstMoveMouse)
            {
              
                _lastPosMouse = positionMouse;
                _isFirstMoveMouse = !_isFirstMoveMouse;
            }
            Vector2 deltaMouse = positionMouse - _lastPosMouse;
        
            _mainCamera.Move( deltaMouse.X,0, deltaMouse.Y);
            _lastPosMouse = positionMouse;
        }
        private void RotateCamera()
        {
            Vector2 positionMouse = new Vector2(OpenTK.Input.Mouse.GetState().X, OpenTK.Input.Mouse.GetState().Y);

            if (_isFirstMoveMouse)
            {

                _lastPosMouse = positionMouse;
                _isFirstMoveMouse = !_isFirstMoveMouse;
            }
            Vector2 deltaMouse = _lastPosMouse - positionMouse;

            _mainCamera.AddRotation(deltaMouse.X, deltaMouse.Y);
            _lastPosMouse = positionMouse;
        }
        private void glControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            
        }

        private void Console_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        void ICommand.RotateCamera(int angleX, int angleY)
        {
            _mainCamera.SetRotate((float)angleX, (float)angleY);
        }

        private void WindowsFormsHost_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void glControl_MouseEnter(object sender, EventArgs e)
        {
            _activeWindow = true;
           
            glControl.Invalidate();
        }

        private void glControl_MouseLeave(object sender, EventArgs e)
        {
            _activeWindow = false;
        }
    }
}
