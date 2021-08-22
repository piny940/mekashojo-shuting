namespace View
{
    public class Cannon__Player : CannonAndLaser__PlayerBase
    {
        void Start()
        {
            fire__Player.SetActive(false);

            Controller.ModelClassController.cannon__Player.OnFiringTargetChanged.AddListener(RotateFire);

            Controller.ModelClassController.cannon__Player.OnFireVisibilityChanged.AddListener((bool isFireVisible) =>
            {
                fire__Player.SetActive(isFireVisible);
            });
        }
    }
}
