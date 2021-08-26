using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    // プレイヤーの方に向かって弾を飛ばす敵、すなわち
    // FastBullet, SlowBullet, SingleBullet, Missile,
    // RepeatedBullet, StunBullet, GuidedBulletはこのクラスを用いる
    public class Enemy__SimpleBullet : DamageFactorManager
    {
        private readonly float _enemyPosition__z = EnemyController.enemyPosition__z;
        private float _time;
        private bool _isAttacking = false;
        private Vector3 _newPlayerPosition;
        private NormalEnemyData _normalEnemyData;
        private BulletProcessInfo _firingBulletInfo;
        protected override DamageFactorData.damageFactorType factorType { get; set; }

        public Enemy__SimpleBullet(PauseController pauseController, PlayerStatusController playerStatusController, EnemyController enemyController, NormalEnemyData normalEnemyData)
                : base(pauseController, enemyController, playerStatusController)
        {
            _normalEnemyData = normalEnemyData;
            _firingBulletInfo.shortInterval_Frame = _normalEnemyData.shortFiringInterval_Frame;
            _firingBulletInfo.firePath = _normalEnemyData.type.ToString();
            _firingBulletInfo.firingAmount = _normalEnemyData.firintgAmount;
            _time = Random.value * _normalEnemyData.firingInterval;
            factorType = DamageFactorData.damageFactorType.FiringNormalEnemy;
        }

        public void RunEveryFrame(Vector3 position, Vector3 playerPosition)
        {
            AttackProcess(position, playerPosition);
            DestroyLater(position);
            StopOnPausing();
            SetConstantVelocity(_normalEnemyData.movingSpeed);
        }

        //一定間隔で攻撃をする処理
        private void AttackProcess(Vector3 position, Vector3 playerPosition)
        {
            if (!pauseController.isGameGoing) return;

            _time += Time.deltaTime;

            if (_time > _normalEnemyData.firingInterval && !_isAttacking)
            {
                _isAttacking = true;
                _newPlayerPosition = new Vector3(playerPosition.x, playerPosition.y, _enemyPosition__z);

                _firingBulletInfo.bulletVelocities
                    = new List<Vector3>()
                    { (_newPlayerPosition - position) * _normalEnemyData.bulletSpeed / Vector3.Magnitude(_newPlayerPosition - position) };

                _time = 0;
            }

            if (_isAttacking) _isAttacking = ProceedBulletFiring(_firingBulletInfo);
        }
    }
}
