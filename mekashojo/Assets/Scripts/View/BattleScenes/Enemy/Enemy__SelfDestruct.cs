using UnityEngine;

namespace View
{
    public class Enemy__SelfDestruct : NormalEnemyBase
    {
        private Animator _animator;

        private void Awake()
        {
            CallAtAwake();
            _animator = GetComponent<Animator>();
        }

        void Start()
        {
            Model.EnemyDamageManager enemyDamageManager = Initialize();

            //実行順序の関係でコンストラクタはStartに書かないといけない
            Model.Enemy__SelfDestruct enemy__SelfDestruct
                = new Model.Enemy__SelfDestruct(
                    Controller.ModelClassController.pauseController,
                    Controller.ModelClassController.playerStatusController,
                    Controller.ModelClassController.enemyController,
                    _normalEnemyData
                    );

            // Controllerのクラスにidやインスタンスの情報を渡す
            Controller.EnemyElements enemyElements = new Controller.EnemyElements()
            {
                enemy__SelfDestruct = enemy__SelfDestruct,
                enemyObject = this.gameObject,
            };
            Controller.EnemyClassController.enemyTable__SelfDestruct.Add(id, enemyElements);
            Controller.EnemyClassController.damageManagerTable.Add(id, enemyDamageManager);


            enemy__SelfDestruct.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                rigidbody2D.velocity = velocity;
            });

            enemy__SelfDestruct.OnIsAnimationPlayingChanged.AddListener((bool isAnimationPlaying) =>
            {
                if (isAnimationPlaying)
                {
                    _animator.SetFloat("speed", 1);
                }
                else
                {
                    _animator.SetFloat("speed", 0);
                }
            });

            enemy__SelfDestruct.OnIsDestroyedChanged.AddListener((bool isDead) =>
            {
                this.isDead = isDead;
            });

            enemyDamageManager.OnIsDeadChanged.AddListener((bool isDead) =>
            {
                this.isDead = isDead;
            });

            // 当たり判定の処理
            playOnEnter += (collision) =>
            {
                if (collision.tag == "BattleScenes/Player")
                {
                    enemy__SelfDestruct.DoDamage();
                }
            };
        }

        private void Update()
        {
            if (isDead) Die();
        }

        private void Die()
        {
            Controller.EnemyClassController.enemyTable__SelfDestruct.Remove(id);
            Destroy(this.gameObject);
        }
    }
}
