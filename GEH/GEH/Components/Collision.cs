using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEH.Entities;
using GEH.ColliderElements;
namespace GEH.Components
{
    
  public  class Collision:Component
    {
      
        private Dictionary<Entity, List<PlaneCollision>> DictionaryPlacesCollisionsWithOtherEntities = default;
        public Collision(Entity entity)
        {
         MainEntity= entity;
        }
       private List<Entity> GetEntitiesInRadius()
        {
           
            List<Entity> sendEntitys = new List<Entity>();
            foreach (var item in Scene.entities)
            {
                    sendEntitys.Add(item);
            }
            return sendEntitys;
        }
       public List<PlaneCollision> GetPlaneCollisionsFromDictionary()
        {
            List<PlaneCollision> endPlaneCollisions = new List<PlaneCollision>();
            foreach (var item in DictionaryPlacesCollisionsWithOtherEntities.Values.ToList())
            {
                foreach (var item2 in item)
                {
                    endPlaneCollisions.Add(item2);
                }
                
            }
            return endPlaneCollisions;
        }
        public List<Entity> GetEntitiesFromDictionary()
        {
            return DictionaryPlacesCollisionsWithOtherEntities.Keys.ToList();
        }
        public void  SetPlacesCollisionsWithOtherEntities()
        {
            PlaneManager planeManager = new PlaneManager(MainEntity);
            CheckIsEnabledComponent();
            DictionaryPlacesCollisionsWithOtherEntities = new Dictionary<Entity, List<PlaneCollision>>();
            List<Entity> entitiesInRadius = GetEntitiesInRadius();
            foreach (var item in entitiesInRadius)
            {
                PlaneManager planeManager2 = new PlaneManager(item);
                List<PlaneCollision> allPlaneCollisions = planeManager2.GetAllPlaneCollisions(planeManager);
                DictionaryPlacesCollisionsWithOtherEntities.Add(item, allPlaneCollisions);
            }
          
        }
    }
}
