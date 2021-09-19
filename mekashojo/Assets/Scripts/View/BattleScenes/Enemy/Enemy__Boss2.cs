using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Enemy__Boss2 : FiringBulletBase
    {
        private const float NOTIFYING_FIRE_TRANSPARENCY = 0.1f;

        [SerializeField, Header("Boss2Fire__SpreadLaserを入れる")] private GameObject _spreadLaser;
        [SerializeField, Header("BossHPBarContentを入れる")] private Image _bossHPBarContent;

        private Rigidbody2D _rigidbody2D;
        private bool _isBeingDestroyed = false;
        private EnemyIDContainer _enemyIDContainer;
        private PolygonCollider2D _polygonCollider2D;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _enemyIDContainer = GetComponent<EnemyIDContainer>();

            _spreadLaser.SetActive(false);
            _polygonCollider2D = _spreadLaser.GetComponent<PolygonCollider2D>();
            _spriteRenderer = _spreadLaser.GetComponent<SpriteRenderer>();
        }

        // Start is called before the first frame update
        void Start()
        {
            Controller.EnemyController.EmergeEnemy__Boss2(this.gameObject);

            Model.Enemy__Boss2 enemy__Boss2
                = Controller.EnemyController.enemyTable
                    [Controller.EnemyController.enemyType__Rough.Boss2]
                    [Controller.EnemyController.bossID].enemy__Boss2;

            Model.EnemyDamageManager enemyDamageManager
                = Controller.EnemyController.damageManagerTable[Controller.EnemyController.bossID];

            _enemyIDContainer.id = Controller.EnemyController.bossID;

            // 速度の監視
            enemy__Boss2.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                _rigidbody2D.velocity = velocity;
            });

            // 弾の発射を監視
            enemy__Boss2.OnFiringBulletInfoChanged.AddListener(collection =>
            {
                Model.DamageFactorManager.FiringBulletInfo info
                    = Model.FiringInfoConverter.MakeStruct(collection);

                Fire(info.bulletVelocity, info.firePath);
            });

            // 拡散レーザーの発射を監視
            enemy__Boss2.OnSpreadLaserStatusChanged.AddListener(status =>
            {
                switch (status)
                {
                    case Model.DamageFactorManager.beamFiringProcesses.IsNotifyingBeamFiring:
                        // 攻撃の予告をする

                        _spreadLaser.SetActive(true);

                        //当たり判定はなくしておく
                        _polygonCollider2D.enabled = false;

                        //薄く表示させる
                        _spriteRenderer.color = new Color(1, 1, 1, NOTIFYING_FIRE_TRANSPARENCY);
                        break;

                    case Model.DamageFactorManager.beamFiringProcesses.IsFiringBeam:
                        //攻撃をする
                        //当たり判定をOnにする
                        _polygonCollider2D.enabled = true;

                        //ちゃんと表示する
                        _spriteRenderer.color = new Color(1, 1, 1, 1);
                        break;

                    case Model.DamageFactorManager.beamFiringProcesses.HasStoppedBeam:
                        _spreadLaser.SetActive(false);
                        break;
                }
            });

            // 消滅の監視
            enemy__Boss2.OnIsBeingDestroyedChanged.AddListener((bool isBeingDestroyed) =>
            {
                _isBeingDestroyed = isBeingDestroyed;
            });

            enemyDamageManager.OnIsDyingChanged.AddListener((bool isBeingDestroyed) =>
            {
                _isBeingDestroyed = isBeingDestroyed;
            });

            // HPの監視
            enemyDamageManager.OnHPChanged.AddListener((float hp) =>
            {
                _bossHPBarContent.fillAmount = hp / Model.Enemy__Boss2.maxHP;
            });
        }

        private void Update()
        {
            if (_isBeingDestroyed) Die();
        }

        private void Die()
        {
            //TODO:死亡モーション

            BGMPlayer.bgmPlayer.ChangeBGM(SceneChangeManager.SceneNames.StageClearScene);
            SceneChangeManager.sceneChangeManager.ChangeScene(SceneChangeManager.SceneNames.StageClearScene);
        }
    }
}
