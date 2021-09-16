using UnityEngine;

namespace View
{
    public class Enemy__Boss1 : FiringBulletBase
    {
        private struct BeamElements
        {
            public GameObject beamObject;
            public PolygonCollider2D polygonCollider2D;
            public SpriteRenderer spriteRenderer;
        }

        private const float NOTIFYING_FIRE_TRANSPARENCY = 0.1f;

        [SerializeField, Header("Boss1Fire__Beamを入れる")] private GameObject _boss1Fire__Beam;
        [SerializeField, Header("Boss1Fire__WideBeamを入れる")] private GameObject _boss1Fire__WideBeam;
        [SerializeField, Header("Boss1Fire__SpreadBeamを入れる")] private GameObject _boss1Fire__SpreadBeam;

        private Rigidbody2D _rigidbody2D;
        private bool _isBeingDestroyed = false;
        private BeamElements _beamElements;
        private BeamElements _wideBeamElements;
        private BeamElements _spreadBeamElements;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _boss1Fire__Beam.SetActive(false);
            _boss1Fire__WideBeam.SetActive(false);
            _boss1Fire__SpreadBeam.SetActive(false);

            // _beamElementsの初期化
            _beamElements = new BeamElements()
            {
                beamObject = _boss1Fire__Beam,
                polygonCollider2D = _boss1Fire__Beam.GetComponent<PolygonCollider2D>(),
                spriteRenderer = _boss1Fire__Beam.GetComponent<SpriteRenderer>(),
            };

            // _wideBeamElementsの初期化
            _wideBeamElements = new BeamElements()
            {
                beamObject = _boss1Fire__WideBeam,
                polygonCollider2D = _boss1Fire__WideBeam.GetComponent<PolygonCollider2D>(),
                spriteRenderer = _boss1Fire__WideBeam.GetComponent<SpriteRenderer>(),
            };

            // _spreadBeamElementsの初期化
            _spreadBeamElements = new BeamElements()
            {
                beamObject = _boss1Fire__SpreadBeam,
                polygonCollider2D = _boss1Fire__SpreadBeam.GetComponent<PolygonCollider2D>(),
                spriteRenderer = _boss1Fire__SpreadBeam.GetComponent<SpriteRenderer>(),
            };
        }

        // Start is called before the first frame update
        void Start()
        {
            Controller.EnemyController.EmergeEnemy__Boss1(this.gameObject);

            Model.Enemy__Boss1 enemy__Boss1
                = Controller.EnemyController.bossTable[Model.ProgressData.stageName.stage1].enemy__Boss1;

            Model.EnemyDamageManager enemyDamageManager
                = Controller.EnemyController.bossDamageManagerTable[Model.ProgressData.stageName.stage1];

            // 速度の監視
            enemy__Boss1.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                _rigidbody2D.velocity = velocity;
            });

            // 弾の発射を監視
            enemy__Boss1.OnFiringBulletInfoChanged.AddListener(collection =>
            {
                Model.DamageFactorManager.FiringBulletInfo info
                    = Model.FiringInfoConverter.MakeStruct(collection);

                Fire(info.bulletVelocity, info.firePath);
            });

            // ビームの状態を監視
            enemy__Boss1.OnBeamStatusChanged.AddListener(status =>
            {
                ChangeBeamStatus(_beamElements, status);
            });

            // 広範囲ビームの状態を監視
            enemy__Boss1.OnWideBeamStatusChanged.AddListener(status =>
            {
                ChangeBeamStatus(_wideBeamElements, status);
            });

            // 拡散ビームの状態を監視
            enemy__Boss1.OnSpreadBeamStatusChanged.AddListener(status =>
            {
                ChangeBeamStatus(_spreadBeamElements, status);
            });

            // 消滅の監視
            enemy__Boss1.OnIsBeingDestroyedChanged.AddListener((bool isBeingDestroyed) =>
            {
                _isBeingDestroyed = isBeingDestroyed;
            });

            enemyDamageManager.OnIsDyingChanged.AddListener((bool isBeingDestroyed) =>
            {
                _isBeingDestroyed = isBeingDestroyed;
            });
        }

        private void Update()
        {
            if (_isBeingDestroyed) Die();
        }

        private void Die()
        {
            //TODO:死亡モーション

            SceneChangeManager.sceneChangeManager.ChangeScene(SceneChangeManager.SceneNames.StageClearScene);
        }

        private void ChangeBeamStatus(BeamElements elements, Model.DamageFactorManager.beamFiringProcesses beamStatus)
        {
            if (beamStatus == Model.DamageFactorManager.beamFiringProcesses.IsNotifyingBeamFiring)
            {
                // 攻撃の予告をする

                elements.beamObject.SetActive(true);

                //当たり判定はなくしておく
                elements.polygonCollider2D.enabled = false;

                //薄く表示させる
                elements.spriteRenderer.color = new Color(1, 1, 1, NOTIFYING_FIRE_TRANSPARENCY);
            }
            else if (beamStatus == Model.DamageFactorManager.beamFiringProcesses.IsFiringBeam)
            {
                //攻撃をする
                //当たり判定をOnにする
                elements.polygonCollider2D.enabled = true;

                //ちゃんと表示する
                elements.spriteRenderer.color = new Color(1, 1, 1, 1);
            }
            else if (beamStatus == Model.DamageFactorManager.beamFiringProcesses.HasStoppedBeam)
            {
                elements.beamObject.SetActive(false);
            }
        }
    }
}
