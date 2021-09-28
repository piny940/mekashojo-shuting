using UnityEngine;

namespace View
{
    public class NormalEnemyFire__WideBeam : CollisionBase
    {
        [SerializeField, Header("NormalEnemyDataを入れる")] private Controller.NormalEnemyData _normalEnemyData;

        private void Start()
        {
            Model.EnemyFire.FireInfo fireInfo = new Model.EnemyFire.FireInfo()
            {
                damageAmount = _normalEnemyData.damageAmount,
                type = Model.EnemyFire.fireType.Beam,
            };

            Model.EnemyFire enemyFire = new Model.EnemyFire(
                fireInfo,
                Vector3.zero,
                Controller.BattleScenesController.enemyManager,
                Controller.BattleScenesController.playerDebuffManager,
                Controller.BattleScenesController.playerStatusManager,
                Controller.BattleScenesController.shield__Player,
                Controller.BattleScenesController.stageStatusManager
                );

            playOnEnter += (collision) =>
            {
                if (collision.tag == TagManager.TagNames.BattleScenes__Player.ToString())
                {
                    enemyFire.Attack();
                }
            };
        }
    }
}
