using UnityEngine;

namespace View
{
    public class Enemy__SpreadBullet : NormalEnemyBase
    {
        private void Awake()
        {
            CallAtAwake();
        }

        void Start()
        {
            Model.EnemyDamageManager enemyDamageManager = Initialize();

            //実行順序の関係でコンストラクタはStartに書かないといけない
            Model.Enemy__SpreadBullet enemy__SpreadBullet
                = new Model.Enemy__SpreadBullet(
                    Controller.BattleScenesClassController.pauseController,
                    Controller.BattleScenesClassController.playerStatusController,
                    Controller.BattleScenesClassController.enemyController,
                    _normalEnemyData
                    );

            // Controllerのクラスにidやインスタンスの情報を渡す
            Controller.EnemyElements enemyElements = new Controller.EnemyElements()
            {
                enemy__SpreadBullet = enemy__SpreadBullet,
                enemyObject = this.gameObject,
            };
            Controller.EnemyClassController.enemyTable__SpreadBullet.Add(id, enemyElements);
            Controller.EnemyClassController.damageManagerTable.Add(id, enemyDamageManager);


            enemy__SpreadBullet.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                rigidbody2D.velocity = velocity;
            });

            enemy__SpreadBullet.OnIsBeingDestroyedChanged.AddListener((bool isBeingDestroyed) =>
            {
                this.isDying = isBeingDestroyed;
            });

            enemyDamageManager.OnIsDyingChanged.AddListener((bool isDying) =>
            {
                this.isDying = isDying;
            });

            enemy__SpreadBullet.OnFiringBulletInfoChanged.AddListener((firingBulletInfo) =>
            {
                Fire(firingBulletInfo.bulletVelocity, firingBulletInfo.firePath);
            });

            // 当たり判定の処理
            playOnEnter += (collision) =>
            {
                if (collision.tag == "BattleScenes/Player")
                {
                    enemy__SpreadBullet.DealCollisionDamage();
                }
            };
        }

        private void Update()
        {
            if (isDying) Die();
        }

        private void Die()
        {
            Controller.EnemyClassController.enemyTable__SpreadBullet.Remove(id);
            Destroy(this.gameObject);
        }
    }
}
