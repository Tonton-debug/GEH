using GEH.Entities;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEH.ColliderElements
{
   public enum PlanePosition
    {
        Up,
        Down,
        Left,
        Right,
        Forward,
        Back,
        Null
    }
    public class Plane
    {
        public PlanePosition MyPlacePosition { get; private set; }
        public Vertex[] vertices { get; private set; }
        private bool[] _leftUpBackVertex;
        private bool[] _rightUpBackVertex;
        private bool[] _leftDownBackVertex;
        private bool[] _rightDownBackVertex;
        private bool[] _leftUpForwardVertex;
        private bool[] _rightUpForwardVertex;
        private bool[] _leftDownForwardVertex;
        private bool[] _rightDownForwardVertex;
        private Entity _myEntity;
       
        public Plane(PlanePosition planePosition, Entity entity)
        {
            InitializationVertex();
            MyPlacePosition = planePosition; 
            _myEntity = entity;
            UpdateVertexInPlane();
        }
        private void InitializationVertex()
        {
            _leftUpBackVertex = new bool[3] { false, true, false };
            _rightUpBackVertex = new bool[3] { true, true, false };
            _leftDownBackVertex = new bool[3] { false, false, false };
            _rightDownBackVertex = new bool[3] { true, false, false };
            _leftUpForwardVertex = new bool[3] { false, true, true };
            _rightUpForwardVertex = new bool[3] { true, true, true };
            _leftDownForwardVertex = new bool[3] { false, false, true };
            _rightDownForwardVertex = new bool[3] { true, false, true };
        }
        public void UpdateVertexInPlane()
        {
            vertices = new Vertex[4];
            switch (MyPlacePosition)
            {
                case PlanePosition.Up:
                    vertices[0] = new Vertex(_rightUpForwardVertex[0], _rightUpForwardVertex[1], _rightUpForwardVertex[2], _myEntity);
                    vertices[1] = new Vertex(_rightUpBackVertex[0], _rightUpBackVertex[1], _rightUpBackVertex[2], _myEntity);
                    vertices[2] = new Vertex(_leftUpBackVertex[0], _leftUpBackVertex[1], _leftUpBackVertex[2], _myEntity);
                    vertices[3] = new Vertex(_leftUpForwardVertex[0], _leftUpForwardVertex[1], _leftUpForwardVertex[2], _myEntity);
                    
                    break;
                case PlanePosition.Down:
                    vertices[0] = new Vertex(_rightDownForwardVertex[0], _rightDownForwardVertex[1], _rightDownForwardVertex[2], _myEntity);
                    vertices[1] = new Vertex(_rightDownBackVertex[0], _rightDownBackVertex[1], _rightDownBackVertex[2], _myEntity);
                    vertices[2] = new Vertex(_leftDownBackVertex[0], _leftDownBackVertex[1], _leftDownBackVertex[2], _myEntity);
                    vertices[3] = new Vertex(_leftDownForwardVertex[0], _leftDownForwardVertex[1], _leftDownForwardVertex[2], _myEntity);
                    break;
                case PlanePosition.Left:
                    vertices[0] = new Vertex(_leftUpForwardVertex[0], _leftUpForwardVertex[1], _leftUpForwardVertex[2], _myEntity);
                    vertices[1] = new Vertex(_leftUpBackVertex[0], _leftUpBackVertex[1], _leftUpBackVertex[2], _myEntity);
                    vertices[2] = new Vertex(_leftDownBackVertex[0], _leftDownBackVertex[1], _leftDownBackVertex[2], _myEntity);
                    vertices[3] = new Vertex(_leftDownForwardVertex[0], _leftDownForwardVertex[1], _leftDownForwardVertex[2], _myEntity);
                    break;
                case PlanePosition.Right:
                    vertices = new Vertex[4];
                    vertices[0] = new Vertex(_rightUpForwardVertex[0], _rightUpForwardVertex[1], _rightUpForwardVertex[2], _myEntity);
                    vertices[1] = new Vertex(_rightUpBackVertex[0], _rightUpBackVertex[1], _rightUpBackVertex[2], _myEntity);
                    vertices[2] = new Vertex(_rightDownBackVertex[0], _rightDownBackVertex[1], _rightDownBackVertex[2], _myEntity);
                    vertices[3] = new Vertex(_rightDownForwardVertex[0], _rightDownForwardVertex[1], _rightDownForwardVertex[2], _myEntity);
                    break;
                case PlanePosition.Forward:
                    vertices[0] = new Vertex(_leftUpForwardVertex[0], _leftUpForwardVertex[1], _leftUpForwardVertex[2], _myEntity);
                    vertices[1] = new Vertex(_leftDownForwardVertex[0], _leftDownForwardVertex[1], _leftDownForwardVertex[2], _myEntity);
                    vertices[2] = new Vertex(_rightDownForwardVertex[0], _rightDownForwardVertex[1], _rightDownForwardVertex[2], _myEntity);
                    vertices[3] = new Vertex(_rightUpForwardVertex[0], _rightUpForwardVertex[1], _rightUpForwardVertex[2], _myEntity);
                    break;
                case PlanePosition.Back:
                    vertices[0] = new Vertex(_leftUpBackVertex[0], _leftUpBackVertex[1], _leftUpBackVertex[2], _myEntity);
                    vertices[1] = new Vertex(_leftDownBackVertex[0], _leftDownBackVertex[1], _leftDownBackVertex[2], _myEntity);
                    vertices[2] = new Vertex(_rightDownBackVertex[0], _rightDownBackVertex[1], _rightDownBackVertex[2], _myEntity);
                    vertices[3] = new Vertex(_rightUpBackVertex[0], _rightUpBackVertex[1], _rightUpBackVertex[2], _myEntity);
                    break;
                default:
                    break;
            }
            Array.Sort(vertices, new VertexComparer());         
        }
        private bool VerticesCoordinatesQqual(float one,float two)
        {
            return one > two ? one - two <= 1 : two - one <= 1;
        }
        private bool HasVertex(Vertex vertex)
        {
            switch (MyPlacePosition)
            {
                case PlanePosition.Up:
                    return vertices[1].X <= vertex.X && vertices[0].X >= vertex.X && VerticesCoordinatesQqual(vertex.Y, vertices[0].Y) && vertices[1].Z >= vertex.Z && vertices[3].Z <= vertex.Z;
                case PlanePosition.Left:
                case PlanePosition.Right:
                    return VerticesCoordinatesQqual(vertex.X,vertices[0].X) && vertices[0].Z >= vertex.Z && vertices[2].Z <= vertex.Z && vertices[0].Y >= vertex.Y && vertices[1].Y <= vertex.Y;
                case PlanePosition.Forward:
                case PlanePosition.Back:
                    return VerticesCoordinatesQqual(vertex.Z, vertices[0].Z) && vertices[0].Y >= vertex.Y && vertices[2].Y <= vertex.Y && vertices[0].X >= vertex.X && vertices[1].X <= vertex.X;
                default:
                    return false;
                   
            }
        }
        public static bool operator !=(Plane mainPlane, Plane otherPlane)
        {
            if (mainPlane._myEntity.ScaleEntity.Length > otherPlane._myEntity.ScaleEntity.Length)
                return !mainPlane.HasVertex(otherPlane.vertices[0]) && !mainPlane.HasVertex(otherPlane.vertices[1]) && !mainPlane.HasVertex(otherPlane.vertices[2]) && !mainPlane.HasVertex(otherPlane.vertices[3]);
            return !otherPlane.HasVertex(mainPlane.vertices[0]) && !otherPlane.HasVertex(mainPlane.vertices[1]) && !otherPlane.HasVertex(mainPlane.vertices[2]) && !otherPlane.HasVertex(mainPlane.vertices[3]);
        }
        public static bool operator ==(Plane mainPlane, Plane otherPlane)
        {
            if(mainPlane._myEntity.ScaleEntity.Length>otherPlane._myEntity.ScaleEntity.Length)
                 return mainPlane.HasVertex(otherPlane.vertices[0]) || mainPlane.HasVertex(otherPlane.vertices[1]) || mainPlane.HasVertex(otherPlane.vertices[2]) || mainPlane.HasVertex(otherPlane.vertices[3]);
           return otherPlane.HasVertex(mainPlane.vertices[0]) || otherPlane.HasVertex(mainPlane.vertices[1]) || otherPlane.HasVertex(mainPlane.vertices[2]) || otherPlane.HasVertex(mainPlane.vertices[3]);
        }
}
}
