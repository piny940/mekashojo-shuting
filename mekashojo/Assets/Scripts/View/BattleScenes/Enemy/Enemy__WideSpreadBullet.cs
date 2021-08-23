using UnityEngine;

namespace View
{
    public class Enemy__WideSpreadBullet : NormalEnemyBase
    {
        private void Awake()
        {
            CallAtAwake();
        }

        void Start()
        {
            Model.EnemyDamageManager enemyDamageManager = Initialize();

            //実行順序の関係でコンストラクタはStartに書かないといけない
            Model.Enemy__WideSpreadBullet enemy__WideSpreadBullet
                = new Model.Enemy__WideSpreadBullet(
                    Controller.BattleScenesClassController.pauseController,
                    Controller.BattleScenesClassController.playerStatusController,
                    Controller.BattleScenesClassController.enemyController,
                    _normalEnemyData
                    );

            // Controllerのクラスにidやインスタンスの情報を渡す
            Controller.EnemyElements enemyElements = new Controller.EnemyElements()
            {
                enemy__WideSpreadBullet = enemy__WideSpreadBullet,
                enemyObject = this.gameObject,
            };
            Controller.EnemyClassController.enemyTable__WideSpreadBullet.Add(id, enemyElements);
            Controller.EnemyClassController.damageManagerTable.Add(id, enemyDamageManager);


            enemy__WideSpreadBullet.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                rigidbody2D.velocity = velocity;
            });

            enemy__WideSpreadBullet.OnIsDestroyedChanged.AddListener((bool isDead) =>
            {
                this.isDead = isDead;
            });

            enemyDamageManager.OnIsDeadChanged.AddListener((bool isDead) =>
            {
                this.isDead = isDead;
            });

            enemy__WideSpreadBullet.FireBullet = Fire;

            // 当たり判定の処理
            playOnEnter += (collision) =>
            {
                if (collision.tag == "BattleScenes/Player")
                {
                    enemy__WideSpreadBullet.DoDamage();
                }
            };
        }

        private void Update()
        {
            if (isDead) Die();
        }

        private void Die()
        {
            Controller.EnemyClassController.enemyTable__WideSpreadBullet.Remove(id);
            Destroy(this.gameObject);
        }
    }
}
