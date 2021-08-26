using UnityEngine;

namespace View
{
    public class Enemy__WideBeam : NormalEnemyBase
    {
        private const float NOTIFYING_FIRE_TRANSPARENCY = 0.1f;
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
            Model.EnemyDamageManager enemyDamageManager = Initialize();

            //実行順序の関係でコンストラクタはStartに書かないといけない
            Model.Enemy__WideBeam enemy__WideBeam
                = new Model.Enemy__WideBeam(
                    Controller.BattleScenesClassController.pauseController,
                    Controller.BattleScenesClassController.enemyController,
                    Controller.BattleScenesClassController.playerStatusController,
                    _normalEnemyData
                    );

            // Controllerのクラスにidやインスタンスの情報を渡す
            Controller.EnemyElements enemyElements = new Controller.EnemyElements()
            {
                enemy__WideBeam = enemy__WideBeam,
                enemyObject = this.gameObject,
            };
            Controller.EnemyClassController.enemyTable__WideBeam.Add(id, enemyElements);
            Controller.EnemyClassController.damageManagerTable.Add(id, enemyDamageManager);


            enemy__WideBeam.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                rigidbody2D.velocity = velocity;
            });

            enemy__WideBeam.OnIsBeingDestroyedChanged.AddListener((bool isBeingDestroyed) =>
            {
                this.isDying = isBeingDestroyed;
            });

            enemyDamageManager.OnIsDyingChanged.AddListener((bool isDying) =>
            {
                this.isDying = isDying;
            });

            // 当たり判定の処理
            playOnEnter += (collision) =>
            {
                if (collision.tag == "BattleScenes/Player")
                {
                    enemy__WideBeam.DealCollisionDamage();
                }
            };

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

        private void Update()
        {
            if (isDying) Die();
        }

        private void Die()
        {
            Controller.EnemyClassController.enemyTable__WideBeam.Remove(id);
            Destroy(this.gameObject);
        }
    }
}
