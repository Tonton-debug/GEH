using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Reflection;
using System.Threading;
using System.Collections;
using GEH.Components;
namespace GEH.Entities
{
   

    public abstract class Entity
    {
        public Vector3 PositionEntity { get; protected set; }
        public Vector3 RotateEntity { get; protected set; }
        public Vector3 ScaleEntity { get; protected set; }
        public Matrix4 ModelMatrix { get; private set; }
        public Collision CollisionComponent { get; private set; }
        public Physics PhysicsComponent { get; private set; }
        public Camera CameraComponent { get; private set; }
        private int VertexBufferObject;
        private int ColorBufferObject;
        private int VertexArrayObject;
        public string NameObject { get; private set; }
        public bool isLoad = false;
     
        public Entity(string name)
        {
            NameObject = name;
            PhysicsComponent = new Physics(this);
            CollisionComponent = new Collision(this);
            CameraComponent = new Camera();
        }
        internal void SetPosition(Vector3 vector3)
        {
            PositionEntity = vector3;
        }
       public void Move(Vector3 moveVector)
        {
            PositionEntity += moveVector;
        }
        public virtual void Update()
        {
            if (CollisionComponent.IsEnabled)
            {
                CollisionComponent.SetPlacesCollisionsWithOtherEntities();
            }
            if (PhysicsComponent.IsEnabled)
            {
               PhysicsComponent.Fall();
            }
            ModelMatrix = Matrix4.CreateScale(ScaleEntity) * Matrix4.CreateRotationX(RotateEntity.X) * Matrix4.CreateRotationY(RotateEntity.Y) * Matrix4.CreateRotationZ(RotateEntity.Z) * Matrix4.CreateTranslation(PositionEntity)*Camera.Main.GetViewMatrix(); 
        }
        
        public virtual int[] GetPoints()
        {
            return new int[] {
                //left
                0, 2, 1,
                0, 3, 2,
                //back
                1, 2, 6,
                6, 5, 1,
                //right
                4, 5, 6,
                6, 7, 4,
                //top
                2, 3, 6,
                6, 3, 7,
                //front
                0, 7, 3,
                0, 4, 7,
                //bottom
                0, 1, 5,
                0, 5, 4
            };
        }
        public virtual Vector3[] GetVertex()
        {
            return new Vector3[] {new Vector3(-0.5f, -0.5f,  -0.5f),
                new Vector3(0.5f, -0.5f,  -0.5f),
                new Vector3(0.5f, 0.5f,  -0.5f),
                new Vector3(-0.5f, 0.5f,  -0.5f),
                new Vector3(-0.5f, -0.5f,  0.5f),
                new Vector3(0.5f, -0.5f,  0.5f),
                new Vector3(0.5f, 0.5f,  0.5f),
                new Vector3(-0.5f, 0.5f,  0.5f),
            };
        }
        public virtual Vector3[] GetColors()
        {
            return new Vector3[] {
                new Vector3(1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f),
                new Vector3( 0f, 1f, 0f),
                new Vector3( 1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f),
                new Vector3( 0f, 1f, 0f),
                new Vector3( 1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f)
            };
        }
       public virtual void VusialEntity()
        {
            if (!isLoad)
            {
                GL.GenBuffers(1, out VertexBufferObject);
                GL.GenBuffers(1, out ColorBufferObject);
                GL.GenBuffers(1, out VertexArrayObject);
                GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(GetVertex().Length * Vector3.SizeInBytes), GetVertex(), BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, ColorBufferObject);
                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(GetColors().Length * Vector3.SizeInBytes), GetColors(), BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, true, 0, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, VertexArrayObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, GetPoints().ToArray().Length * sizeof(int), GetPoints().ToArray(), BufferUsageHint.StaticDraw);
                isLoad = true;
            }
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);   
        } 
    }
}


