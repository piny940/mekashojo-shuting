using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class PlayerStatusController
    {
        private const float MAIN_ENERGY_AUTO_CHARGE_AMOUNT = 10;
        private const float SUB_ENERGY_AUTO_CHARGE_AMOUNT = 10;
        private const int MAX_BOMB_AMOUNT = 3;

        //この3つの定数はView側でも使うからpublicにしておく
        //constにできないからhpとかの初期化がコンストラクタの中で行われてる
        //もっと綺麗なやり方があったら教えて欲しい
        public readonly float maxHP = 300;
        public readonly float maxMainEnergy = 1000;
        public readonly float maxSubEnergy = 1000;

        private float _hp;
        private float _mainEnergyAmount;
        private float _subEnergyAmount;
        private int _bombAmount = 0;

        private PauseController _pauseController;

        public UnityEvent<float> OnHPChanged = new UnityEvent<float>();
        public UnityEvent<float> OnMainEnergyChanged = new UnityEvent<float>();
        public UnityEvent<float> OnSubEnergyChanged = new UnityEvent<float>();
        public UnityEvent<int> OnBombAmountChanged = new UnityEvent<int>();

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

        public PlayerStatusController(PauseController pauseController)
        {
            _pauseController = pauseController;
            hp = maxHP;
            mainEnergyAmount = maxMainEnergy;
            subEnergyAmount = maxSubEnergy;
        }

        public void ChangeHP(float amount)
        {
            hp -= amount;
        }

        public void ChargeEnergyAutomatically()
        {
            if (!_pauseController.isGameGoing)
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
            if (bombAmount != MAX_BOMB_AMOUNT)
            {
                bombAmount++;
            }
        }
    }
}
