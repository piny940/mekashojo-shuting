using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class PlayerStatusController
    {
        private const float MAIN_ENERGY_AUTO_CHARGE_AMOUNT = 10;
        private const float SUB_ENERGY_AUTO_CHARGE_AMOUNT = 10;

        //この3つの定数はView側でも使うからpublicにしておく
        //constにできないからhpとかの初期化がコンストラクタの中で行われてる
        //もっと綺麗なやり方があったら教えて欲しい
        public readonly float maxHP = 300;
        public readonly float maxMainEnergy = 1000;
        public readonly float maxSubEnergy = 1000;

        private float _hp;
        private float _mainEnergyAmount;
        private float _subEnergyAmount;

        private PauseController _pauseController;

        public UnityEvent<float> OnHPChanged = new UnityEvent<float>();

        public float hp
        {
            get { return _hp; }
            set
            {
                _hp = value;
                OnHPChanged?.Invoke(_hp);
            }
        }

        public UnityEvent<float> OnMainEnergyChanged = new UnityEvent<float>();

        public float mainEnergyAmount
        {
            get { return _mainEnergyAmount; }
            set
            {
                _mainEnergyAmount = value;
                OnMainEnergyChanged?.Invoke(_mainEnergyAmount);
            }
        }

        public UnityEvent<float> OnSubEnergyChanged = new UnityEvent<float>();

        public float subEnergyAmount
        {
            get { return _subEnergyAmount; }
            set
            {
                _subEnergyAmount = value;
                OnSubEnergyChanged?.Invoke(_subEnergyAmount);
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

            if (mainEnergyAmount < maxMainEnergy)
            {
                mainEnergyAmount += MAIN_ENERGY_AUTO_CHARGE_AMOUNT * Time.deltaTime;
            }

            if (subEnergyAmount < maxSubEnergy)
            {
                subEnergyAmount += SUB_ENERGY_AUTO_CHARGE_AMOUNT * Time.deltaTime;
            }
        }
    }
}
