using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEH.Entities;
using OpenTK;
using GEH.ColliderElements;
namespace GEH.Components
{
    public enum StatePhysics
    {
        Rest,
        Fall,
        MoveSpeed,
        Fly
    }
    public  class Physics:Component
    {
     
        public StatePhysics MyStatePhysics { get; private set; }
        private const float g = 1f;
        private double time = 0;
        private List<Vector3> lastPositions = new List<Vector3>();
        public Physics(Entity entity)
        {
            MainEntity = entity;
            MyStatePhysics = StatePhysics.Fall;
        }
        private void ResetChange(StatePhysics getStatePhysics)
        {
            if (getStatePhysics != MyStatePhysics)
            {
                time = 0;
                MyStatePhysics = getStatePhysics;
                
                lastPositions = new List<Vector3>();
            }
        }
        private bool IsCollision(Vector3 XYZForCheck,Vector3 moveVector)
        {
            bool chekX = XYZForCheck.X != 0;
            bool chekY = XYZForCheck.Y != 0;
            bool chekZ = XYZForCheck.Z != 0;
            bool isCollisionX = (((MainEntity.CollisionComponent.GetPlaneCollisionsFromDictionary().Contains(PlaneCollision.Left)&& moveVector.X<0) || (MainEntity.CollisionComponent.GetPlaneCollisionsFromDictionary().Contains(PlaneCollision.Right)&& moveVector.X>0))) && chekX;
            bool isCollisionY = (((MainEntity.CollisionComponent.GetPlaneCollisionsFromDictionary().Contains(PlaneCollision.Up)&&moveVector.Y>0) || (MainEntity.CollisionComponent.GetPlaneCollisionsFromDictionary().Contains(PlaneCollision.Down)&&moveVector.Y<0))) && chekY;
            bool isCollisionZ = (((MainEntity.CollisionComponent.GetPlaneCollisionsFromDictionary().Contains(PlaneCollision.Back)&&moveVector.Z>0) || (MainEntity.CollisionComponent.GetPlaneCollisionsFromDictionary().Contains(PlaneCollision.Forward))&&moveVector.Z<0)) && chekZ;
            return isCollisionX || isCollisionY || isCollisionZ;
        }
        public void AddMomentum(Vector3 speedXYZEntity)
        {  
            CheckIsEnabledComponent();
            ResetChange(StatePhysics.Fly);    
            Vector3 endVector = new Vector3((float)(speedXYZEntity.X * Math.Cos(1) * time), (float)((speedXYZEntity.Y * Math.Sin(1) * time) - (g * Math.Pow(time, 2) / 2)), (float)(speedXYZEntity.Z * Math.Cos(1) * time));
            lastPositions.Add(MainEntity.PositionEntity);
            time +=0.1;
            MainEntity.Move(endVector);
            MainEntity.CollisionComponent.SetPlacesCollisionsWithOtherEntities();
            if (IsCollision(speedXYZEntity,endVector))
            {  
                    MyStatePhysics = StatePhysics.Fall;
            }
        }
        public void Fall()
        {
            CheckIsEnabledComponent();
            //     ResetChange(StatePhysics.Fall);
            if (!MainEntity.CollisionComponent.GetPlaneCollisionsFromDictionary().Contains(PlaneCollision.Down) && MyStatePhysics == StatePhysics.Fall)
                MainEntity.Move(-new Vector3(0,(float)Math.Round(g,0), 0));
           
           
        }
    }
}
