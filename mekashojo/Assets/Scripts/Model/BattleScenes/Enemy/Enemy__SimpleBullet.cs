using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    // プレイヤーの方に向かって弾を飛ばす敵、すなわち
    // FastBullet, SlowBullet, SingleBullet, Missile,
    // RepeatedBullet, StunBullet, GuidedBulletはこのクラスを用いる
    public class Enemy__SimpleBullet : DamageFactorManager
    {
        private readonly Controller.NormalEnemyData _normalEnemyData;
        private float _time;
        private bool _isAttacking = false;
        private int _attackingFrameCount = 0;
        private Vector3 _newPlayerPosition;
        private BulletProcessInfo _bulletProcessInfo;
        protected override DamageFactorData.damageFactorType factorType { get; set; }

        public Enemy__SimpleBullet(PauseManager pauseManager, PlayerStatusManager playerStatusManager, EnemyManager enemyManager, Controller.NormalEnemyData normalEnemyData)
                : base(pauseManager, enemyManager, playerStatusManager)
        {
            _normalEnemyData = normalEnemyData;

            _bulletProcessInfo = new BulletProcessInfo()
            {
                shortInterval_Frame = _normalEnemyData.shortFiringInterval_Frame,
                firePath = _normalEnemyData.type.ToString(),
                firingAmount = _normalEnemyData.firintgAmount,
            };

            factorType = DamageFactorData.damageFactorType.FiringNormalEnemy;

            // 攻撃のタイミング処理のための_time変数
            // 0で初期化をするとスポーンしてから攻撃し始めるまでの時間が長すぎるため、
            // 0〜「攻撃間隔」までの中の乱数で初期化する
            _time = Random.value * _normalEnemyData.firingInterval;
        }

        public void RunEveryFrame(Vector3 position, Vector3 playerPosition)
        {
            AttackProcess(position, playerPosition);
            DestroyIfOutside(position);
            StopOnPausing();
            SetConstantVelocity(_normalEnemyData.movingSpeed);
        }

        //一定間隔で攻撃をする処理
        private void AttackProcess(Vector3 position, Vector3 playerPosition)
        {
            if (!pauseManager.isGameGoing) return;

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
                _newPlayerPosition = new Vector3(playerPosition.x, playerPosition.y, EnemyManager.enemyPosition__z);

                _bulletProcessInfo.bulletVelocities
                    = new List<Vector3>()
                    { (_newPlayerPosition - position) * _normalEnemyData.bulletSpeed / Vector3.Magnitude(_newPlayerPosition - position) };

                _time = 0;
            }

            // 攻撃本体
            if (_isAttacking)
            {
                _isAttacking = ProceedBulletFiring(_bulletProcessInfo);
                _attackingFrameCount++;
            }

            // ProceedBulletFiringは本来一定時間が経てば自動的にfalseを返すようになるのだが、
            // 何らかの原因でfalseを返さなくなった場合を想定して、一定時間が経過したら
            // 強制的に攻撃を終了するプログラムを書いておく
            if (_attackingFrameCount > _bulletProcessInfo.firingAmount
                                            * _bulletProcessInfo.shortInterval_Frame
                                            + EXTRA_FRAME_AMOUNT)
            {
                _isAttacking = false;
                ResetAttacking();
            }
        }
    }
}
