using UnityEngine;

namespace Model
{
    public class DropMaterialManager : MovingObjectBase
    {
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

        public DropMaterialManager(materialType type, PlayerStatusController playerStatusController, PauseController pauseController) : base(pauseController)
        {
            _materialType = type;
            _playerStatusController = playerStatusController;
        }


        public void RunEveryFrame(Vector3 thisPosition)
        {
            StopOnPausing();
            DestroyLater(thisPosition);
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
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__Cannon]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case materialType.LaserEnhancementMaterial:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__Laser]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case materialType.BeamMachineGunEnhancementMaterial:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case materialType.BalkanEnhancementMaterial:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.SubWeapon__Balkan]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case materialType.MissileEnhancementMaterial:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.SubWeapon__Missile]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case materialType.BombEnhancementMaterial:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Bomb]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case materialType.HeavyShieldEnhancementMaterial:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Shield__Heavy]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case materialType.LightShieldEnhancementMaterial:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Shield__Light]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case materialType.EnergyChargeMaterial:
                    _playerStatusController.ChargeMainEnergy(MAIN_ENERGY_CHARGE_AMOUNT);
                    _playerStatusController.ChargeSubEnergy(SUB_ENERGY_CHARGE_AMOUNT);
                    break;

                case materialType.BombChargeMaterial:
                    //TODO:ボムをチャージする
                    break;

                default:
                    break;
            }

            isDestroyed = true;
        }
    }
}
