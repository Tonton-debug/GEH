using OpenTK;
using System;

namespace GEH
{
   
  public   class Camera:Component
    {
        private  Vector3 _Position = Vector3.Zero;
        private  Vector3 _Orientation = new Vector3((float)Math.PI, 0f, 0f);
        private static int zNear=1;
        public static Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), 1, 0.1f, 100.0f);
        public  float MoveSpeed { get; set; }
        public  float MouseSensitivity { get; set; }
        public static Camera Main { get; private set; }
      
        public static void GetWidthAndHeight(int height,int width)
        {
            zNear = width / height;
        }
        public  Matrix4 GetViewMatrix()
        {
          
            
             Vector3 lookat = new Vector3();

            lookat.X = (float)(Math.Sin((float)_Orientation.X) * Math.Cos((float)_Orientation.Y));
            lookat.Y = (float)Math.Sin((float)_Orientation.Y);
            lookat.Z = (float)(Math.Cos((float)_Orientation.X) * Math.Cos((float)_Orientation.Y));

            return Matrix4.LookAt(_Position, _Position + lookat, Vector3.UnitY);
        }
        public void SetActiveCurrentCamera()
        {
            Main = this;
        }
     
        public  void Move(float x, float y, float z)
        {
          

            Vector3 offset = new Vector3();

            Vector3 forward = new Vector3((float)Math.Sin((float)_Orientation.X), 0, (float)Math.Cos((float)_Orientation.X));
            Vector3 right = new Vector3(-forward.Z, 0, forward.X);

            offset += x * right;
            offset += y * forward;
            offset.Y += z;

            offset.NormalizeFast();
            offset = Vector3.Multiply(offset, MoveSpeed);

            _Position += offset;
        }

      
        public  void AddRotation(float x, float y)
        { 
         
            x = x * MouseSensitivity;
            y = y * MouseSensitivity;

            _Orientation.X = (_Orientation.X + x) % ((float)Math.PI * 2.0f);
            _Orientation.Y = Math.Max(Math.Min(_Orientation.Y + y, (float)Math.PI / 2.0f - 0.1f), (float)-Math.PI / 2.0f + 0.1f);
        }
    }
}
