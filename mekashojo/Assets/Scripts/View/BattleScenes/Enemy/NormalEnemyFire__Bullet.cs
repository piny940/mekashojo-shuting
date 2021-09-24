using UnityEngine;

namespace View
{
    public class NormalEnemyFire__Bullet : CollisionBase
    {
        [SerializeField, Header("NormalEnemyDataを入れる")] Controller.NormalEnemyData _normalEnemyData;
        private int _id;
        private Rigidbody2D _rigidbody2D;
        private bool _isBeingDestroyed;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            Model.EnemyFire.FireInfo fireInfo = new Model.EnemyFire.FireInfo()
            {
                bulletSpeed = _normalEnemyData.bulletSpeed,
                damageAmount = _normalEnemyData.damageAmount,
            };

            switch (_normalEnemyData.type)
            {
                case Controller.NormalEnemyData.normalEnemyType.StunBullet:
                    fireInfo.type = Model.EnemyFire.fireType.StunBullet;
                    break;

                case Controller.NormalEnemyData.normalEnemyType.GuidedBullet:
                    fireInfo.type = Model.EnemyFire.fireType.GuidedBullet;
                    break;

                case Controller.NormalEnemyData.normalEnemyType.WideBeam:
                    // このクラスはビーム型の敵用のクラスではないのでエラーを出す
                    throw new System.Exception();

                default:
                    fireInfo.type = Model.EnemyFire.fireType.NormalBullet;
                    break;
            }

            _id = Controller.EnemyController.EmergeEnemyBullet(fireInfo, this.gameObject);

            // ControllerからModelクラスのインスタンスを取得
            Model.EnemyFire enemyFire = Controller.EnemyController.fireTable__Bullet[_id].enemyFire;

            // 速度の監視
            enemyFire.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                _rigidbody2D.velocity = velocity;

                //弾の回転
                if (velocity != Vector3.zero)
                {
                    float theta = Vector3.SignedAngle(new Vector3(1, 0, 0), new Vector3(velocity.x, velocity.y, 0), new Vector3(0, 0, 1));
                    transform.localEulerAngles = new Vector3(0, 0, theta);
                }
            });

            // 消滅の監視
            enemyFire.OnIsBeingDestroyedChanged.AddListener((bool isBeingDestroyed) =>
            {
                _isBeingDestroyed = isBeingDestroyed;
            });

            // 当たり判定の処理
            playOnEnter += (Collider2D collision) =>
            {
                if (collision.tag == TagManager.TagNames.BattleScenes__Player.ToString())
                {
                    enemyFire.Attack();
                }
            };
        }

        // AddListenerにDie()を書くとforeachのループの中で「ループに使っているテーブル」に変更を入れてしまい、
        // "Collection was modified; enumeration operation may not execute."と言われるので
        // Updateで死ぬ処理を行う
        private void Update()
        {
            if (_isBeingDestroyed) Die();
        }

        private void Die()
        {
            Controller.EnemyController.fireTable__Bullet.Remove(_id);
            Destroy(this.gameObject);
        }
    }
}
