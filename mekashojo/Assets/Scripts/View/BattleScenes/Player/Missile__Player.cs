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
                Vector3 bulletVelocity = new Vector3(1, 0, 0) * BULLET_SPEED;

                Fire(bulletVelocity, Model.EquipmentData.equipmentFireNames.MissileFire__Player.ToString());
            });
        }
    }
}
