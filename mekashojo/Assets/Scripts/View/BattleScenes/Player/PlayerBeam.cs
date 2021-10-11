using UnityEngine;

namespace View
{
    // CannonとLaserにはこのクラスをつける
    public class PlayerBeam : CollisionBase
    {
        [SerializeField, Header("武器のタイプを選ぶ")] private Model.EquipmentData.equipmentType _type;

        void Start()
        {
            playWhileIn += (collision) =>
            {
                if (collision.tag == TagManager.TagNames.BattleScenes__Enemy.ToString())
                {
                    DealDamage(collision);
                }
            };
        }

        private void DealDamage(Collider2D collision)
        {
            EnemyIDContainer enemyIDContainer = collision.GetComponent<EnemyIDContainer>();

            if (enemyIDContainer == null) throw new System.Exception();

            Model.EnemyDamageManager enemyDamageManager
                = Controller.EnemyController.damageManagerTable[enemyIDContainer.id];

            switch (_type)
            {
                case Model.EquipmentData.equipmentType.MainWeapon__Cannon:
                    Controller.PlayerController.cannonFire.DealDamage(enemyDamageManager);
                    break;

                case Model.EquipmentData.equipmentType.MainWeapon__Laser:
                    Controller.PlayerController.laserFire.DealDamage(enemyDamageManager);
                    break;

                default:
                    throw new System.Exception();
            }
        }
    }
}
