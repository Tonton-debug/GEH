using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEH.Entities;
using GEH.ColliderElements;
namespace GEH.Components
{
   public enum PlaneCollision
    {
        Left,
        Right,
        Down,
        Up,
        Forward,
        Back

    }
  public  class Collision:Component
    {
        public readonly Plane PlaneUp;
        public readonly Plane PlaneDown;
        public readonly Plane PlaneLeft;
        public readonly Plane PlaneRight;
        public readonly Plane PlaneForward;
        public readonly Plane PlaneBack;
        private Dictionary<Entity, List<PlaneCollision>> CollisionsWithOtherEntities = default;
        private Stack<Plane> _planeInStuckEntities = new Stack<Plane>();
        public Collision(Entity entity)
        {
         MainEntity= entity;
            PlaneUp = new Plane(PlanePosition.Up, this);
            PlaneDown = new Plane(PlanePosition.Down, this);
            PlaneLeft = new Plane(PlanePosition.Left, this);
            PlaneRight = new Plane(PlanePosition.Right, this);
            PlaneForward = new Plane(PlanePosition.Forward, this);
            PlaneBack = new Plane(PlanePosition.Back, this);
            UpdatePlanes();
        }
       private List<Entity> GetEntitiesInRadius()
        {
           
            List<Entity> sendEntitys = new List<Entity>();
            //foreach (var item in Scene.entities)
            //{
            //    float distance = item.PositionEntity.Length > MainEntity.PositionEntity.Length ? item.PositionEntity.Length - MainEntity.PositionEntity.Length : MainEntity.PositionEntity.Length - item.PositionEntity.Length;
            //    float maxScale = item.ScaleEntity.Length > MainEntity.ScaleEntity.Length ? item.ScaleEntity.Length : MainEntity.ScaleEntity.Length;
            //    if (item != MainEntity&& item.CollisionComponent.IsEnabled&& distance<maxScale-1)
            //        sendEntitys.Add(item);
            //}
            return sendEntitys;
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
        public List<PlaneCollision> GetPlaneCollisionsFromDictionary()
        {
            List<PlaneCollision> endPlaneCollisions = new List<PlaneCollision>();
            foreach (var item in CollisionsWithOtherEntities.Values.ToList())
            {
                foreach (var item2 in item)
                {
                   
                        endPlaneCollisions.Add(item2);
                }
                
            }
            return endPlaneCollisions;
        }
        public List<Entity> GetEntitiesFromDictionary()
        {
            return CollisionsWithOtherEntities.Keys.ToList();
        }
        private PlaneCollision SetPositivePlaneCollsion(PlaneCollision get)
        {
            switch (get)
            {
                case PlaneCollision.Left:
                    return PlaneCollision.Right;
                case PlaneCollision.Right:
                    return PlaneCollision.Left;
                case PlaneCollision.Down:
                    return PlaneCollision.Up;
                case PlaneCollision.Forward:
                    return PlaneCollision.Back;
                case PlaneCollision.Back:
                    return PlaneCollision.Forward;
                default:
                    throw new Exception("HOW");
            }
        }
        public void AddCollisionPositionForEntityStuckInEntity(Plane plane)
        {
            _planeInStuckEntities.Push(plane);
        }
        private List<PlaneCollision> GetAllPlaneCollisions(Collision collision, bool mainPlane)
        {
            List<PlaneCollision> allPlaneCollisions = new List<PlaneCollision>();
            if (PlaneUp == collision.PlaneDown)
                allPlaneCollisions.Add(mainPlane ? PlaneCollision.Up : PlaneCollision.Down);
            if (PlaneDown == collision.PlaneUp)
                allPlaneCollisions.Add(mainPlane ? PlaneCollision.Down : PlaneCollision.Up);
            if (PlaneLeft == collision.PlaneRight)
                allPlaneCollisions.Add(mainPlane ? PlaneCollision.Left : PlaneCollision.Right);
            if (PlaneRight == collision.PlaneLeft)
                allPlaneCollisions.Add(mainPlane ? PlaneCollision.Right : PlaneCollision.Left);
            if (PlaneForward == collision.PlaneBack)
                allPlaneCollisions.Add(mainPlane ? PlaneCollision.Forward : PlaneCollision.Back);
            if (PlaneBack == collision.PlaneForward)
                allPlaneCollisions.Add(mainPlane ? PlaneCollision.Back : PlaneCollision.Forward);
            return allPlaneCollisions;
        }
        public void  SetPlacesCollisionsWithOtherEntities()
        {
         
            CheckIsEnabledComponent();
            UpdatePlanes();
            CollisionsWithOtherEntities = new Dictionary<Entity, List<PlaneCollision>>();
            List<Entity> entitiesInRadius = GetEntitiesInRadius();
            if(MainEntity.NameObject=="player")
            Console.WriteLine(entitiesInRadius.Count);
            foreach (var item in entitiesInRadius)
            {
                item.CollisionComponent.UpdatePlanes();
                List<PlaneCollision> allPlaneCollisions =item.ScaleEntity.Length>MainEntity.ScaleEntity.Length? item.CollisionComponent.GetAllPlaneCollisions(this, false): GetAllPlaneCollisions(item.CollisionComponent, true);

                List<PlaneCollision> allPlaneCollisionsNegative = item.ScaleEntity.Length > MainEntity.ScaleEntity.Length ? GetAllPlaneCollisions(item.CollisionComponent, false) : item.CollisionComponent.GetAllPlaneCollisions(this, true);
                allPlaneCollisionsNegative.RemoveAll((b) => b == PlaneCollision.Up);
                foreach (var item2 in allPlaneCollisionsNegative)
                {
                    allPlaneCollisions.Add(SetPositivePlaneCollsion(item2));
                }

                CollisionsWithOtherEntities.Add(item, allPlaneCollisions);
            }
            if (_planeInStuckEntities.Count != 0)
                PushTheEntityOutOfCollision();
        }
        private void PushTheEntityOutOfCollision()
        {
            while (_planeInStuckEntities.Count != 0)
            {
                Plane plane = _planeInStuckEntities.Pop();
                if(MainEntity.NameObject=="player")
                    Console.WriteLine("Q:"+plane.MyPlacePosition);

                Entity entity = plane.ParentCollisionComponent.MainEntity;
                Vector3 newPosition;
                float offset = 0.1f;
                switch (plane.MyPlacePosition)
                {
                    case PlanePosition.Up:
                        newPosition = new Vector3(MainEntity.PositionEntity.X, offset+entity.PositionEntity.Y + entity.ScaleEntity.Y / 2 + MainEntity.ScaleEntity.Y / 2, MainEntity.PositionEntity.Z);
                        MainEntity.SetPosition(newPosition);
                        break;
                    case PlanePosition.Down:
                      
                        break;
                    case PlanePosition.Left:
                        break;
                    case PlanePosition.Right:
                        break;
                    case PlanePosition.Forward:
                        break;
                    case PlanePosition.Back:
                        newPosition = new Vector3(MainEntity.PositionEntity.X, MainEntity.PositionEntity.Y, entity.PositionEntity.Z - entity.ScaleEntity.Y / 2 + MainEntity.ScaleEntity.Y / 2);
                        MainEntity.SetPosition(newPosition);
                        break;
                    case PlanePosition.Null:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
