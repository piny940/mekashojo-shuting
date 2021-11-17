using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Enemy__Boss2 : FiringBulletBase
    {
        [SerializeField, Header("Boss2Fire__SpreadLaserを入れる")] private GameObject _spreadLaser;
        [SerializeField, Header("BossHPBarContentを入れる")] private Image _bossHPBarContent;

        private Rigidbody2D _rigidbody2D;
        private EnemyIDContainer _enemyIDContainer;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _enemyIDContainer = GetComponent<EnemyIDContainer>();

            _spreadLaser.SetActive(false);
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
                    case Model.DamageFactorManager.beamFiringProcesses.IsFiringBeam:
                        _spreadLaser.SetActive(true);
                        break;

                    case Model.DamageFactorManager.beamFiringProcesses.HasStoppedBeam:
                        _spreadLaser.SetActive(false);
                        break;
                }
            });

            // HPの監視
            enemyDamageManager.OnHPChanged.AddListener((float hp) =>
            {
                _bossHPBarContent.fillAmount = hp / Model.Enemy__Boss2.maxHP;
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
    }
}
