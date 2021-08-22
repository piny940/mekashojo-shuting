using UnityEngine;

namespace View
{
    // プレイヤーの方に向かって弾を飛ばす敵、すなわち
    // FastBullet, SlowBullet, SingleBullet, Missile
    // RepeatedBullet, StunBullet, GuidedBulletはこのクラスを用いる
    public class Enemy__SimpleBullet : NormalEnemyBase
    {
        private void Awake()
        {
            CallAtAwake();
        }

        void Start()
        {
            Model.EnemyDamageManager enemyDamageManager = Initialize();

            //実行順序の関係でコンストラクタはStartに書かないといけない
            Model.Enemy__SimpleBullet enemy__SimpleBullet
                = new Model.Enemy__SimpleBullet(
                    Controller.ModelClassController.pauseController,
                    Controller.ModelClassController.playerStatusController,
                    Controller.ModelClassController.enemyController,
                    _normalEnemyData
                    );

            // Controllerのクラスにidやインスタンスの情報を渡す
            Controller.EnemyElements enemyElements = new Controller.EnemyElements()
            {
                enemy__SimpleBullet = enemy__SimpleBullet,
                enemyObject = this.gameObject,
            };
            Controller.EnemyClassController.enemyTable__SimpleBullet.Add(id, enemyElements);
            Controller.EnemyClassController.damageManagerTable.Add(id, enemyDamageManager);


            enemy__SimpleBullet.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                rigidbody2D.velocity = velocity;
            });

            enemy__SimpleBullet.OnIsDestroyedChanged.AddListener((bool isDead) =>
            {
                this.isDead = isDead;
            });

            enemyDamageManager.OnIsDeadChanged.AddListener((bool isDead) =>
            {
                this.isDead = isDead;
            });

            enemy__SimpleBullet.FireBullet = Fire;

            // 当たり判定の処理
            playOnEnter += (collision) =>
            {
                if (collision.tag == "BattleScenes/Player")
                {
                    enemy__SimpleBullet.DoDamage();
                }
            };
        }

        private void Update()
        {
            if (isDead) Die();
        }

        private void Die()
        {
            Controller.EnemyClassController.enemyTable__SimpleBullet.Remove(id);
            Destroy(this.gameObject);
        }
    }
}
