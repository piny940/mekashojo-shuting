using UnityEngine;

namespace Model
{
    public class PlayerFire : MovingObjectBase
    {
        private readonly EquipmentData.equipmentType _type;
        private float _power;
        private PlayerDebuffManager _playerDebuffManager;
        protected override movingObjectType objectType { get; set; }

        public PlayerFire(PlayerDebuffManager playerDebuffManager, EnemyManager enemyManager, PauseManager pauseManager, EquipmentData.equipmentType type)
                : base(enemyManager, pauseManager)
        {
            objectType = movingObjectType.PlayerFire;

            _playerDebuffManager = playerDebuffManager;

            _type = type;
            _power = EquipmentData.equipmentData.equipmentStatus[_type]
                    [EquipmentData.equipmentData.equipmentLevel[_type]]
                    [EquipmentData.equipmentParameter.Power];
        }

        public void RunEveryFrame(Vector3 position)
        {
            StopOnPausing();
            DisappearIfOutside(position);
        }

        public void DealDamage(EnemyDamageManager enemyDamageManager)
        {
            float temporaryHP = enemyDamageManager.hp;

            //ダメージを与える
            // キャノン/レーザーの場合、毎フレーム攻撃がされるため、
            // _powerにTime.deltaTimeをかけておく必要がある
            if (_type == EquipmentData.equipmentType.MainWeapon__Cannon
                || _type == EquipmentData.equipmentType.MainWeapon__Laser)
            {
                enemyDamageManager.GetDamage(_power * _playerDebuffManager.powerReductionRate * Time.deltaTime);
            }
            else
            {
                enemyDamageManager.GetDamage(_power * _playerDebuffManager.powerReductionRate);
            }

            // タイプがキャノン・レーザー以外の場合、
            //　弾は敵に当たったら敵のHP分だけ攻撃力が小さくなり、
            //　攻撃力が0以下になったら消滅する
            if (_type == EquipmentData.equipmentType.MainWeapon__Cannon
                || _type == EquipmentData.equipmentType.MainWeapon__Laser)
                return;

            _power -= temporaryHP;
            if (_power <= 0)
            {
                isBeingDestroyed = true;
            }
        }
    }
}
