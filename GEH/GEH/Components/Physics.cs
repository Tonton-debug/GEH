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
        private const float G = -0.98f;
        private double _time = 0;
        private double _middleTime = 0.1;
        public Physics(Entity entity)
        {
            MainEntity = entity;
            MyStatePhysics = StatePhysics.Fall;
        }
        private void ResetChange(StatePhysics getStatePhysics)
        {
            if (getStatePhysics != MyStatePhysics)
            {
                _time = 0;
                MyStatePhysics = getStatePhysics;
                
              
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
            //speedXYZEntity = new Vector3((float)(speedXYZEntity.X * Math.Sin(45) + G * _time),
            //    (float)(speedXYZEntity.Y * Math.Sin(45) + G * _time),
            //    (float)(speedXYZEntity.Z * Math.Sin(45) + G * _time));
            speedXYZEntity.Y = speedXYZEntity.Y!=0?(float)(speedXYZEntity.Y * Math.Sin(45) + G * _time): speedXYZEntity.Y;
            //speedXYZEntity.X = speedXYZEntity.X!=0?(float)(speedXYZEntity.X * Math.Sin(45) + G * _time): speedXYZEntity.X;
            //speedXYZEntity.Z = speedXYZEntity.Z!=0?(float)(speedXYZEntity.X * Math.Sin(45) + G * _time): speedXYZEntity.Z;
            Vector3 endVector = new Vector3((float)(speedXYZEntity.X * Math.Cos(45) * _time),
                (float)(speedXYZEntity.Y * Math.Sin(45) * _time + (G * Math.Pow(_time, 2)) / 2),
                (float)(speedXYZEntity.Z * Math.Cos(45) * _time));
         //   endVector=new Vector3((float)Math.Round(endVector.X,2), (float)Math.Round(endVector.Y, 2), (float)Math.Round(endVector.Z, 2));
             _time += _middleTime;
            _time = Math.Round(_time, 1);
                MainEntity.Move(endVector);
            MainEntity.CollisionComponent.SetPlacesCollisionsWithOtherEntities();
            Console.WriteLine("Y:::"+Math.Round(endVector.Y,2));
            if (MainEntity.CollisionComponent.GetPlaneCollisionsFromDictionary().Count!=0&&_time>0.3)
            {  
                    MyStatePhysics = StatePhysics.Fall;
              
            }
        }
       
        public void Fall()
        {
            CheckIsEnabledComponent();
        
            if (!MainEntity.CollisionComponent.GetPlaneCollisionsFromDictionary().Contains(PlaneCollision.Down) && MyStatePhysics == StatePhysics.Fall)
                MainEntity.Move(-new Vector3(0, (float)Math.Round(-G, 1), 0)); 
        }
    }
}
