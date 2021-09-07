using UnityEngine;

namespace View
{
    public class Enemy__WideBeam : NormalEnemyBase
    {
        private const float NOTIFYING_FIRE_TRANSPARENCY = 0.1f;
        private int _id;
        private PolygonCollider2D _polygonCollier2D;
        private SpriteRenderer _spriteRenderer;
        [SerializeField, Header("EmemyFire__WideBeamを入れる")] private GameObject _enemyFire__WideBeam;

        private void Awake()
        {
            _polygonCollier2D = _enemyFire__WideBeam.GetComponent<PolygonCollider2D>();
            _spriteRenderer = _enemyFire__WideBeam.GetComponent<SpriteRenderer>();

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
            enemy__WideBeam.OnBeamStatusChanged.AddListener(OnBeamStatusChanged);
        }

        private void OnBeamStatusChanged(Model.DamageFactorManager.beamFiringProcesses beamStatus)
        {
            if (beamStatus == Model.DamageFactorManager.beamFiringProcesses.IsNotifyingBeamFiring)
            {
                // 攻撃の予告をする

                _enemyFire__WideBeam.SetActive(true);

                //当たり判定はなくしておく
                _polygonCollier2D.enabled = false;

                //薄く表示させる
                _spriteRenderer.color = new Color(1, 1, 1, NOTIFYING_FIRE_TRANSPARENCY);
            }
            else if (beamStatus == Model.DamageFactorManager.beamFiringProcesses.IsFiringBeam)
            {
                //攻撃をする
                //当たり判定をOnにする
                _polygonCollier2D.enabled = true;

                //ちゃんと表示する
                _spriteRenderer.color = new Color(1, 1, 1, 1);
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
