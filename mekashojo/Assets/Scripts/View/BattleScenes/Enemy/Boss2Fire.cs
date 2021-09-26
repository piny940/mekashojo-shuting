using UnityEngine;

namespace View
{
    public class Boss2Fire : CollisionBase
    {
        [SerializeField, Header("攻撃のタイプを選ぶ")] private Model.Enemy__Boss2.attackType _type;
        private int _id;
        private Rigidbody2D _rigidbody2D;
        private bool _isBeingDestroyed;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            if (_type == Model.Enemy__Boss2.attackType.SpreadLaser)
                // タイプがレーザー系の場合
                EmergeBeam(_type);
            //タイプが弾丸系の場合
            else EmergeBullet(_type);
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

        // タイプがビーム系の場合にStartメソッドで呼ぶ
        private void EmergeBeam(Model.Enemy__Boss2.attackType type)
        {
            Model.EnemyFire.FireInfo fireInfo = new Model.EnemyFire.FireInfo()
            {
                damageAmount = Model.Enemy__Boss2.damageAmounts[type],
                type = Model.EnemyFire.fireType.Beam,
            };

            Model.EnemyFire enemyFire = new Model.EnemyFire(
                fireInfo,
                Vector3.zero,
                Controller.BattleScenesController.enemyManager,
                Controller.BattleScenesController.playerDebuffManager,
                Controller.BattleScenesController.playerStatusManager,
                Controller.BattleScenesController.shield__Player,
                Controller.BattleScenesController.stageStatusManager
                );

            playOnEnter += (collision) =>
            {
                if (collision.tag == TagManager.TagNames.BattleScenes__Player.ToString())
                {
                    enemyFire.Attack();
                }
            };

            // ゲーム終了時
            Controller.BattleScenesController.stageStatusManager.OnCurrentStageStatusChanged.AddListener(status =>
            {
                if (status == Model.StageStatusManager.stageStatus.BossDying)
                {
                    this.gameObject.SetActive(false);
                }
            });
        }

        // タイプが弾丸系の場合にStartメソッドで呼ぶ
        private void EmergeBullet(Model.Enemy__Boss2.attackType type)
        {
            Model.EnemyFire.FireInfo fireInfo = new Model.EnemyFire.FireInfo()
            {
                bulletSpeed = Model.Enemy__Boss2.bulletSpeeds[type],
                damageAmount = Model.Enemy__Boss2.damageAmounts[type],
            };

            fireInfo.type = Model.EnemyFire.fireType.Barrage;

            switch (type)
            {
                case Model.Enemy__Boss2.attackType.SpreadMissile:
                    fireInfo.disappearTime = Model.Enemy__Boss2.spreadMissileDisappearTime;
                    break;

                case Model.Enemy__Boss2.attackType.SpreadBalkan:
                    fireInfo.disappearTime = Model.Enemy__Boss2.spreadBalkanDisappearTime;
                    break;
            }

            _id = Controller.EnemyController.EmergeEnemyBullet(
                fireInfo,
                _rigidbody2D.velocity,
                this.gameObject
                );

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

            // ゲーム終了時
            Controller.BattleScenesController.stageStatusManager.OnCurrentStageStatusChanged.AddListener(status =>
            {
                if (status == Model.StageStatusManager.stageStatus.BossDying)
                {
                    _isBeingDestroyed = true;
                }
            });
        }
    }
}
