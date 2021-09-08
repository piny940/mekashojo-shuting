using UnityEngine;

namespace Model
{
    public class PlayerPositionManager : MovingObjectBase
    {
        private const int ONE_SHAKE_FRAME_AMOUNT = 2; //Stun時の振動をどれだけ細かくするか
        private const float SHAKING_SPEED = 5; //Stun時の振動の速さ
        private const float STUN_DURATION = 2; //Stunの長さ
        private const float REDUCTED_SPEED_RATE_HEAVY = 0.4f; //重シールド使用中の移動速度の割合
        private const float REDUCTED_SPEED_RATE_LIGHT = 0.7f; //軽シールド使用中の移動速度の割合
        private const float SPEED = 3; //移動速度

        private Shield__Player _shield__Player;

        private int _stunFrameCount = 0;
        private Vector3 _shakingVector = Vector3.zero;
        private float _stunTime = 0;
        private float _reductedSpeedRate;

        protected override movingObjectType objectType { get; set; }

        public bool isStunning = false;

        public PlayerPositionManager(Shield__Player shield__Player, EnemyManager enemyManager, PauseManager pauseManager)
                : base(enemyManager, pauseManager)
        {
            _shield__Player = shield__Player;
            objectType = movingObjectType.Player;

            if (EquipmentData.equipmentData.selectedShieldName == EquipmentData.equipmentType.Shield__Heavy)
            {
                _reductedSpeedRate = REDUCTED_SPEED_RATE_HEAVY;
            }
            else
            {
                _reductedSpeedRate = REDUCTED_SPEED_RATE_LIGHT;
            }
        }

        public void RunEveryFrame()
        {
            SetVelocity();
        }

        private void SetVelocity()
        {
            if (!pauseManager.isGameGoing)
            {
                velocity = Vector3.zero;
                return;
            }

            if (isStunning)
            {
                Stun();
                return;
            }

            //速度の設定
            if (_shield__Player.isUsingShield)
            {
                // シールドを使用中の場合
                velocity = new Vector3(
                    InputManager.horizontalKey * SPEED * _reductedSpeedRate,
                    InputManager.verticalKey * SPEED * _reductedSpeedRate,
                    0);
            }
            else
            {
                velocity = new Vector3(
                    InputManager.horizontalKey * SPEED,
                    InputManager.verticalKey * SPEED,
                    0);
            }
        }

        private void Stun()
        {
            //微小振動させる
            if (_stunFrameCount % (ONE_SHAKE_FRAME_AMOUNT * 2) == 0)
            {
                _shakingVector = new Vector3(Random.value * SHAKING_SPEED, Random.value * SHAKING_SPEED, 0);
                velocity = -_shakingVector;
                _stunFrameCount = 0;
            }
            else if (_stunFrameCount % (ONE_SHAKE_FRAME_AMOUNT * 2) == ONE_SHAKE_FRAME_AMOUNT)
            {
                velocity = _shakingVector;
            }

            _stunFrameCount++;
            _stunTime += Time.deltaTime;

            //一定時間振動したら元に戻る
            if (_stunTime > STUN_DURATION)
            {
                isStunning = false;
                _stunTime = 0;
            }
        }
    }
}
