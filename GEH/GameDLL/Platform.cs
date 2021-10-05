using GEH.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
namespace GameDLL
{
    public class Platform:Entity
    {
        public Platform() : base("platform")
        {
            PositionEntity = new Vector3(0, -10, 0);
            ScaleEntity = new Vector3(1, 1, 1);
            CollisionComponent.IsEnabled = true;
        }
     
        public override Vector3[] GetColors()
        {
            return new Vector3[] {
                new Vector3(0.08f, 0.98f, 0.38f),
                new Vector3( 0.08f, 0.98f, 0.38f),
                new Vector3(0.28f, 0.98f, 0.38f),
                new Vector3( 0.58f, 0.98f, 0.38f),
                new Vector3( 0.18f, 0.58f, 0.78f),
                new Vector3( 0.68f, 0.98f, 0.98f),
                new Vector3( 0.88f, 0.18f, 0.18f),
                new Vector3( 0.58f, 0.28f, 0.48f)
            };
        }
    }
}
