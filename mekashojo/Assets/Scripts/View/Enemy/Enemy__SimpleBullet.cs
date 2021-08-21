namespace View
{
    // プレイヤーの方に向かって弾を飛ばす敵、すなわち
    // FastBullet, SlowBullet, SingleBullet,
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
                enemyDamageManager = enemyDamageManager,
                enemyObject = this.gameObject,
            };
            Controller.EnemyClassController.enemyElements__SimpleBullet.Add(id, enemyElements);


            enemy__SimpleBullet.OnVelocityChanged.AddListener((velocity) =>
            {
                rigidbody2D.velocity = velocity;
            });

            enemy__SimpleBullet.OnIsDestroyedChanged.AddListener((isDead) =>
            {
                if (isDead) { Die(); }
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
    }
}
