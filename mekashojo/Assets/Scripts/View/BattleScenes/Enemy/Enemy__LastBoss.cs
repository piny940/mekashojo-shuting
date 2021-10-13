using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Enemy__LastBoss : FiringBulletBase
    {
        private struct BeamElements
        {
            public GameObject beamObject;
            public PolygonCollider2D polygonCollider2D;
            public SpriteRenderer spriteRenderer;
        }

        private const float NOTIFYING_FIRE_TRANSPARENCY = 0.1f;

        [SerializeField, Header("LastBossFire__ThickBeamを入れる")] private GameObject _lastBossFire__ThickBeam;
        [SerializeField, Header("LastBossFire__SpreadBeamを入れる")] private GameObject _lastBossFire__SpreadBeam;
        [SerializeField, Header("LastBossFire__SpreadLaserWithMissileを入れる")] private GameObject _lastBossFire__SpreadLaserWithMissile;
        [SerializeField, Header("LastBossFire__SpreadLaserWithStunを入れる")] private GameObject _lastBossFire__SpreadLaserWithStun;
        [SerializeField, Header("BossHPBarContentを入れる")] private Image _bossHPBarContent;

        private Rigidbody2D _rigidbody2D;
        private EnemyIDContainer _enemyIDContainer;
        private BeamElements _thickBeamElements;
        private BeamElements _spreadBeamElements;
        private BeamElements _spreadLaserWithMissile;
        private BeamElements _spreadLaserWithStun;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _enemyIDContainer = GetComponent<EnemyIDContainer>();

            _lastBossFire__ThickBeam.SetActive(false);
            _lastBossFire__SpreadBeam.SetActive(false);
            _lastBossFire__SpreadLaserWithMissile.SetActive(false);
            _lastBossFire__SpreadLaserWithStun.SetActive(false);

            // _beamElementsの初期化
            _thickBeamElements = new BeamElements()
            {
                beamObject = _lastBossFire__ThickBeam,
                polygonCollider2D = _lastBossFire__ThickBeam.GetComponent<PolygonCollider2D>(),
                spriteRenderer = _lastBossFire__ThickBeam.GetComponent<SpriteRenderer>(),
            };

            // _spreadBeamElementsの初期化
            _spreadBeamElements = new BeamElements()
            {
                beamObject = _lastBossFire__SpreadBeam,
                polygonCollider2D = _lastBossFire__SpreadBeam.GetComponent<PolygonCollider2D>(),
                spriteRenderer = _lastBossFire__SpreadBeam.GetComponent<SpriteRenderer>(),
            };

            // _spreadLaserWithMissileの初期化
            _spreadLaserWithMissile = new BeamElements()
            {
                beamObject = _lastBossFire__SpreadLaserWithMissile,
                polygonCollider2D = _lastBossFire__SpreadLaserWithMissile.GetComponent<PolygonCollider2D>(),
                spriteRenderer = _lastBossFire__SpreadLaserWithMissile.GetComponent<SpriteRenderer>(),
            };

            // _spreadLaserWithStunの初期化
            _spreadLaserWithStun = new BeamElements()
            {
                beamObject = _lastBossFire__SpreadLaserWithStun,
                polygonCollider2D = _lastBossFire__SpreadLaserWithStun.GetComponent<PolygonCollider2D>(),
                spriteRenderer = _lastBossFire__SpreadLaserWithStun.GetComponent<SpriteRenderer>(),
            };
        }

        // Start is called before the first frame update
        void Start()
        {
            Controller.EnemyController.EmergeEnemy__LastBoss(this.gameObject);

            Model.Enemy__LastBoss enemy__LastBoss
                = Controller.EnemyController.enemyTable
                    [Controller.EnemyController.enemyType__Rough.LastBoss]
                    [Controller.EnemyController.bossID].enemy__LastBoss;

            Model.EnemyDamageManager enemyDamageManager
                = Controller.EnemyController.damageManagerTable[Controller.EnemyController.bossID];

            _enemyIDContainer.id = Controller.EnemyController.bossID;

            // 速度の監視
            enemy__LastBoss.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                _rigidbody2D.velocity = velocity;
            });

            // 弾の発射を監視
            enemy__LastBoss.OnFiringBulletInfoChanged.AddListener(collection =>
            {
                Model.DamageFactorManager.FiringBulletInfo info
                    = Model.FiringInfoConverter.MakeStruct(collection);

                Fire(info.bulletVelocity, info.firePath);
            });

            // 極太ビームの状態を監視
            enemy__LastBoss.OnThickBeamStatusChanged.AddListener(status =>
            {
                ChangeBeamStatus(_thickBeamElements, status);
            });

            // 拡散ビームの状態を監視
            enemy__LastBoss.OnSpreadBeamStatusChanged.AddListener(status =>
            {
                ChangeBeamStatus(_spreadBeamElements, status);
            });

            // 追尾ミサイルと一緒に出てくる拡散レーザーの状態を監視
            enemy__LastBoss.OnSpreadLaserWithMissileStatusChanged.AddListener(status =>
            {
                ChangeBeamStatus(_spreadLaserWithMissile, status);
            });

            // スタン弾と一緒に出てくる拡散レーザーの状態を監視
            enemy__LastBoss.OnSpreadLaserWithStunStatusChanged.AddListener(status =>
            {
                ChangeBeamStatus(_spreadLaserWithStun, status);
            });

            // HPの監視
            enemyDamageManager.OnHPChanged.AddListener((float hp) =>
            {
                _bossHPBarContent.fillAmount = hp / Model.Enemy__LastBoss.maxHP;
            });

            // 消滅の監視
            Controller.BattleScenesController.stageStatusManager.OnCurrentStageStatusChanged.AddListener(status =>
            {
                if (status == Model.StageStatusManager.stageStatus.BossDead)
                {
                    this.gameObject.SetActive(false);
                }
            });
        }

        private void ChangeBeamStatus(BeamElements elements, Model.DamageFactorManager.beamFiringProcesses beamStatus)
        {
            switch (beamStatus)
            {
                case Model.DamageFactorManager.beamFiringProcesses.IsNotifyingBeamFiring:
                    // 攻撃の予告をする

                    elements.beamObject.SetActive(true);

                    //当たり判定はなくしておく
                    elements.polygonCollider2D.enabled = false;

                    //薄く表示させる
                    elements.spriteRenderer.color = new Color(1, 1, 1, NOTIFYING_FIRE_TRANSPARENCY);
                    break;

                case Model.DamageFactorManager.beamFiringProcesses.IsFiringBeam:
                    //攻撃をする
                    //当たり判定をOnにする
                    elements.polygonCollider2D.enabled = true;

                    //ちゃんと表示する
                    elements.spriteRenderer.color = new Color(1, 1, 1, 1);
                    break;

                case Model.DamageFactorManager.beamFiringProcesses.HasStoppedBeam:
                    elements.beamObject.SetActive(false);
                    break;
            }
        }
    }
}
