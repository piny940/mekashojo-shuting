using UnityEngine;

namespace View
{
    public class BeamMachineGun__Player : FiringBulletBase
    {
        private const float BULLET_SPEED = 15;

        void Start()
        {
            Controller.BattleScenesController.beamMachineGun__Player.OnBeamMachineGunNumberChanged.AddListener(() =>
            {
                Vector3 bulletVelocity
                    = (Model.InputManager.mousePosition - transform.position)
                        * BULLET_SPEED
                        / Vector3.Magnitude(Model.InputManager.mousePosition - transform.position);

                Fire(bulletVelocity, "BeamMachineGunFire__Player");
            });
        }
    }
}
