using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class Enemy__SpreadBullet : EnemyManager
    {
        private float _time;
        private bool _isAttacking = false;
        private readonly NormalEnemyData _normalEnemyData;
        private FiringBulletSettings _firingBulletSettings;
        private const int FIRE_AMOUNT_PER_ONCE = 5;

        public Enemy__SpreadBullet(PauseController pauseController, PlayerStatusController playerStatusController, EnemyController enemyController, NormalEnemyData normalEnemyData) : base(pauseController, enemyController, playerStatusController)
        {
            _normalEnemyData = normalEnemyData;
            _firingBulletSettings.shortFiringIntervalFrameAmount = _normalEnemyData.shortFiringIntervalFrameAmount;
            _firingBulletSettings.firePath = _normalEnemyData.type.ToString();
            _firingBulletSettings.firingAmount = _normalEnemyData.firintgAmount;
            _time = Random.value * _normalEnemyData.firingInterval;
            _firingBulletSettings.bulletVelocities = new List<Vector3>();

            //弾を発射する方向を計算
            for (int i = 0; i < FIRE_AMOUNT_PER_ONCE; i++)
            {
                _firingBulletSettings.bulletVelocities.Add(
                    _normalEnemyData.bulletSpeed
                    * new Vector3(
                        Mathf.Cos(Mathf.PI / 2 + Mathf.PI * i / FIRE_AMOUNT_PER_ONCE),
                        Mathf.Sin(Mathf.PI / 2 + Mathf.PI * i / FIRE_AMOUNT_PER_ONCE),
                        0)
                    );
            }
        }

        public void RunEveryFrame(Vector3 thisPosition)
        {
            AttackProcess();
            DestroyLater(thisPosition);
            StopOnPausing();
            SetConstantVelocity(_normalEnemyData.movingSpeed);
        }

        //一定間隔で攻撃をする処理
        private void AttackProcess()
        {
            if (!pauseController.isGameGoing) return;

            _time += Time.deltaTime;

            if (_time > _normalEnemyData.firingInterval && !_isAttacking)
            {
                _isAttacking = true;
                _time = 0;
            }

            if (_isAttacking) _isAttacking = IsBulletsProcessRunning(_firingBulletSettings);
        }
    }
}
