using UnityEngine;

namespace Model
{
    public class Enemy__WideBeam : DamageFactorManager
    {
        private float _time = 0;
        private bool _isAttacking = false;
        private NormalEnemyData _normalEnemyData;
        protected override DamageFactorData.damageFactorType factorType { get; set; }

        public Enemy__WideBeam(PauseController pauseController, EnemyController enemyController, PlayerStatusController playerStatusController, NormalEnemyData normalEnemyData)
                : base(pauseController, enemyController, playerStatusController)
        {
            _normalEnemyData = normalEnemyData;
            _time = Random.value * _normalEnemyData.firingInterval;
            factorType = DamageFactorData.damageFactorType.FiringNormalEnemy;
        }

        public void RunEveryFrame(Vector3 position)
        {
            AttackProcess();
            DestroyLater(position);
            StopOnPausing();
            SetConstantVelocity(_normalEnemyData.movingSpeed);
        }

        private void AttackProcess()
        {
            if (!pauseController.isGameGoing) return;

            _time += Time.deltaTime;

            if (_time > _normalEnemyData.firingInterval && !_isAttacking)
            {
                _isAttacking = true;
                _time = 0;
            }

            if (_isAttacking)
                _isAttacking = ProceedBeamFiring(_normalEnemyData.beamNotifyingTime, _normalEnemyData.beamTime);
        }
    }
}
