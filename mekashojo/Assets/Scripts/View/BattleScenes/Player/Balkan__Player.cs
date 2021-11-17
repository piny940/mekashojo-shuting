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
                    = new Vector3(1, 0, 0) * BULLET_SPEED;

                Fire(bulletVelocity, Model.EquipmentData.equipmentFireNames.BalkanFire__Player.ToString());
            });
        }
    }
}
