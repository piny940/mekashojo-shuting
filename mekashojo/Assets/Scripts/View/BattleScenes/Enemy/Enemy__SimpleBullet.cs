using UnityEngine;

namespace View
{
    // プレイヤーの方に向かって弾を飛ばす敵、すなわち
    // FastBullet, SlowBullet, SingleBullet, Missile
    // RepeatedBullet, StunBullet, GuidedBulletはこのクラスを用いる
    public class Enemy__SimpleBullet : NormalEnemyBase
    {
        private int _id;

        private void Awake()
        {
            CallAtAwake();
        }

        void Start()
        {
            _id = Controller.EnemyController.EmergeEnemy__SimpleBullet(normalEnemyData, this.gameObject);

            Initialize(_id);

            // ControllerからModelクラスのインスタンスを取得
            Model.Enemy__SimpleBullet enemy__SimpleBullet
                = Controller.EnemyController.enemyTable
                    [Controller.EnemyController.enemyType__Rough.SimpleBullet]
                        [_id].enemy__SimpleBullet;

            // 速度の監視
            enemy__SimpleBullet.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                rigidbody2D.velocity = velocity;
            });

            // 消滅の監視
            enemy__SimpleBullet.OnIsBeingDestroyedChanged.AddListener((bool isBeingDestroyed) =>
            {
                this.isBeingDestroyed = isBeingDestroyed;
            });

            // 弾の発射の監視
            enemy__SimpleBullet.OnFiringBulletInfoChanged.AddListener((firingBulletInfo__Collection) =>
            {
                Model.DamageFactorManager.FiringBulletInfo info
                     = Model.FiringInfoConverter.MakeStruct(firingBulletInfo__Collection);

                Fire(info.bulletVelocity, info.firePath);
            });

            // 当たり判定の処理
            playOnEnter += (collision) =>
            {
                if (collision.tag == "BattleScenes/Player")
                {
                    enemy__SimpleBullet.DealCollisionDamage();
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
