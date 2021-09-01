using UnityEngine;

namespace View
{
    public class Enemy__SelfDestruct : NormalEnemyBase
    {
        private Animator _animator;
        private int _id;

        private void Awake()
        {
            CallAtAwake();
            _animator = GetComponent<Animator>();
        }

        void Start()
        {
            _id = Controller.EnemyController.EmergeEnemy__SelfDestruct(normalEnemyData, this.gameObject);

            // ControllerからModelクラスのインスタンスを取得
            Model.Enemy__SelfDestruct enemy__SelfDestruct
                = Controller.EnemyController.enemyTable
                    [Controller.EnemyController.enemyType__Rough.SelfDestruct]
                        [_id].enemy__SelfDestruct;

            // 速度の監視
            enemy__SelfDestruct.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                rigidbody2D.velocity = velocity;
            });

            // アニメーションの開始/停止の監視
            enemy__SelfDestruct.OnIsMovingChanged.AddListener((bool isMoving) =>
            {
                if (isMoving)
                {
                    _animator.SetFloat("speed", 1);
                }
                else
                {
                    _animator.SetFloat("speed", 0);
                }
            });

            // 消滅の監視
            enemy__SelfDestruct.OnIsBeingDestroyedChanged.AddListener((bool isDying) =>
            {
                this.isBeingDestroyed = isDying;
            });

            // 当たり判定の処理
            playOnEnter += (collision) =>
            {
                if (collision.tag == "BattleScenes/Player")
                {
                    enemy__SelfDestruct.DealCollisionDamage();
                }
            };
        }

        // AddListenerにDie()を書くとforeachのループの中で「ループに使っているテーブル」に変更を入れてしまい、
        // "Collection was modified; enumeration operation may not execute."と言われるので
        // Updateで死ぬ処理を行う
        private void Update()
        {
            if (isBeingDestroyed) Die();
        }

        private void Die()
        {
            Controller.EnemyController.enemyTable
                [Controller.EnemyController.enemyType__Rough.SimpleBullet].Remove(_id);
            Destroy(this.gameObject);
        }
    }
}
