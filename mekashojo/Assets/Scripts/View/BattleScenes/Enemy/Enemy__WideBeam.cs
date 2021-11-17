using UnityEngine;

namespace View
{
    public class Enemy__WideBeam : NormalEnemyBase
    {
        private int _id;
        [SerializeField, Header("EnemyFire__WideBeamを入れる")] private GameObject _enemyFire__WideBeam;

        private void Awake()
        {
            //初期化
            _enemyFire__WideBeam.SetActive(false);

            CallAtAwake();
        }

        // Start is called before the first frame update
        void Start()
        {
            _id = Controller.EnemyController.EmergeEnemy__WideBeam(normalEnemyData, this.gameObject);

            Initialize(_id);

            // ControllerからModelクラスのインスタンスを取得
            Model.Enemy__WideBeam enemy__WideBeam
                = Controller.EnemyController.enemyTable
                    [Controller.EnemyController.enemyType__Rough.WideBeam]
                        [_id].enemy__WideBeam;

            // 速度の監視
            enemy__WideBeam.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                rigidbody2D.velocity = velocity;
            });

            // 消滅の監視
            enemy__WideBeam.OnIsBeingDestroyedChanged.AddListener((bool isBeingDestroyed) =>
            {
                this.isBeingDestroyed = isBeingDestroyed;
            });

            // 当たり判定の処理
            playOnEnter += (collision) =>
            {
                if (collision.tag == TagManager.TagNames.BattleScenes__Player.ToString())
                {
                    enemy__WideBeam.DealCollisionDamage();
                }
            };

            // ビームの状態変化の監視
            enemy__WideBeam.OnBeamStatusChanged.AddListener(ChangeBeamStatus);

            // ゲーム終了時
            Controller.BattleScenesController.stageStatusManager.OnCurrentStageStatusChanged.AddListener(status =>
            {
                if (status == Model.StageStatusManager.stageStatus.BossDying)
                {
                    isBeingDestroyed = true;
                }
            });
        }

        private void ChangeBeamStatus(Model.DamageFactorManager.beamFiringProcesses beamStatus)
        {
            if (beamStatus == Model.DamageFactorManager.beamFiringProcesses.IsFiringBeam)
            {
                //攻撃をする
                _enemyFire__WideBeam.SetActive(true);
            }
            else if (beamStatus == Model.DamageFactorManager.beamFiringProcesses.HasStoppedBeam)
            {
                _enemyFire__WideBeam.SetActive(false);
            }
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
                [Controller.EnemyController.enemyType__Rough.WideBeam].Remove(_id);
            Destroy(this.gameObject);
        }
    }
}
