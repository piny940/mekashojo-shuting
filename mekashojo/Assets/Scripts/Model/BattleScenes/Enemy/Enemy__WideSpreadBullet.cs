using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class Enemy__WideSpreadBullet : DamageFactorManager
    {
        private const int FIRING_AMOUNT_PER_ONCE = 8;
        private readonly Controller.NormalEnemyData _normalEnemyData;
        private int _attackingFrameCount = 0;
        private float _time;
        private bool _isAttacking = false;
        private BulletProcessInfo _bulletProcessInfo;

        protected override DamageFactorData.damageFactorType factorType { get; set; }
        protected override void ChangeBeamStatus(beamFiringProcesses process) { }

        public Enemy__WideSpreadBullet(StageStatusManager stageStatusManager, PlayerStatusManager playerStatusManager, EnemyManager enemyManager, Controller.NormalEnemyData normalEnemyData)
                : base(stageStatusManager, enemyManager, playerStatusManager)
        {
            _normalEnemyData = normalEnemyData;

            _bulletProcessInfo = new BulletProcessInfo()
            {
                shortInterval_Frame = _normalEnemyData.shortFiringInterval_Frame,
                firePath = _normalEnemyData.type.ToString(),
                firingAmount = _normalEnemyData.firintgAmount,
                bulletVelocities = new List<Vector3>(),
            };

            //弾を発射する方向を計算
            for (int i = 0; i < FIRING_AMOUNT_PER_ONCE; i++)
            {
                _bulletProcessInfo.bulletVelocities.Add(
                    _normalEnemyData.bulletSpeed
                    * new Vector3(
                        Mathf.Cos(2 * Mathf.PI * i / FIRING_AMOUNT_PER_ONCE),
                        Mathf.Sin(2 * Mathf.PI * i / FIRING_AMOUNT_PER_ONCE),
                        0)
                    );
            }

            _time = Random.value * _normalEnemyData.firingInterval;
            factorType = DamageFactorData.damageFactorType.FiringNormalEnemy;
        }

        public void RunEveryFrame(Vector3 position)
        {
            ProceedAttack();
            DisappearIfOutside(position);
            StopOnPausing();
            SetConstantVelocity(_normalEnemyData.movingSpeed);
        }

        //一定間隔で攻撃をする処理
        private void ProceedAttack()
        {
            if (!stageStatusManager.isGameGoing) return;

            _time += Time.deltaTime;

            // 攻撃をやめる処理
            if (!_isAttacking && _attackingFrameCount > 0)
            {
                _attackingFrameCount = 0;
                return;
            }

            // 攻撃を始める処理
            if (_time > _normalEnemyData.firingInterval && !_isAttacking)
            {
                _isAttacking = true;
                _time = 0;
            }

            // 攻撃本体
            if (_isAttacking)
            {
                _isAttacking = ProceedBulletFiring(_bulletProcessInfo);
                _attackingFrameCount++;
            }
        }
    }
}
