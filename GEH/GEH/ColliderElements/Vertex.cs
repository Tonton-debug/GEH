using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEH.Entities;
using OpenTK;

namespace GEH.ColliderElements
{
    
  public  struct Vertex
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Z { get; private set; }
        public Vertex(bool positiveOffsetX, bool positiveOffsetY, bool positiveOffsetZ, Entity entityForWork)
        {
            Vector3 startPosition = entityForWork.PositionEntity;
            Vector3 startScale = entityForWork.ScaleEntity;

            X = startPosition.X + (positiveOffsetX ? (startScale.X / 2) : -(startScale.X / 2));
            Y = startPosition.Y + (positiveOffsetY ? (startScale.Y / 2) : -(startScale.Y / 2));
            Z = startPosition.Z + (positiveOffsetZ ? (startScale.Z / 2) : -(startScale.Z / 2));
            Round(1);
        }
        private void Round(int decimals)
        {
            X = (float)Math.Round(X,decimals);
            Y = (float)Math.Round(Y, decimals);
            Z = (float)Math.Round(Z, decimals);
        }
    }
    public class VertexComparer : IComparer<Vertex>
    {
        public int Compare(Vertex vertex1, Vertex vertex2)
        {
            if (vertex1.Z == vertex2.Z)
            {
                if (vertex1.Y == vertex2.Y)
                {
                        return vertex1.X < vertex2.X ? 1 : -1;
                }
                else
                {
                    return vertex1.Y < vertex2.Y ? 1 : -1;
                }
            }
            else
            {
                return vertex1.Z < vertex2.Z ? 1 : -1;
            }
          
        }
    }
}
