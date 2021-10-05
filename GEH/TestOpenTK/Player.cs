using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEH.Components;
using GEH.Entities;
using GEH;
using OpenTK;
using OpenTK.Input;
using GEH.ColliderElements;
namespace Game
{
    class Player:Entity
    {
        private bool isJump = false;
        private float speed = 0.5f;
        private Vector3 moveVector3;
        public Player() : base("player")
        {
            PositionEntity = new Vector3(0, 3, 0);
            ScaleEntity = new Vector3(2, 2, 2);
            CollisionComponent.IsEnabled = true;
            PhysicsComponent.IsEnabled = true;

        }
        public void MovePlayer()
        {
            var key = Keyboard.GetState();
            moveVector3 = Vector3.Zero;
            if (key.IsKeyDown(Key.W))
            {
                moveVector3 = !CollisionComponent.GetPlaneCollisionsFromDictionary().Contains(PlaneCollision.Back) ? new Vector3(0, 0, -speed) : Vector3.Zero;
              
            }
            if (key.IsKeyDown(Key.A))
            {
                moveVector3 = !CollisionComponent.GetPlaneCollisionsFromDictionary().Contains(PlaneCollision.Left) ? new Vector3(-speed, 0, 0) : Vector3.Zero;
               
            }
            if (key.IsKeyDown(Key.S))
            {
                moveVector3 = !CollisionComponent.GetPlaneCollisionsFromDictionary().Contains(PlaneCollision.Forward) ? new Vector3(0, 0, speed) : Vector3.Zero;
             
            }
            if (key.IsKeyDown(Key.D))
            {
                moveVector3 = !CollisionComponent.GetPlaneCollisionsFromDictionary().Contains(PlaneCollision.Right) ? new Vector3(speed, 0, 0) : Vector3.Zero;
               
            }
            if (key.IsKeyDown(Key.Space))
            {
                if (!isJump)
                    isJump=true;
            }
            PositionEntity += moveVector3;
           
        }
        public override Vector3[] GetColors()
        {
        
            return new Vector3[] {
                new Vector3(1f, 1f, 1f),
                new Vector3( 1f, 1f, 1f),
                new Vector3( 1f, 1f, 1f),
                new Vector3( 1f, 1f, 1f),
                new Vector3( 1f, 1f, 1f),
                new Vector3( 1f, 1f, 1f),
                new Vector3( 1f, 1f, 1f),
                new Vector3( 1f, 1f, 1f)
            };
        }
        public override void Update()
        {
            base.Update();
            MovePlayer();
            if (isJump)
            {
                PhysicsComponent.AddMomentum(new Vector3(0, 200f, 0));
                if (PhysicsComponent.MyStatePhysics == StatePhysics.Fall)
                    isJump = false;
            }
        }
    }
}
