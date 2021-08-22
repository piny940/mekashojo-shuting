using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    // プレイヤーの方に向かって弾を飛ばす敵、すなわち
    // FastBullet, SlowBullet, SingleBullet, Missile,
    // RepeatedBullet, StunBullet, GuidedBulletはこのクラスを用いる
    public class Enemy__SimpleBullet : EnemyManager
    {
        private float _time;
        private bool _isAttacking = false;
        private Vector3 _newPlayerPosition;
        private readonly NormalEnemyData _normalEnemyData;
        private FiringBulletSettings _firingBulletSettings;
        private readonly float _enemyPosition__z = EnemyController.enemyPosition__z;

        public Enemy__SimpleBullet(PauseController pauseController, PlayerStatusController playerStatusController, EnemyController enemyController, NormalEnemyData normalEnemyData) : base(pauseController, enemyController, playerStatusController)
        {
            _normalEnemyData = normalEnemyData;
            _firingBulletSettings.shortFiringIntervalFrameAmount = _normalEnemyData.shortFiringIntervalFrameAmount;
            _firingBulletSettings.firePath = _normalEnemyData.type.ToString();
            _firingBulletSettings.firingAmount = _normalEnemyData.firintgAmount;
            _time = Random.value * _normalEnemyData.firingInterval;
        }

        public void RunEveryFrame(Vector3 playerPosition, Vector3 thisPosition)
        {
            AttackProcess(playerPosition, thisPosition);
            DestroyLater(thisPosition);
            StopOnPausing();
            SetConstantVelocity(_normalEnemyData.movingSpeed);
        }

        //一定間隔で攻撃をする処理
        private void AttackProcess(Vector3 playerPosition, Vector3 thisPosition)
        {
            if (!pauseController.isGameGoing) return;

            _time += Time.deltaTime;

            if (_time > _normalEnemyData.firingInterval && !_isAttacking)
            {
                _isAttacking = true;
                _newPlayerPosition = new Vector3(playerPosition.x, playerPosition.y, _enemyPosition__z);

                _firingBulletSettings.bulletVelocities
                    = new List<Vector3>()
                    { (_newPlayerPosition - thisPosition) * _normalEnemyData.bulletSpeed / Vector3.Magnitude(_newPlayerPosition - thisPosition) };

                _time = 0;
            }

            if (_isAttacking) _isAttacking = IsBulletsProcessRunning(_firingBulletSettings);
        }
    }
}
