using UnityEngine;

namespace Model
{
    public class Enemy__WideBeam : EnemyManager
    {
        float _time = 0;
        bool _isAttacking = false;

        NormalEnemyData _normalEnemyData;

        public Enemy__WideBeam(PauseController pauseController, EnemyController enemyController, PlayerStatusController playerStatusController, NormalEnemyData normalEnemyData) : base(pauseController, enemyController, playerStatusController)
        {
            _normalEnemyData = normalEnemyData;
        }

        public void RunEveryFrame(Vector3 thisPosition)
        {
            AttackProcess();
            DestroyLater(thisPosition);
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
                _isAttacking = IsBeamsProcessRunning(_normalEnemyData.beamNoticingTime, _normalEnemyData.beamTime);
        }
    }
}
