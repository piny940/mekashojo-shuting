using UnityEngine;

namespace Model
{
    public class EnemyFire : MovingObjectBase
    {
        private const float STOP_CHASING_DISTANCE = 3;
        private bool _hasApproached = false;
        private NormalEnemyData _normalEnemyData;
        private PlayerPositionController _playerPositionController;
        private PlayerStatusController _playerStatusController;

        protected override movingObjectType objectType { get; set; }

        public EnemyFire(NormalEnemyData normalEnemyData, EnemyController enemyController, PlayerStatusController playerStatusController, PlayerPositionController playerPositionController, PauseController pauseController) : base(enemyController, pauseController)
        {
            _normalEnemyData = normalEnemyData;
            _playerPositionController = playerPositionController;
            _playerStatusController = playerStatusController;
            objectType = movingObjectType.EnemyFire;
        }

        public void RunEveryFrame(Vector3 position, Vector3 playerPosition)
        {
            StopOnPausing();
            DestroyLater(position);

            if (_normalEnemyData.type == NormalEnemyData.normalEnemyType.GuidedBullet)
            {
                ChasePlayer(position, playerPosition);
            }
        }

        public void Attack()
        {
            _playerStatusController.ChangeHP(_normalEnemyData.damageAmount);

            switch (_normalEnemyData.type)
            {
                //スタン型の場合は
                case NormalEnemyData.normalEnemyType.StunBullet:
                    //スタンさせる
                    _playerPositionController.isStunning = true;
                    break;

                //全方位ビームの場合は何もしない
                case NormalEnemyData.normalEnemyType.WideBeam:
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
            if (!pauseController.isGameGoing) return;

            Vector3 adjustedPlayerPosition = new Vector3(playerPosition.x, playerPosition.y, EnemyController.enemyPosition__z);

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
