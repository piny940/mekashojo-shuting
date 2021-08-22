using UnityEngine;

namespace Model
{
    public class EnemyFire : MovingObjectBase
    {
        private NormalEnemyData _normalEnemyData;
        private PlayerPositionController _playerPositionController;
        private PlayerStatusController _playerStatusController;
        private const float STOP_CHASING_DISTANCE = 3;
        private bool _hasApproached = false;


        public EnemyFire(NormalEnemyData normalEnemyData, PlayerStatusController playerStatusController, PlayerPositionController playerPositionController, PauseController pauseController) : base(pauseController)
        {
            _normalEnemyData = normalEnemyData;
            _playerPositionController = playerPositionController;
            _playerStatusController = playerStatusController;
        }

        public void RunEveryFrame(Vector3 thisPosition, Vector3 playerPosition)
        {
            StopOnPausing();
            DestroyLater(thisPosition);

            if (_normalEnemyData.type == NormalEnemyData.normalEnemyType.GuidedBullet)
            {
                ChasePlayer(thisPosition, playerPosition);
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
                    isDestroyed = true;
                    break;

            }
        }

        // Playerを追跡する
        private void ChasePlayer(Vector3 thisPosition, Vector3 playerPosition)
        {
            if (!pauseController.isGameGoing) return;


            Vector3 adjustedPlayerPosition = new Vector3(playerPosition.x, playerPosition.y, EnemyController.enemyPosition__z);

            float distance = Vector3.Magnitude(adjustedPlayerPosition - thisPosition);

            if (!_hasApproached)
            {
                velocity = (adjustedPlayerPosition - thisPosition) * _normalEnemyData.bulletSpeed / distance;
            }

            if (distance < STOP_CHASING_DISTANCE)
            {
                _hasApproached = true;
            }
        }
    }
}
