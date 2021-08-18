using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Reflection;
namespace GEH
{
    enum Transformations
    {
        Vector3Colors,
        Vector3Vertex,
        Vector3Points
    }
    public class Entity
    {
        private List<Component> componentsOnEntity = new List<Component>();
      
        private string _PathForLoadVertex;
        private string _PathForLoadColors;
        public Vector3[] Colors { get; private set; }
        public Vector3[] Vertex { get; private set; }
        public List<int> Points { get; private set; }
        public Entity(string _getVertex, string _getPoints, string _getColors, string pathVertex, string pathColors)
        {
            ConvertStringToVector(_getPoints, Transformations.Vector3Points);
            ConvertStringToVector(_getVertex, Transformations.Vector3Vertex);
            ConvertStringToVector(_getColors, Transformations.Vector3Colors);
            _PathForLoadColors = pathColors;
            _PathForLoadVertex = pathVertex;
        }

        private void ConvertStringToVector(string getString, Transformations whatToConvert)
        {
            getString = getString.Replace(" ", "");
            string[] vectors = getString.Split(';');


            List<Vector3> vectorForSend = new List<Vector3>();
            foreach (string get in vectors)
            {
                double[] theeValues = new double[3];
                if (!double.TryParse(get.Split('#')[0], out theeValues[0]) || !double.TryParse(get.Split('#')[1], out theeValues[1]) || !double.TryParse(get.Split('#')[2], out theeValues[2]))
                {

                    throw new EntityException("Failed convert string to " + whatToConvert);
                }
                vectorForSend.Add(new Vector3((float)theeValues[0], (float)theeValues[1], (float)theeValues[2]));
            }
            Colors = whatToConvert == Transformations.Vector3Colors ? vectorForSend.ToArray() : Colors;
            Vertex = whatToConvert == Transformations.Vector3Vertex ? vectorForSend.ToArray() : Vertex;
            if (whatToConvert == Transformations.Vector3Points)
            {
                Points = new List<int>();
                foreach (var item in vectorForSend)
                {
                    Points.Add((int)item[0]);
                    Points.Add((int)item[1]);
                    Points.Add((int)item[2]);
                }
            }
        }

        public void VusialEntity()
        {
            int VertexBufferObject;
            int ColorBufferObject;
            int VertexArrayObject;
            Shader shader = GetComponent(new Shader()) as Shader;
            if (shader!=null)
            {

                if (!shader.ShaderIsLoad)
                {
                    shader.LoadShaders(_PathForLoadVertex, _PathForLoadColors);
                
                }
             
                GL.GenBuffers(1, out VertexBufferObject);
                GL.GenBuffers(1, out ColorBufferObject);
                GL.GenBuffers(1, out VertexArrayObject);
                GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(Vertex.Length * Vector3.SizeInBytes), Vertex, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

                GL.BindBuffer(BufferTarget.ArrayBuffer, ColorBufferObject);
                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(Colors.Length * Vector3.SizeInBytes), Colors, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, true, 0, 0);
             
                
              
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, VertexArrayObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, Points.ToArray().Length * sizeof(int), Points.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(0);
                GL.EnableVertexAttribArray(1);
                shader.Use();

            }
        }
        public Component GetComponent<T>(T component) where T : Component
        {
            return componentsOnEntity.Find((b) => b.GetType() == component.GetType());

        }
            public void RemoveComponent<T>(T component) where T : Component
        {
            Component findComponent = componentsOnEntity.Find((b) => b.GetType() == component.GetType());
            if (findComponent != null)
            {
                componentsOnEntity.Remove(findComponent);
            }
            else
            {
                throw new EntityException("Component "+component.GetType().Name+" not found");
            }
        }
        public void AddComponent<T>(T component) where T:Component
        {
            if (componentsOnEntity.Find((b)=>b.GetType()==component.GetType())!=null)
            {
                throw new EntityException("This component already exit");
            }
            componentsOnEntity.Add(component);
          

        }
    }
}
