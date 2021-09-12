using GEH.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
namespace Game
{
    class Platform:Entity
    {
        public Platform() : base("platform")
        {
            PositionEntity = new Vector3(0, -10, 0);
            ScaleEntity = new Vector3(30, 1, 30);
            CollisionComponent.IsEnabled = true;
        }
    }
}
