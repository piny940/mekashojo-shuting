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
        private BulletProcessInfo _bulletProcessInfo;

        protected override DamageFactorData.damageFactorType factorType { get; set; }
        protected override void ChangeBeamStatus(beamFiringProcesses process) { }

        public Enemy__SimpleBullet(StageStatusManager stageStatusManager, PlayerStatusManager playerStatusManager, EnemyManager enemyManager, Controller.NormalEnemyData normalEnemyData)
                : base(stageStatusManager, enemyManager, playerStatusManager)
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
            ProceedAttack(position, playerPosition);
            DisappearIfOutside(position);
            StopOnPausing();
            SetConstantVelocity(_normalEnemyData.movingSpeed);
        }

        //一定間隔で攻撃をする処理
        private void ProceedAttack(Vector3 position, Vector3 playerPosition)
        {
            if (!stageStatusManager.isGameGoing
                || stageStatusManager.currentStageStatus == StageStatusManager.stageStatus.BossAppearing)
                return;

            _time += Time.deltaTime;

            // 攻撃を始める処理
            if (_time > _normalEnemyData.firingInterval && !_isAttacking)
            {
                _isAttacking = true;
                Vector3 newPlayerPosition = new Vector3(playerPosition.x, playerPosition.y, EnemyManager.enemyPosition__z);

                _bulletProcessInfo.bulletVelocities
                    = new List<Vector3>()
                    { (newPlayerPosition - position) * _normalEnemyData.bulletSpeed / Vector3.Magnitude(newPlayerPosition - position) };

                _time = 0;
            }

            // 攻撃本体
            if (_isAttacking)
            {
                _isAttacking = ProceedBulletFiring(_bulletProcessInfo);
            }
        }
    }
}
