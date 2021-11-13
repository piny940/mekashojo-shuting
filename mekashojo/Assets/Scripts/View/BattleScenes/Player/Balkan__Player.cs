using UnityEngine;

namespace View
{
    public class Balkan__Player : FiringBulletBase
    {
        private const float BULLET_SPEED = 30;

        void Start()
        {
            Controller.BattleScenesController.balkan__Player.OnBalkanNumberChanged.AddListener(() =>
            {
                Vector3 bulletVelocity
                    = (Model.InputManager.mousePosition - transform.position)
                        * BULLET_SPEED
                        / Vector3.Magnitude(Model.InputManager.mousePosition - transform.position);

                Fire(bulletVelocity, Model.EquipmentData.equipmentFireNames.BalkanFire__Player.ToString());
            });
        }
    }
}