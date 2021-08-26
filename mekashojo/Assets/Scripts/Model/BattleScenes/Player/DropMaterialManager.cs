using UnityEngine;

namespace Model
{
    public class DropMaterialManager : MovingObjectBase
    {
        private const float RISE_TIME = 0.1f; //上昇する時間
        private const float FALL_TIME = 0.1f; //下降する時間
        private const float RISING_SPEED = 2; //上昇するスピード
        private const float FALLING_SPEED = 2; //下降するスピード
        private const float MAIN_ENERGY_CHARGE_AMOUNT = 300; //メインエネルギー回復量
        private const float SUB_ENERGY_CHARGE_AMOUNT = 300; //サブエネルギー回復量
        private materialType _materialType;
        private PlayerStatusController _playerStatusController;
        private float _time = 0;
        private bool _hasAppeared = false;
        private bool _isRising = false;

        protected override movingObjectType objectType { get; set; }

        public enum materialType
        {
            CannonEnhancementMaterial,
            LaserEnhancementMaterial,
            BeamMachineGunEnhancementMaterial,
            BalkanEnhancementMaterial,
            MissileEnhancementMaterial,
            BombEnhancementMaterial,
            HeavyShieldEnhancementMaterial,
            LightShieldEnhancementMaterial,
            EnergyChargeMaterial,
            BombChargeMaterial
        }

        public DropMaterialManager(materialType type, EnemyController enemyController, PlayerStatusController playerStatusController, PauseController pauseController) : base(enemyController, pauseController)
        {
            _materialType = type;
            _playerStatusController = playerStatusController;
            objectType = movingObjectType.DropItem;
        }


        public void RunEveryFrame(Vector3 position)
        {
            StopOnPausing();
            DestroyLater(position);
            Emerge();
        }

        /// <summary>
        /// 出現するときにぴょこんと跳ぶ動きをつける<br></br>
        /// </summary>
        private void Emerge()
        {
            if (!_hasAppeared)
            {
                _time += Time.deltaTime;

                //上昇
                if (!_isRising && _time < RISE_TIME)
                {
                    velocity = new Vector3(0, RISING_SPEED, 0);
                    _isRising = true;
                    return;
                }

                //下降
                if (_isRising && _time > RISE_TIME && _time < RISE_TIME + FALL_TIME)
                {
                    velocity = new Vector3(0, -FALLING_SPEED, 0);
                    _isRising = false;
                    return;
                }

                //停止
                if (_time >= RISE_TIME + FALL_TIME)
                {
                    velocity = Vector3.zero;
                    _hasAppeared = true;
                }
            }
        }

        public void PickedUp()
        {
            switch (_materialType)
            {
                case materialType.CannonEnhancementMaterial:
                    EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.MainWeapon__Cannon]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case materialType.LaserEnhancementMaterial:
                    EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.MainWeapon__Laser]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case materialType.BeamMachineGunEnhancementMaterial:
                    EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.MainWeapon__BeamMachineGun]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case materialType.BalkanEnhancementMaterial:
                    EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.SubWeapon__Balkan]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case materialType.MissileEnhancementMaterial:
                    EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.SubWeapon__Missile]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case materialType.BombEnhancementMaterial:
                    EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.Bomb]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case materialType.HeavyShieldEnhancementMaterial:
                    EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.Shield__Heavy]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case materialType.LightShieldEnhancementMaterial:
                    EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.Shield__Light]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case materialType.EnergyChargeMaterial:
                    _playerStatusController.ChargeMainEnergy(MAIN_ENERGY_CHARGE_AMOUNT);
                    _playerStatusController.ChargeSubEnergy(SUB_ENERGY_CHARGE_AMOUNT);
                    break;

                case materialType.BombChargeMaterial:
                    _playerStatusController.ChargeBomb();
                    break;

                default:
                    break;
            }

            isBeingDestroyed = true;
        }
    }
}
