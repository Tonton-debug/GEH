using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEH.Entities;
using OpenTK;
namespace Game
{
    class Cube:Entity
    {
        public Cube(bool isPhysic) : base("cube")
        {
            Random random = new Random();
            ScaleEntity = new Vector3(3, 3, 3);
            PositionEntity = new Vector3(random.Next(-10, 10), isPhysic?0:-6, random.Next(-10, 10));
            CollisionComponent.IsEnabled = true;
            PhysicsComponent.IsEnabled = isPhysic;
        }
    }
}
