using System.Collections.ObjectModel;
using UnityEngine;

namespace Model
{
    public class Enemy__SelfDestruct : DamageFactorManager
    {
        private Controller.NormalEnemyData _normalEnemyData;
        protected override DamageFactorData.damageFactorType factorType { get; set; }
        protected override void FireBullet(ObservableCollection<object> firingBulletInfo) { }

        public Enemy__SelfDestruct(PauseManager pauseManager, PlayerStatusManager playerStatusManager, EnemyManager enemyManager, Controller.NormalEnemyData normalEnemyData)
                : base(pauseManager, enemyManager, playerStatusManager)
        {
            _normalEnemyData = normalEnemyData;
            factorType = DamageFactorData.damageFactorType.NormalEnemy__SelfDestruct;
        }

        public void RunEveryFrame(Vector3 position)
        {
            StopOnPausing();
            DestroyIfOutside(position);
            SetConstantVelocity(_normalEnemyData.movingSpeed);
        }
    }
}
