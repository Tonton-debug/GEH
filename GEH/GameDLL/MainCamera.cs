using GEH.Entities;
using OpenTK;
namespace GameDLL
{
    public class MainCamera:Entity
    {
        public MainCamera() : base("MainCamera")
        {
            CameraComponent.IsEnabled = true;
            CameraComponent.SetActiveMainCamera();
            CameraComponent.SetPosition(new Vector3(0, 5, 30));
            CameraComponent.SetRotate(0, 0f);
        }
     
    }
}
