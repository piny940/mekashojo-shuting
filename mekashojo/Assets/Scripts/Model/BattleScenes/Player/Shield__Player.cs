using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class Shield__Player
    {
        private const float SHRINK_SPEED_HEAVY = 0.5f;
        private const float SHRINK_SPEED_LIGHT = 0.1f;
        private const float RECOVER_SPEED_HEAVY = 0.7f;
        private const float RECOVER_SPEED_LIGHT = 0.4f;

        private bool _isUsingShield = false;
        private float _shieldSize = 1;
        private StageStatusManager _stageStatusManager;
        private float _shrinkSpeed;
        private float _recoverSpeed;

        public UnityEvent<bool> OnIsUsingShieldChanged = new UnityEvent<bool>();
        public UnityEvent<float> OnShieldSizeChanged = new UnityEvent<float>();

        public bool isUsingShield
        {
            get { return _isUsingShield; }
            set
            {
                _isUsingShield = value;
                OnIsUsingShieldChanged?.Invoke(value);
            }
        }

        public float shieldSize
        {
            get { return _shieldSize; }
            set
            {
                _shieldSize = value;
                OnShieldSizeChanged?.Invoke(value);
            }
        }

        public Shield__Player(StageStatusManager stageStatusManager)
        {
            _stageStatusManager = stageStatusManager;

            if (EquipmentData.equipmentData.selectedShieldName == EquipmentData.equipmentType.Shield__Heavy)
            {
                // 重シールドを選択中の時
                _shrinkSpeed = SHRINK_SPEED_HEAVY;
                _recoverSpeed = RECOVER_SPEED_HEAVY;
            }
            else
            {
                // 軽シールドを選択中の時
                _shrinkSpeed = SHRINK_SPEED_LIGHT;
                _recoverSpeed = RECOVER_SPEED_LIGHT;
            }
        }

        public void RunEveryFrame()
        {
            ProceedShield();
        }

        private void ProceedShield()
        {
            if (!_stageStatusManager.isGameGoing) return;

            // 使用を始める/やめる処理
            if (InputManager.isMouseRight && shieldSize > 0)
            {
                if (!isUsingShield) isUsingShield = true;
                shieldSize -= _shrinkSpeed * Time.deltaTime;
            }
            else if (isUsingShield)
            {
                isUsingShield = false;
            }

            //右クリックを離したらシールドが回復する
            if (!InputManager.isMouseRight && shieldSize < 1)
            {
                shieldSize += _recoverSpeed * Time.deltaTime;
            }
        }
    }
}
