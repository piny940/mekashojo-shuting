namespace View
{
    public class Laser__Player : CannonAndLaser__PlayerBase
    {
        void Start()
        {
            fire__Player.SetActive(false);

            Controller.ModelClassController.laser__Player.OnFiringTargetChanged.AddListener(RotateFire);

            Controller.ModelClassController.laser__Player.OnFireVisibilityChanged.AddListener((bool isFireVisible) =>
            {
                fire__Player.SetActive(isFireVisible);
            });
        }
    }
}
