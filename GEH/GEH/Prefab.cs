using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEH.Entities;
using OpenTK;

namespace GEH
{
  public sealed  class Prefab
    {
        public readonly Entity Entity;
        public readonly Vector3 StartPosition;
        public readonly Vector3 StartRotate;
        public readonly Vector3 StartScale;
        public Prefab(Entity entity, Vector3 getPosition, Vector3 getRotate, Vector3 getScale)
        {
            Entity = entity;
            StartPosition = getPosition;
            StartRotate = getRotate;
            StartScale = getScale;
        }
    }
}
