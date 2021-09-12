using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEH.Entities;
using OpenTK;
namespace Game
{
    class MainCamera:Entity
    {
        public MainCamera() : base("MainCamera")
        {
            CameraComponent.IsEnabled = true;
            CameraComponent.SetActiveMainCamera();
              CameraComponent.SetPosition(new Vector3(0, 0, 30));
          //  CameraComponent.SetRotate(0, -3f);
        }
     
    }
}
