using UnityEngine;

namespace View
{
    public class Enemy__WideBeam : NormalEnemyBase
    {
        private PolygonCollider2D _polygonCollier2D;
        private SpriteRenderer _spriteRenderer;
        [SerializeField, Header("EmemyFire__WideBeamを入れる")] private GameObject _enemyFire__WideBeam;
        private const float NOTICING_FIRE_TRANSPARENCY = 0.1f;

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
                    Controller.ModelClassController.pauseController,
                    Controller.ModelClassController.enemyController,
                    Controller.ModelClassController.playerStatusController,
                    _normalEnemyData
                    );

            // Controllerのクラスにidやインスタンスの情報を渡す
            Controller.EnemyElements enemyElements = new Controller.EnemyElements()
            {
                enemy__WideBeam = enemy__WideBeam,
                enemyDamageManager = enemyDamageManager,
                enemyObject = this.gameObject,
            };
            Controller.EnemyClassController.enemyTable__SimpleBullet.Add(id, enemyElements);


            enemy__WideBeam.OnVelocityChanged.AddListener((velocity) =>
            {
                rigidbody2D.velocity = velocity;
            });

            enemy__WideBeam.OnIsDestroyedChanged.AddListener((isDead) =>
            {
                this.isDead = isDead;
            });

            enemyDamageManager.OnIsDeadChanged.AddListener((bool isDead) =>
            {
                this.isDead = isDead;
            });

            // 当たり判定の処理
            playOnEnter += (collision) =>
            {
                if (collision.tag == "BattleScenes/Player")
                {
                    enemy__WideBeam.DoDamage();
                }
            };

            enemy__WideBeam.OnBeamStatusChanged.AddListener(OnBeamStatusChanged);
        }


        private void OnBeamStatusChanged(Model.EnemyManager.beamFiringProcesses beamStatus)
        {
            if (beamStatus == Model.EnemyManager.beamFiringProcesses.IsNoticingBeamFiring)
            {
                // 攻撃の予告をする

                _enemyFire__WideBeam.SetActive(true);

                //当たり判定はなくしておく
                _polygonCollier2D.enabled = false;

                //薄く表示させる
                _spriteRenderer.color = new Color(1, 1, 1, NOTICING_FIRE_TRANSPARENCY);
            }
            else if (beamStatus == Model.EnemyManager.beamFiringProcesses.IsFiringBeam)
            {
                //攻撃をする
                //当たり判定をOnにする
                _polygonCollier2D.enabled = true;

                //ちゃんと表示する
                _spriteRenderer.color = new Color(1, 1, 1, 1);
            }
            else if (beamStatus == Model.EnemyManager.beamFiringProcesses.HasStoppedBeam)
            {
                _enemyFire__WideBeam.SetActive(false);
            }
        }


        private void Update()
        {
            if (isDead) Die();
        }

        private void Die()
        {
            Controller.EnemyClassController.enemyTable__WideBeam.Remove(id);
            Destroy(this.gameObject);
        }
    }
}
