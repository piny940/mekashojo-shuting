using UnityEngine;

namespace View
{
    public class EnemyFire__WideBeam : CollisionBase
    {
        [SerializeField, Header("NormalEnemyDataを入れる")] private Controller.NormalEnemyData _normalEnemyData;

        private void Start()
        {
            Model.EnemyFire enemyFire = new Model.EnemyFire(
                _normalEnemyData,
                Controller.BattleScenesController.enemyManager,
                Controller.BattleScenesController.playerStatusManager,
                Controller.BattleScenesController.playerPositionManager,
                Controller.BattleScenesController.pauseManager
                );

            playOnEnter += (collision) =>
            {
                if (collision.tag == "BattleScenes/Player")
                {
                    enemyFire.Attack();
                }
            };
        }
    }
}
