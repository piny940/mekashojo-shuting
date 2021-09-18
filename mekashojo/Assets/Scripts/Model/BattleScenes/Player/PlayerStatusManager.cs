using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class PlayerStatusManager
    {
        private const float MAIN_ENERGY_AUTO_CHARGE_AMOUNT = 10;
        private const float SUB_ENERGY_AUTO_CHARGE_AMOUNT = 10;
        private const int MAX_BOMB_AMOUNT = 3;

        //この3つの定数はView側でも使うからpublicにしておく
        //constにできないからhpとかの初期化がコンストラクタの中で行われてる
        public readonly float maxHP = 3000;
        public readonly float maxMainEnergy = 1000;
        public readonly float maxSubEnergy = 1000;

        private float _hp;
        private float _mainEnergyAmount;
        private float _subEnergyAmount;
        private int _bombAmount = 0;
        private bool _isDying = false;
        private float _damageReductionRate_Percent;

        private PauseManager _pauseManager;
        private Shield__Player _shield__Player;

        public UnityEvent<float> OnHPChanged = new UnityEvent<float>();
        public UnityEvent<float> OnMainEnergyChanged = new UnityEvent<float>();
        public UnityEvent<float> OnSubEnergyChanged = new UnityEvent<float>();
        public UnityEvent<int> OnBombAmountChanged = new UnityEvent<int>();
        public UnityEvent<bool> OnIsDyingChanged = new UnityEvent<bool>();

        public float hp
        {
            get { return _hp; }
            set
            {
                _hp = value;
                OnHPChanged?.Invoke(_hp);
            }
        }

        public float mainEnergyAmount
        {
            get { return _mainEnergyAmount; }
            set
            {
                _mainEnergyAmount = value;
                OnMainEnergyChanged?.Invoke(_mainEnergyAmount);
            }
        }

        public float subEnergyAmount
        {
            get { return _subEnergyAmount; }
            set
            {
                _subEnergyAmount = value;
                OnSubEnergyChanged?.Invoke(_subEnergyAmount);
            }
        }

        public int bombAmount
        {
            get { return _bombAmount; }
            set
            {
                _bombAmount = value;
                OnBombAmountChanged?.Invoke(value);
            }
        }

        public bool isDying
        {
            get { return _isDying; }
            set
            {
                _isDying = value;
                OnIsDyingChanged?.Invoke(value);
            }
        }

        public PlayerStatusManager(Shield__Player shield__Player, PauseManager pauseManager)
        {
            _pauseManager = pauseManager;
            _shield__Player = shield__Player;
            hp = maxHP;
            mainEnergyAmount = maxMainEnergy;
            subEnergyAmount = maxSubEnergy;

            if (EquipmentData.equipmentData.selectedShieldName == EquipmentData.equipmentType.Shield__Heavy)
            {
                _damageReductionRate_Percent
                    = EquipmentData.equipmentData.equipmentStatus
                    [EquipmentData.equipmentType.Shield__Heavy]
                    [EquipmentData.equipmentData.equipmentLevel
                    [EquipmentData.equipmentType.Shield__Heavy]]
                    [EquipmentData.equipmentParameter.DamageReductionRate];
            }
            else
            {
                _damageReductionRate_Percent
                    = EquipmentData.equipmentData.equipmentStatus
                    [EquipmentData.equipmentType.Shield__Light]
                    [EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.Shield__Light]]
                    [EquipmentData.equipmentParameter.DamageReductionRate];
            }
        }

        public void GetDamage(float amount)
        {
            if (_shield__Player.isUsingShield)
            {
                // シールドを使用中の場合
                hp -= amount * (100 - _damageReductionRate_Percent) * 0.01f;
            }
            else
            {
                hp -= amount;
            }

            if (hp < 0)
            {
                isDying = true;
            }
        }

        public void RunEveryFrame()
        {
            ChargeEnergyAutomatically();
        }

        private void ChargeEnergyAutomatically()
        {
            if (!_pauseManager.isGameGoing)
            {
                return;
            }

            ChargeMainEnergy(MAIN_ENERGY_AUTO_CHARGE_AMOUNT * Time.deltaTime);

            ChargeSubEnergy(SUB_ENERGY_AUTO_CHARGE_AMOUNT * Time.deltaTime);
        }

        public void ChargeMainEnergy(float amount)
        {
            mainEnergyAmount += Mathf.Min(amount, maxMainEnergy - mainEnergyAmount);
        }

        public void ChargeSubEnergy(float amount)
        {
            subEnergyAmount += Mathf.Min(amount, maxSubEnergy - subEnergyAmount);
        }

        public void ChargeBomb()
        {
            if (bombAmount < MAX_BOMB_AMOUNT)
            {
                bombAmount++;
            }
        }
    }
}
