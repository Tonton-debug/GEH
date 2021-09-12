using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEH.Entities;
using GEH.Components;
namespace GEH.ColliderElements
{
    public enum PlaneCollision
    {
        Left,
        Right,
        Up,
        Down,
        Forward,
        Back,
    }
    public  class PlaneManager
    {
        public readonly Plane PlaneUp;
        public readonly Plane PlaneDown;
        public readonly Plane PlaneLeft;
        public readonly Plane PlaneRight;
        public readonly Plane PlaneForward;
        public readonly Plane PlaneBack;
        private Entity _myEntity;
        public PlaneManager(Entity get)
        {
            _myEntity = get;
            PlaneUp = new Plane(PlanePosition.Up, _myEntity);
            PlaneDown = new Plane(PlanePosition.Down, _myEntity);
            PlaneLeft = new Plane(PlanePosition.Left, _myEntity);
            PlaneRight = new Plane(PlanePosition.Right, _myEntity);
            PlaneForward = new Plane(PlanePosition.Forward, _myEntity);
            PlaneBack = new Plane(PlanePosition.Back, _myEntity);
        }
        public void UpdatePlanes()
        {
            PlaneUp.UpdateVertexInPlane();
            PlaneDown.UpdateVertexInPlane();
            PlaneLeft.UpdateVertexInPlane();
            PlaneRight.UpdateVertexInPlane();
            PlaneForward.UpdateVertexInPlane();
            PlaneBack.UpdateVertexInPlane();
          
        }
        public List<PlaneCollision> GetAllPlaneCollisions(PlaneManager placeManager)
        {
            List<PlaneCollision> allPlaneCollisions = new List<PlaneCollision>();
            if (PlaneUp == placeManager.PlaneDown)
                allPlaneCollisions.Add(PlaneCollision.Down);
            if (PlaneDown == placeManager.PlaneUp)
                allPlaneCollisions.Add(PlaneCollision.Up);
            if (PlaneLeft == placeManager.PlaneRight)
                allPlaneCollisions.Add(PlaneCollision.Right);
            if (PlaneRight == placeManager.PlaneLeft)
                allPlaneCollisions.Add(PlaneCollision.Left);
            if (PlaneForward == placeManager.PlaneBack)
                allPlaneCollisions.Add(PlaneCollision.Back);
            if (PlaneBack == placeManager.PlaneForward)
                allPlaneCollisions.Add(PlaneCollision.Forward);
            return allPlaneCollisions;



        }
    }
}
