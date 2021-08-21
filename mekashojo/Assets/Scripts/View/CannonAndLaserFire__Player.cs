using UnityEngine;

namespace View
{
    public class CannonAndLaserFire__Player : CollisionBase
    {
        [SerializeField, Header("武器のタイプを選ぶ")] private Model.EquipmentData.equipmentType _type;

        void Start()
        {
            playWhileIn += (collision) =>
            {
                if (collision.tag == "BattleScenes/Enemy")
                {
                    EnemyIDContainer enemyIDContainer = collision.GetComponent<EnemyIDContainer>();

                    if (enemyIDContainer == null) throw new System.Exception();

                    Controller.PlayerClassController.cannonFire.DoDamage(
                        Controller.EnemyClassController.damageManagerTable[enemyIDContainer.id],
                        Model.EquipmentData.equipmentData.equipmentStatus[_type]
                        [Model.EquipmentData.equipmentData.equipmentLevel[_type]]
                        [Model.EquipmentData.equipmentParameter.Power] * Time.deltaTime);
                }
            };
        }
    }
}
