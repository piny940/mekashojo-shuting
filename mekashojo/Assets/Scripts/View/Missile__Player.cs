using UnityEngine;

namespace View
{
    public class Missile__Player : FiringBulletBase
    {
        private const float BULLET_SPEED = 10;

        // Start is called before the first frame update
        void Start()
        {
            Controller.ModelClassController.missile__Player.OnMissileNumberChanged.AddListener(() =>
            {
                Vector3 bulletVelocity
                    = (Model.InputController.mousePosition - transform.position)
                        * BULLET_SPEED
                        / Vector3.Magnitude(Model.InputController.mousePosition - transform.position);

                Fire(bulletVelocity, "MissileFire__Player");
            });
        }
    }
}
