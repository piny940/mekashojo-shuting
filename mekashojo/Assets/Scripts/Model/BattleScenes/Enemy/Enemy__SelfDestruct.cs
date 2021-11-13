using UnityEngine;

namespace Model
{
    public class Enemy__SelfDestruct : DamageFactorManager
    {
        private Controller.NormalEnemyData _normalEnemyData;
        protected override DamageFactorData.damageFactorType factorType { get; set; }
        protected override void ChangeBeamStatus(beamFiringProcesses process) { }

        public Enemy__SelfDestruct(StageStatusManager stageStatusManager, PlayerStatusManager playerStatusManager, EnemyManager enemyManager, Controller.NormalEnemyData normalEnemyData)
                : base(stageStatusManager, enemyManager, playerStatusManager)
        {
            _normalEnemyData = normalEnemyData;
            factorType = DamageFactorData.damageFactorType.NormalEnemy__SelfDestruct;
        }

        public void RunEveryFrame(Vector3 position)
        {
            StopOnPausing();
            DisappearIfOutside(position);
            SetConstantVelocity(_normalEnemyData.movingSpeed);
        }
    }
}
