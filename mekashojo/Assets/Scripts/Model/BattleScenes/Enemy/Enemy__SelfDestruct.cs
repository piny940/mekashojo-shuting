using UnityEngine;

namespace Model
{
    public class Enemy__SelfDestruct : EnemyManager
    {
        private NormalEnemyData _normalEnemyData;

        public Enemy__SelfDestruct(PauseController pauseController, PlayerStatusController playerStatusController, EnemyController enemyController, NormalEnemyData normalEnemyData) : base(pauseController, enemyController, playerStatusController)
        {
            _normalEnemyData = normalEnemyData;
        }

        public void RunEveryFrame(Vector3 thisPosition)
        {
            StopOnPausing();
            DestroyLater(thisPosition);
            StartAnimation();
            SetConstantVelocity(_normalEnemyData.movingSpeed);
        }

        public new void DoDamage()
        {
            DoDamageBase(_normalEnemyData.damageAmount);
            isDestroyed = true;
        }
    }
}
