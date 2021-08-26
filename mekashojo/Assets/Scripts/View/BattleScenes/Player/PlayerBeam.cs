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
                if (collision.tag == "BattleScenes/Enemy")
                {
                    DealDamage(collision);
                }
            };
        }


        private void DealDamage(Collider2D collision)
        {
            EnemyIDContainer enemyIDContainer = collision.GetComponent<EnemyIDContainer>();

            if (enemyIDContainer == null) throw new System.Exception();

            float damageAmount
                = Model.EquipmentData.equipmentData.equipmentStatus[_type]
                    [Model.EquipmentData.equipmentData.equipmentLevel[_type]]
                    [Model.EquipmentData.equipmentParameter.Power] * Time.deltaTime;

            Model.EnemyDamageManager enemyDamageManager
                = Controller.EnemyClassController.damageManagerTable[enemyIDContainer.id];

            switch (_type)
            {
                case Model.EquipmentData.equipmentType.MainWeapon__Cannon:
                    Controller.PlayerClassController.cannonFire.DealDamage(enemyDamageManager, damageAmount);
                    break;

                case Model.EquipmentData.equipmentType.MainWeapon__Laser:
                    Controller.PlayerClassController.laserFire.DealDamage(enemyDamageManager, damageAmount);
                    break;

                default:
                    throw new System.Exception();
            }
        }
    }
}
