namespace Model
{
    public class EnemyFire : MovingObjectBase
    {
        private NormalEnemyData _normalEnemyData;
        private PlayerPositionController _playerPositionController;
        private PlayerStatusController _playerStatusController;


        public EnemyFire(NormalEnemyData normalEnemyData, PlayerStatusController playerStatusController, PlayerPositionController playerPositionController, PauseController pauseController) : base(pauseController)
        {
            _normalEnemyData = normalEnemyData;
            _playerPositionController = playerPositionController;
            _playerStatusController = playerStatusController;
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
    }
}
