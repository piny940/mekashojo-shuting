namespace Model
{
    public class BombFire__Player
    {
        public void Attack(EnemyDamageManager enemyDamageManager)
        {
            //敵がボムの内側にスポーンした場合はダメージを与えない
            if (enemyDamageManager.frameCounterForPlayerBomb < enemyDamageManager.noBombDamageFrames)
            {
                return;
            }

            enemyDamageManager.GetDamage(
                EquipmentData.equipmentData.equipmentStatus
                [EquipmentData.equipmentType.Bomb]
                [EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.Bomb]]
                [EquipmentData.equipmentParameter.Power]
                );
        }
    }
}
