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
        public Player() : base("player")
        {
            PositionEntity = new Vector3(0, 0, -10);
            ScaleEntity = new Vector3(2, 2, 2);
            CollisionComponent.IsEnabled = true;
            PhysicsComponent.IsEnabled = true;

        }
        public void MovePlayer()
        {
            var key = Keyboard.GetState();
            if (key.IsKeyDown(Key.W))
            {
                PositionEntity +=!CollisionComponent.GetPlaneCollisionsFromDictionary().Contains(PlaneCollision.Back)?new Vector3(0, 0, -speed) :Vector3.Zero;
            }
            if (key.IsKeyDown(Key.A))
            {
                PositionEntity += !CollisionComponent.GetPlaneCollisionsFromDictionary().Contains(PlaneCollision.Left) ? new Vector3(-speed, 0, 0):Vector3.Zero;
            }
            if (key.IsKeyDown(Key.S))
            {
                PositionEntity += !CollisionComponent.GetPlaneCollisionsFromDictionary().Contains(PlaneCollision.Forward) ? new Vector3(0, 0, speed) :Vector3.Zero;
            }
            if (key.IsKeyDown(Key.D))
            {
                PositionEntity += !CollisionComponent.GetPlaneCollisionsFromDictionary().Contains(PlaneCollision.Right) ? new Vector3(speed, 0, 0):Vector3.Zero;
            }
            if (key.IsKeyDown(Key.Space))
            {
                if (!isJump)
                    isJump=true;
            }
        }
        public override void Update()
        {
            base.Update();
            MovePlayer();
            if (isJump)
            {
                PhysicsComponent.AddMomentum(new Vector3(0, 1.3f, 0));
                if (PhysicsComponent.MyStatePhysics == StatePhysics.Fall)
                    isJump = false;
            }
        }
    }
}
