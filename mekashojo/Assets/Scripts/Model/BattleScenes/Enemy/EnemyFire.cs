using UnityEngine;

namespace Model
{
    public class EnemyFire : MovingObjectBase
    {
        private const float STOP_CHASING_DISTANCE = 8;
        private const float START_CHASING_TIME = 0.5f;
        private const float STUN_DURATION = 2;
        private const float CHASING_RATE = 1;
        private bool _hasApproached = false;
        private float _chasingTime = 0;
        private FireInfo _fireInfo;
        private PlayerStatusManager _playerStatusManager;
        private PlayerDebuffManager _playerDebuffManager;
        private Shield__Player _shield__Player;

        protected override movingObjectType objectType { get; set; }

        public enum fireType
        {
            NormalBullet,
            StunBullet,
            GuidedBullet,
            Beam,
            Barrage, // 弾幕
        }

        public struct FireInfo
        {
            public fireType type;
            public float damageAmount;
            public float bulletSpeed;
            public float disappearTime;
        }

        public EnemyFire(FireInfo fireInfo, Vector3 initialVelocity, EnemyManager enemyManager, PlayerDebuffManager playerDebuffManager, PlayerStatusManager playerStatusManager, Shield__Player shield__Player, StageStatusManager stageStatusManager)
                : base(enemyManager, stageStatusManager)
        {
            _fireInfo = fireInfo;
            velocity = initialVelocity;
            _playerDebuffManager = playerDebuffManager;
            _playerStatusManager = playerStatusManager;
            _shield__Player = shield__Player;
            objectType = movingObjectType.EnemyFire;
            disappearTime = _fireInfo.disappearTime;
        }

        public void RunEveryFrame(Vector3 position, Vector3 playerPosition)
        {
            StopOnPausing();
            DisappearIfOutside(position);

            if (_fireInfo.type == fireType.GuidedBullet)
                ChasePlayer(position, playerPosition);

            if (_fireInfo.type == fireType.Barrage)
                DisappearLater();
        }

        public void Attack()
        {
            _playerStatusManager.GetDamage(_fireInfo.damageAmount);

            switch (_fireInfo.type)
            {
                //スタン型の場合は
                case fireType.StunBullet:
                    isBeingDestroyed = true;
                    //盾を使用していなかったらスタンさせる
                    if (!_shield__Player.isUsingShield)
                        _ = _playerDebuffManager.AddDebuff(PlayerDebuffManager.debuffTypes.Stun, STUN_DURATION);
                    break;

                //ビームの場合は何もしない
                case fireType.Beam:
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
            if (!stageStatusManager.isGameGoing) return;

            if (_chasingTime < START_CHASING_TIME)
            {
                _chasingTime += Time.deltaTime;
                return;
            }

            Vector3 adjustedPlayerPosition = new Vector3(playerPosition.x, playerPosition.y, EnemyManager.enemyPosition__z);

            float distance = Vector3.Magnitude(adjustedPlayerPosition - position);

            Vector3 direction = (adjustedPlayerPosition - position) / distance * CHASING_RATE + velocity;

            if (!_hasApproached)
            {
                velocity = direction * _fireInfo.bulletSpeed / Vector3.Magnitude(direction);
            }

            // 一定距離まで近づいたら追跡をやめる
            if (distance < STOP_CHASING_DISTANCE)
            {
                _hasApproached = true;
            }
        }
    }
}
