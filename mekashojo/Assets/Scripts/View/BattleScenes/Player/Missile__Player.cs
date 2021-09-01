using UnityEngine;

namespace View
{
    public class Missile__Player : FiringBulletBase
    {
        private const float BULLET_SPEED = 10;

        void Start()
        {
            Controller.BattleScenesController.missile__Player.OnMissileNumberChanged.AddListener(() =>
            {
                Vector3 bulletVelocity
                    = (Model.InputManager.mousePosition - transform.position)
                        * BULLET_SPEED
                        / Vector3.Magnitude(Model.InputManager.mousePosition - transform.position);

                Fire(bulletVelocity, "MissileFire__Player");
            });
        }
    }
}
