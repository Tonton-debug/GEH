using GEH.Components;
using GEH.Entities;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS0660 // Тип определяет оператор == или оператор !=, но не переопределяет Object.Equals(object o)
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
        public readonly Collision ParentCollisionComponent;
        private bool[] _leftUpBackVertex;
        private bool[] _rightUpBackVertex;
        private bool[] _leftDownBackVertex;
        private bool[] _rightDownBackVertex;
        private bool[] _leftUpForwardVertex;
        private bool[] _rightUpForwardVertex;
        private bool[] _leftDownForwardVertex;
        private bool[] _rightDownForwardVertex;
        private Vertex[] vertices = new Vertex[4];
        private bool _vertexIsCreated;
        private Dictionary<string,Vertex> _verticesDictionary = new Dictionary<string, Vertex>();

        public Plane(PlanePosition planePosition,Collision get)
        {
            InitializationVertex();
            MyPlacePosition = planePosition;
            ParentCollisionComponent = get;
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
        private void SetVertex(bool[] one,bool[] two,bool[] three,bool[] four)
        {
            
            vertices[0] = new Vertex(one[0], one[1], one[2],ParentCollisionComponent.MainEntity);
            vertices[1] = new Vertex(two[0], two[1], two[2],ParentCollisionComponent.MainEntity);
            vertices[2] = new Vertex(three[0], three[1], three[2],ParentCollisionComponent.MainEntity);
            vertices[3] = new Vertex(four[0], four[1], four[2],ParentCollisionComponent.MainEntity);
            if(MyPlacePosition!=PlanePosition.Left&& MyPlacePosition != PlanePosition.Right)
              Array.Sort(vertices, new VertexComparer());
            if (!_vertexIsCreated)
            {
                _verticesDictionary.Add("UpRight", vertices[0]);
                _verticesDictionary.Add("UpLeft", vertices[1]);
                _verticesDictionary.Add("DownRight", vertices[2]);
                _verticesDictionary.Add("DownLeft", vertices[3]);
                _vertexIsCreated = true;
            }
            else
            {
                _verticesDictionary["UpRight"]=vertices[0];
                _verticesDictionary["UpLeft"] = vertices[1];
                _verticesDictionary["DownRight"] = vertices[2];
                _verticesDictionary["DownLeft"] = vertices[3];
            }
          
        }
        public void UpdateVertexInPlane()
        {
           
            switch (MyPlacePosition)
            {
                case PlanePosition.Up:
                    SetVertex(_rightUpForwardVertex, _rightUpBackVertex, _leftUpBackVertex, _leftUpForwardVertex);
                    break;
                case PlanePosition.Down:
                    SetVertex(_rightDownForwardVertex, _rightDownBackVertex, _leftDownBackVertex, _leftDownForwardVertex);
                    break;
                case PlanePosition.Left:
                    SetVertex(_leftUpForwardVertex, _leftUpBackVertex, _leftDownBackVertex, _leftDownForwardVertex);
                    break;
                case PlanePosition.Right:
                    SetVertex(_rightUpForwardVertex, _rightUpBackVertex, _rightDownBackVertex, _rightDownForwardVertex);
                    break;
                case PlanePosition.Forward:
                    SetVertex(_leftUpForwardVertex, _leftDownForwardVertex, _rightDownForwardVertex, _rightUpForwardVertex);
                    break;
                case PlanePosition.Back:
                    SetVertex(_leftUpBackVertex, _leftDownBackVertex, _rightDownBackVertex, _rightUpBackVertex);
                    break;
                default:
                    break;
            }
        }
        private bool VertexIsBetweenThePlanes(Vertex vertex)
        {
            float offset = 0.1f;
            switch (MyPlacePosition)
            {
                case PlanePosition.Up:
                    return _verticesDictionary["DownRight"].Y+ offset >= vertex.Y && ParentCollisionComponent.PlaneDown._verticesDictionary["UpRight"].Y <=vertex.Y;
                case PlanePosition.Down:
                    return _verticesDictionary["UpRight"].Y <=vertex.Y && ParentCollisionComponent.PlaneUp._verticesDictionary["DownRight"].Y + offset >= vertex.Y;
                case PlanePosition.Left:
                    return _verticesDictionary["DownRight"].X <= vertex.X && ParentCollisionComponent.PlaneRight._verticesDictionary["DownRight"].X + offset >= vertex.X;
                case PlanePosition.Right:
                    return _verticesDictionary["DownRight"].X + offset >= vertex.X && ParentCollisionComponent.PlaneUp._verticesDictionary["DownRight"].X <= vertex.X;
                case PlanePosition.Forward:
                    return _verticesDictionary["DownRight"].Z >= vertex.Z && ParentCollisionComponent.PlaneBack._verticesDictionary["DownRight"].Z +offset <= vertex.Z;
                case PlanePosition.Back:
                    return _verticesDictionary["DownRight"].Z+ offset  <= vertex.Z && ParentCollisionComponent.PlaneForward._verticesDictionary["DownRight"].Z  >= vertex.Z;
               
                default:
                    break;
            }
            return false;
        }
        private bool PlaneContainVertex(Plane planeForGetsVertex)
        {
            
            List<bool> allBools = new List<bool>();
            bool planeContainVertex=false;
            float offset = 0.1f;
            foreach (var item in planeForGetsVertex._verticesDictionary.Values)
            {
                switch (MyPlacePosition)
                {
                    case PlanePosition.Up:
                    case PlanePosition.Down:
                        planeContainVertex=(_verticesDictionary["UpLeft"].X <= item.X && item.X <= _verticesDictionary["UpRight"].X) &&
                            (_verticesDictionary["DownLeft"].Z <= item.Z && item.Z <= _verticesDictionary["UpRight"].Z);
                        if (planeContainVertex && _verticesDictionary["DownRight"].Y+ offset != item.Y)
                            planeForGetsVertex.ParentCollisionComponent.MainEntity.CollisionComponent.AddCollisionPositionForEntityStuckInEntity(this);
                        break;
                    case PlanePosition.Left:
                    case PlanePosition.Right:
                        planeContainVertex=(_verticesDictionary["UpLeft"].Z <= item.Z && item.Z <= _verticesDictionary["UpRight"].Z) &&
                           (_verticesDictionary["DownRight"].Y <= item.Y && item.Y <= _verticesDictionary["UpLeft"].Y);
                        if (planeContainVertex && _verticesDictionary["DownRight"].X+ offset != item.X)
                            planeForGetsVertex.ParentCollisionComponent.MainEntity.CollisionComponent.AddCollisionPositionForEntityStuckInEntity(this);
                        break;
                    case PlanePosition.Forward:
                    case PlanePosition.Back:
                        planeContainVertex=(_verticesDictionary["UpLeft"].X <= item.X && item.X <= _verticesDictionary["UpRight"].X) &&
                         (_verticesDictionary["DownLeft"].Y <= item.Y && item.Y <= _verticesDictionary["UpLeft"].Y);
                        if (planeContainVertex && _verticesDictionary["DownRight"].Z+ offset != item.Z)
                            planeForGetsVertex.ParentCollisionComponent.MainEntity.CollisionComponent.AddCollisionPositionForEntityStuckInEntity(this);
                        break;
                }
                allBools.Add(planeContainVertex);
            }
            return allBools.Contains(true);
        }
        public static bool operator !=(Plane mainPlane, Plane otherPlane)
        {
                switch (mainPlane.MyPlacePosition)
                {
                    case PlanePosition.Up:
                    case PlanePosition.Down:
                    return !mainPlane.VertexIsBetweenThePlanes(otherPlane._verticesDictionary["UpLeft"]) && !mainPlane.PlaneContainVertex(otherPlane);
                case PlanePosition.Left:
                    case PlanePosition.Right:
                        return !mainPlane.VertexIsBetweenThePlanes(otherPlane._verticesDictionary["UpLeft"]) && ! mainPlane.PlaneContainVertex(otherPlane);
                    case PlanePosition.Forward:
                    case PlanePosition.Back:
                        return !mainPlane.VertexIsBetweenThePlanes(otherPlane._verticesDictionary["UpLeft"]) && ! mainPlane.PlaneContainVertex(otherPlane);
                }
            return true;
        }
        public static bool operator ==(Plane mainPlane, Plane otherPlane)
        {  
                switch (mainPlane.MyPlacePosition)
                {
                    case PlanePosition.Up:
                    case PlanePosition.Down:
                        return mainPlane.VertexIsBetweenThePlanes(otherPlane._verticesDictionary["UpLeft"]) && mainPlane.PlaneContainVertex(otherPlane);
                 case PlanePosition.Left:
                    case PlanePosition.Right:
                        return mainPlane.VertexIsBetweenThePlanes(otherPlane._verticesDictionary["UpLeft"]) && mainPlane.PlaneContainVertex(otherPlane);
                    case PlanePosition.Forward:
                    case PlanePosition.Back:
                        return mainPlane.VertexIsBetweenThePlanes(otherPlane._verticesDictionary["UpLeft"])&& mainPlane.PlaneContainVertex(otherPlane);
                }
            return false;
        }
    }
}


