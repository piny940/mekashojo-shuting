using UnityEngine;

namespace Model
{
    public class PlayerPositionManager : MovingObjectBase
    {
        private const int ONE_SHAKE_FRAME_AMOUNT = 2; //Stun時の振動をどれだけ細かくするか
        private const float SHAKING_SPEED = 5; //Stun時の振動の速さ
        private const float REDUCTED_SPEED_RATE_HEAVY = 0.4f; //重シールド使用中の移動速度の割合
        private const float REDUCTED_SPEED_RATE_LIGHT = 0.7f; //軽シールド使用中の移動速度の割合
        private const float SPEED = 3; //移動速度
        private const float SPEED_RATE_WHILE_BOSS_APPEARING = 0.1f;

        private Shield__Player _shield__Player;
        private PlayerDebuffManager _playerDebuffManager;

        private int _stunFrameCount = 0;
        private Vector3 _shakingVector = Vector3.zero;
        private float _reductedSpeedRate__Shield;

        protected override movingObjectType objectType { get; set; }

        public PlayerPositionManager(Shield__Player shield__Player, PlayerDebuffManager playerDebuffManager, EnemyManager enemyManager, StageStatusManager stageStatusManager)
                : base(enemyManager, stageStatusManager)
        {
            _shield__Player = shield__Player;
            _playerDebuffManager = playerDebuffManager;
            objectType = movingObjectType.Player;

            if (EquipmentData.equipmentData.selectedShieldName == EquipmentData.equipmentType.Shield__Heavy)
            {
                _reductedSpeedRate__Shield = REDUCTED_SPEED_RATE_HEAVY;
            }
            else
            {
                _reductedSpeedRate__Shield = REDUCTED_SPEED_RATE_LIGHT;
            }
        }

        public void RunEveryFrame()
        {
            SetVelocity();
        }

        private void SetVelocity()
        {
            if (!stageStatusManager.isGameGoing)
            {
                velocity = Vector3.zero;
                return;
            }

            if (_playerDebuffManager.isStunned)
            {
                Stun();
                return;
            }

            //速度の設定
            float speed = SPEED * _playerDebuffManager.speedReductionRate;

            if (_shield__Player.isUsingShield)
            {
                // シールドを使用中の場合
                speed *= _reductedSpeedRate__Shield;
            }

            if (stageStatusManager.currentStageStatus == StageStatusManager.stageStatus.BossAppearing)
            {
                // ボス出現演出の途中の場合
                speed *= SPEED_RATE_WHILE_BOSS_APPEARING;
            }

            velocity = speed
                        * new Vector3(InputManager.horizontalKey,
                                        InputManager.verticalKey,
                                        0);
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
        }
    }
}
