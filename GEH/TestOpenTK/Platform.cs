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
            ScaleEntity = new Vector3(10, 10, 10);
            CollisionComponent.IsEnabled = true;
        }
        public override Vector3[] GetColors()
        {

            return new Vector3[] {
                new Vector3(0.08f, 0.98f, 0.38f),
                new Vector3( 0.08f, 0.98f, 0.38f),
                new Vector3(0.08f, 0.98f, 0.38f),
                new Vector3( 0.08f, 0.98f, 0.38f),
                new Vector3( 0.08f, 0.98f, 0.38f),
                new Vector3( 0.08f, 0.98f, 0.38f),
                new Vector3( 0.08f, 0.98f, 0.38f),
                new Vector3( 0.08f, 0.98f, 0.38f)
            };
        }
        public override Vector3[] GetVertex()
        {
            Random random = new Random();
            return new Vector3[] {new Vector3(-(float)random.NextDouble(), -(float)random.NextDouble(),  -(float)random.NextDouble()),
                new Vector3((float)random.NextDouble(), -(float)random.NextDouble(),  -(float)random.NextDouble()),
                new Vector3((float)random.NextDouble(), (float)random.NextDouble(),  -(float)random.NextDouble()),
                new Vector3(-(float)random.NextDouble(), (float)random.NextDouble(),  -(float)random.NextDouble()),
                new Vector3(-(float)random.NextDouble(), -(float)random.NextDouble(),  (float)random.NextDouble()),
                new Vector3((float)random.NextDouble(), -(float)random.NextDouble(),  (float)random.NextDouble()),
                new Vector3((float)random.NextDouble(), (float)random.NextDouble(),  (float)random.NextDouble()),
                new Vector3(-(float)random.NextDouble(), (float)random.NextDouble(),  (float)random.NextDouble()),
            };
        }
    }
}
