using UnityEngine;

namespace View
{
    public class LastBossFire : CollisionBase
    {
        [SerializeField, Header("攻撃のタイプを選ぶ")] private Model.Enemy__LastBoss.attackType _type;
        [SerializeField, Header("攻撃時になる音を入れる")] private AudioClip _fireSound;
        private int _id;
        private Rigidbody2D _rigidbody2D;
        private bool _isBeingDestroyed;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            if (_type == Model.Enemy__LastBoss.attackType.ThickBeam
                || _type == Model.Enemy__LastBoss.attackType.SpreadBeam
                || _type == Model.Enemy__LastBoss.attackType.SpreadLaserWithMissile
                || _type == Model.Enemy__LastBoss.attackType.SpreadLaserWithStun)
            {
                // タイプがビーム系の場合
                EmergeBeam(_type);
            }
            else
            {
                //タイプが弾丸系の場合
                EmergeBullet(_type);
            }

            SEPlayer.sePlayer.PlayOneShot(_fireSound);
        }

        // AddListenerにDie()を書くとforeachのループの中で「ループに使っているテーブル」に変更を入れてしまい、
        // "Collection was modified; enumeration operation may not execute."と言われるので
        // Updateで死ぬ処理を行う
        private void Update()
        {
            if (_isBeingDestroyed)
            {
                Die();
            }
        }

        private void Die()
        {
            Controller.EnemyController.fireTable__Bullet.Remove(_id);
            Destroy(this.gameObject);
        }

        // タイプがビーム系の場合にStartメソッドで呼ぶ
        private void EmergeBeam(Model.Enemy__LastBoss.attackType type)
        {
            Model.EnemyFire.FireInfo fireInfo = new Model.EnemyFire.FireInfo()
            {
                damageAmount = Model.Enemy__LastBoss.damageAmounts[type],
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

            playOnEnter += collision =>
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
        private void EmergeBullet(Model.Enemy__LastBoss.attackType type)
        {
            Model.EnemyFire.FireInfo fireInfo = new Model.EnemyFire.FireInfo()
            {
                bulletSpeed = Model.Enemy__LastBoss.bulletSpeeds[type],
                damageAmount = Model.Enemy__LastBoss.damageAmounts[type],
            };

            switch (type)
            {
                case Model.Enemy__LastBoss.attackType.GuidedMissile:
                    fireInfo.type = Model.EnemyFire.fireType.GuidedBullet;
                    fireInfo.disappearTime = Model.Enemy__LastBoss.guidedMissileDisappearTime;
                    break;

                case Model.Enemy__LastBoss.attackType.SpreadStunBullet:
                    fireInfo.type = Model.EnemyFire.fireType.StunBullet;
                    break;

                case Model.Enemy__LastBoss.attackType.SpreadBomb:
                    fireInfo.disappearTime = Model.Enemy__LastBoss.spreadBombDisappearTime;
                    fireInfo.type = Model.EnemyFire.fireType.Barrage;
                    break;

                case Model.Enemy__LastBoss.attackType.SpreadBalkan:
                    fireInfo.type = Model.EnemyFire.fireType.Barrage;
                    fireInfo.disappearTime = Model.Enemy__LastBoss.spreadBalkanDisappearTime;
                    break;

                case Model.Enemy__LastBoss.attackType.Slash:
                    fireInfo.type = Model.EnemyFire.fireType.NormalBullet;
                    break;

                default:
                    throw new System.Exception();
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
