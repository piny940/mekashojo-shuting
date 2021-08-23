using UnityEngine;

namespace View
{
    public class EnemyFire__WideBeam : CollisionBase
    {
        [SerializeField, Header("NormalEnemyDataを入れる")] private Model.NormalEnemyData _normalEnemyData;

        private void Start()
        {
            Model.EnemyFire enemyFire = new Model.EnemyFire(
                _normalEnemyData,
                Controller.BattleScenesClassController.playerStatusController,
                Controller.BattleScenesClassController.playerPositionController,
                Controller.BattleScenesClassController.pauseController
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
