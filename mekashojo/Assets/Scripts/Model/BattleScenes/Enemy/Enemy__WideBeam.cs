using UnityEngine;

namespace Model
{
    public class Enemy__WideBeam : DamageFactorManager
    {
        private float _time = 0;
        private float _attackingTime = 0;
        private bool _isAttacking = false;
        private Controller.NormalEnemyData _normalEnemyData;
        protected override DamageFactorData.damageFactorType factorType { get; set; }

        public Enemy__WideBeam(PauseManager pauseManager, PlayerStatusManager playerStatusManager, EnemyManager enemyManager, Controller.NormalEnemyData normalEnemyData)
                : base(pauseManager, enemyManager, playerStatusManager)
        {
            _normalEnemyData = normalEnemyData;
            _time = Random.value * _normalEnemyData.firingInterval;
            factorType = DamageFactorData.damageFactorType.FiringNormalEnemy;
        }

        public void RunEveryFrame(Vector3 position)
        {
            AttackProcess();
            DestroyIfOutside(position);
            StopOnPausing();
            SetConstantVelocity(_normalEnemyData.movingSpeed);
        }

        private void AttackProcess()
        {
            if (!pauseManager.isGameGoing) return;

            _time += Time.deltaTime;

            // 攻撃をやめる処理
            if (!_isAttacking && _attackingTime > 0)
            {
                _attackingTime = 0;
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
                _isAttacking = ProceedBeamFiring(_normalEnemyData.beamNotifyingTime, _normalEnemyData.beamTime);
                _attackingTime += Time.deltaTime;
            }

            // ProceedBeamFiringは本来一定時間が経てば自動的にfalseを返すようになるのだが、
            // 何らかの原因でfalseを返さなくなった場合を想定して、一定時間が経過したら
            // 強制的に攻撃を終了するプログラムを書いておく
            if (_attackingTime > _normalEnemyData.beamNotifyingTime
                                            + _normalEnemyData.beamTime
                                            + EXTRA_FRAME_AMOUNT)
            {
                _isAttacking = false;
                ResetAttacking();
            }
        }
    }
}
