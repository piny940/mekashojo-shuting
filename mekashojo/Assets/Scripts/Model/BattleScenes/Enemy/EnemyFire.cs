using UnityEngine;

namespace Model
{
    public class EnemyFire : MovingObjectBase
    {
        private const float STOP_CHASING_DISTANCE = 3;
        private bool _hasApproached = false;
        private Controller.NormalEnemyData _normalEnemyData;
        private PlayerPositionManager _playerPositionManager;
        private PlayerStatusManager _playerStatusManager;

        protected override movingObjectType objectType { get; set; }

        public EnemyFire(Controller.NormalEnemyData normalEnemyData, EnemyManager enemyManager, PlayerStatusManager playerStatusManager, PlayerPositionManager playerPositionManager, PauseManager pauseManager) : base(enemyManager, pauseManager)
        {
            _normalEnemyData = normalEnemyData;
            _playerPositionManager = playerPositionManager;
            _playerStatusManager = playerStatusManager;
            objectType = movingObjectType.EnemyFire;
        }

        public void RunEveryFrame(Vector3 position, Vector3 playerPosition)
        {
            StopOnPausing();
            DestroyLater(position);

            if (_normalEnemyData.type == Controller.NormalEnemyData.normalEnemyType.GuidedBullet)
            {
                ChasePlayer(position, playerPosition);
            }
        }

        public void Attack()
        {
            _playerStatusManager.GetDamage(_normalEnemyData.damageAmount);

            switch (_normalEnemyData.type)
            {
                //スタン型の場合は
                case Controller.NormalEnemyData.normalEnemyType.StunBullet:
                    //スタンさせる
                    _playerPositionManager.isStunning = true;
                    isBeingDestroyed = true;
                    break;

                //全方位ビームの場合は何もしない
                case Controller.NormalEnemyData.normalEnemyType.WideBeam:
                    break;

                //それ以外ならプレイヤーに当たったら消滅する
                default:
                    isBeingDestroyed = true;
                    break;

            }
        }

        // Playerを追跡する
        private void ChasePlayer(Vector3 position, Vector3 playerPosition)
        {
            if (!pauseManager.isGameGoing) return;

            Vector3 adjustedPlayerPosition = new Vector3(playerPosition.x, playerPosition.y, EnemyManager.enemyPosition__z);

            float distance = Vector3.Magnitude(adjustedPlayerPosition - position);

            if (!_hasApproached)
            {
                velocity = (adjustedPlayerPosition - position) * _normalEnemyData.bulletSpeed / distance;
            }

            if (distance < STOP_CHASING_DISTANCE)
            {
                _hasApproached = true;
            }
        }
    }
}
