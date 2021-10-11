using UnityEngine;

namespace View
{
    public class Enemy__SpreadBullet : NormalEnemyBase
    {
        private int _id;

        private void Awake()
        {
            CallAtAwake();
        }

        void Start()
        {
            _id = Controller.EnemyController.EmergeEnemy__SpreadBullet(normalEnemyData, this.gameObject);

            Initialize(_id);

            // ControllerからModelクラスのインスタンスを取得
            Model.Enemy__SpreadBullet enemy__SpreadBullet
                = Controller.EnemyController.enemyTable
                    [Controller.EnemyController.enemyType__Rough.SpreadBullet]
                        [_id].enemy__SpreadBullet;

            // 速度の監視
            enemy__SpreadBullet.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                rigidbody2D.velocity = velocity;
            });

            // 消滅の監視
            enemy__SpreadBullet.OnIsBeingDestroyedChanged.AddListener((bool isBeingDestroyed) =>
            {
                this.isBeingDestroyed = isBeingDestroyed;
            });

            // 弾の発射の監視
            enemy__SpreadBullet.OnFiringBulletInfoChanged.AddListener((firingBulletInfo__Collection) =>
            {
                Model.DamageFactorManager.FiringBulletInfo info
                    = Model.FiringInfoConverter.MakeStruct(firingBulletInfo__Collection);

                Fire(info.bulletVelocity, info.firePath);
            });

            // 当たり判定の処理
            playOnEnter += (collision) =>
            {
                if (collision.tag == TagManager.TagNames.BattleScenes__Player.ToString())
                {
                    enemy__SpreadBullet.DealCollisionDamage();
                }
            };

            // ゲーム終了時
            Controller.BattleScenesController.stageStatusManager.OnCurrentStageStatusChanged.AddListener(status =>
            {
                if (status == Model.StageStatusManager.stageStatus.BossDying)
                {
                    isBeingDestroyed = true;
                }
            });
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
                [Controller.EnemyController.enemyType__Rough.SpreadBullet].Remove(_id);
            Destroy(this.gameObject);
        }
    }
}
