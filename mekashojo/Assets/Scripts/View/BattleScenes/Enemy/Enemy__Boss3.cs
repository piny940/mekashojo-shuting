using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Enemy__Boss3 : FiringBulletBase
    {
        [SerializeField, Header("BossHPBarContentを入れる")] private Image _bossHPBarContent;

        private Rigidbody2D _rigidbody2D;
        private bool _isBeingDestroyed = false;
        private EnemyIDContainer _enemyIDContainer;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _enemyIDContainer = GetComponent<EnemyIDContainer>();
        }

        // Start is called before the first frame update
        void Start()
        {
            Controller.EnemyController.EmergeEnemy__Boss3(this.gameObject);

            Model.Enemy__Boss3 enemy__Boss3
                = Controller.EnemyController.enemyTable
                    [Controller.EnemyController.enemyType__Rough.Boss3]
                    [Controller.EnemyController.bossID].enemy__Boss3;

            Model.EnemyDamageManager enemyDamageManager
                = Controller.EnemyController.damageManagerTable[Controller.EnemyController.bossID];

            _enemyIDContainer.id = Controller.EnemyController.bossID;

            // 速度の監視
            enemy__Boss3.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                _rigidbody2D.velocity = velocity;
            });

            // 弾の発射を監視
            enemy__Boss3.OnFiringBulletInfoChanged.AddListener(collection =>
            {
                Model.DamageFactorManager.FiringBulletInfo info
                    = Model.FiringInfoConverter.MakeStruct(collection);

                Fire(info.bulletVelocity, info.firePath);
            });

            // 消滅の監視
            enemy__Boss3.OnIsBeingDestroyedChanged.AddListener((bool isBeingDestroyed) =>
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
                _bossHPBarContent.fillAmount = hp / Model.Enemy__Boss3.maxHP;
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
