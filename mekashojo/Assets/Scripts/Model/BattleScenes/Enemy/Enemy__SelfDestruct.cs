using UnityEngine;

namespace Model
{
    public class Enemy__SelfDestruct : DamageFactorManager
    {
        private NormalEnemyData _normalEnemyData;
        protected override DamageFactorData.damageFactorType factorType { get; set; }

        public Enemy__SelfDestruct(PauseController pauseController, PlayerStatusController playerStatusController, EnemyController enemyController, NormalEnemyData normalEnemyData)
                : base(pauseController, enemyController, playerStatusController)
        {
            _normalEnemyData = normalEnemyData;
            factorType = DamageFactorData.damageFactorType.NormalEnemy__SelfDestruct;
        }

        public void RunEveryFrame(Vector3 position)
        {
            StopOnPausing();
            DestroyLater(position);
            SetConstantVelocity(_normalEnemyData.movingSpeed);
        }
    }
}
