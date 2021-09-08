namespace View
{
    public class Cannon__Player : CannonAndLaser__PlayerBase
    {
        void Start()
        {
            fire__Player.SetActive(false);

            Controller.BattleScenesController.cannon__Player.OnFiringTargetChanged.AddListener(RotateFire);

            Controller.BattleScenesController.cannon__Player.OnFireVisibilityChanged.AddListener((bool isFireVisible) =>
            {
                fire__Player.SetActive(isFireVisible);
            });
        }
    }
}
