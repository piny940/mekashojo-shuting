using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Enemy__LastBoss : FiringBulletBase
    {
        private struct BeamElements
        {
            public GameObject beamObject;
        }

        [SerializeField, Header("LastBossFire__ThickBeamを入れる")] private GameObject _lastBossFire__ThickBeam;
        [SerializeField, Header("LastBossFire__SpreadBeamを入れる")] private GameObject _lastBossFire__SpreadBeam;
        [SerializeField, Header("LastBossFire__SpreadLaserWithMissileを入れる")] private GameObject _lastBossFire__SpreadLaserWithMissile;
        [SerializeField, Header("LastBossFire__SpreadLaserWithStunを入れる")] private GameObject _lastBossFire__SpreadLaserWithStun;
        [SerializeField, Header("BossHPBarContentを入れる")] private Image _bossHPBarContent;

        [SerializeField, Header("LastBossModelを入れる")] private Animator _lastBossModelAnim;
        [SerializeField, Header("ThickBeamを入れる")] private GameObject _thickBeam;
        [SerializeField, Header("SpreadBalkanを入れる")] private GameObject _spreadBalkan;
        [SerializeField, Header("Slashを入れる")] private GameObject _slash;
        [SerializeField, Header("SpreadLaserWithGuidedMissileを入れる")] private GameObject _spreadLaserWithGuidedMissile;
        [SerializeField, Header("SpreadLaserWithStunを入れる")] private GameObject _spreadLaserWithStun;

        private Animator _thickBeamAnim;
        private Animator _spreadBalkanAnim;
        private Animator _slashAnim;
        private Animator _spreadLaserWithGuidedMissileAnim;
        private Animator _spreadLaserWithStunAnim;

        private Rigidbody2D _rigidbody2D;
        private EnemyIDContainer _enemyIDContainer;
        private BeamElements _thickBeamElements;
        private BeamElements _spreadBeamElements;
        private BeamElements _spreadLaserWithMissileElements;
        private BeamElements _spreadLaserWithStunElements;

        private enum animationParameters
        {
            waitThickBeam,
            fireThickBeam,
            fireSpreadBalkan,
            fireSlash,
            fireSpreadLaserWithGuidedMissile,
            fireSpreadLaserWithStun,
        }

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
            };

            // _spreadBeamElementsの初期化
            _spreadBeamElements = new BeamElements()
            {
                beamObject = _lastBossFire__SpreadBeam,
            };

            // _spreadLaserWithMissileの初期化
            _spreadLaserWithMissileElements = new BeamElements()
            {
                beamObject = _lastBossFire__SpreadLaserWithMissile,
            };

            // _spreadLaserWithStunの初期化
            _spreadLaserWithStunElements = new BeamElements()
            {
                beamObject = _lastBossFire__SpreadLaserWithStun,
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
                ChangeBeamStatus(_spreadLaserWithMissileElements, status);
            });

            // スタン弾と一緒に出てくる拡散レーザーの状態を監視
            enemy__LastBoss.OnSpreadLaserWithStunStatusChanged.AddListener(status =>
            {
                ChangeBeamStatus(_spreadLaserWithStunElements, status);
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

            InitializeAnimations();
        }

        private void ChangeBeamStatus(BeamElements elements, Model.DamageFactorManager.beamFiringProcesses beamStatus)
        {
            switch (beamStatus)
            {
                case Model.DamageFactorManager.beamFiringProcesses.IsFiringBeam:
                    elements.beamObject.SetActive(true);
                    break;

                case Model.DamageFactorManager.beamFiringProcesses.HasStoppedBeam:
                    elements.beamObject.SetActive(false);
                    break;
            }
        }

        // アニメーション関連の設定を行う
        private void InitializeAnimations()
        {
            _thickBeamAnim = _thickBeam.GetComponent<Animator>();
            _spreadBalkanAnim = _spreadBalkan.GetComponent<Animator>();
            _slashAnim = _slash.GetComponent<Animator>();
            _spreadLaserWithGuidedMissileAnim = _spreadLaserWithGuidedMissile.GetComponent<Animator>();
            _spreadLaserWithStunAnim = _spreadLaserWithStun.GetComponent<Animator>();

            DeactivateAllWeapons();

            // 初めはwaitThickBeamの動きをさせる
            PlayAnimation(Model.Enemy__LastBoss.attackGroups.ThickBeam);

            Model.Enemy__LastBoss enemy__LastBoss
                = Controller.EnemyController.enemyTable
                    [Controller.EnemyController.enemyType__Rough.LastBoss]
                    [Controller.EnemyController.bossID].enemy__LastBoss;

            enemy__LastBoss.OnProceedingAttackGroupNameChanged.AddListener(SwitchWeapon);

            enemy__LastBoss.OnThickBeamStatusChanged.AddListener(status =>
            {
                if (status == Model.DamageFactorManager.beamFiringProcesses.IsFiringBeam)
                {
                    _lastBossModelAnim.SetTrigger(animationParameters.fireThickBeam.ToString());
                    _thickBeamAnim.SetTrigger(animationParameters.fireThickBeam.ToString());
                }
            });
        }

        // 全ての武器を非アクティブにする
        private void DeactivateAllWeapons()
        {
            _thickBeam.SetActive(false);
            _spreadBalkan.SetActive(false);
            _slash.SetActive(false);
            _spreadLaserWithGuidedMissile.SetActive(false);
            _spreadLaserWithStun.SetActive(false);
        }

        // 引数で与えられたattackGroupに対応する武器をアクティブにし、アニメーションを動かす
        private void PlayAnimation(Model.Enemy__LastBoss.attackGroups attackGroup)
        {
            switch (attackGroup)
            {
                case Model.Enemy__LastBoss.attackGroups.ThickBeam:
                    _thickBeam.SetActive(true);
                    _lastBossModelAnim.SetTrigger(animationParameters.waitThickBeam.ToString());
                    _thickBeamAnim.SetTrigger(animationParameters.waitThickBeam.ToString());
                    break;

                case Model.Enemy__LastBoss.attackGroups.SpreadBalkanSet:
                    _spreadBalkan.SetActive(true);
                    _lastBossModelAnim.SetTrigger(animationParameters.fireSpreadBalkan.ToString());
                    _spreadBalkanAnim.SetTrigger(animationParameters.fireSpreadBalkan.ToString());
                    break;

                case Model.Enemy__LastBoss.attackGroups.Slash:
                    _slash.SetActive(true);
                    _lastBossModelAnim.SetTrigger(animationParameters.fireSlash.ToString());
                    _slashAnim.SetTrigger(animationParameters.fireSlash.ToString());
                    break;

                case Model.Enemy__LastBoss.attackGroups.GuidedMissileSet:
                    _spreadLaserWithGuidedMissile.SetActive(true);
                    _lastBossModelAnim.SetTrigger(animationParameters.fireSpreadLaserWithGuidedMissile.ToString());
                    _spreadLaserWithGuidedMissileAnim.SetTrigger(animationParameters.fireSpreadLaserWithGuidedMissile.ToString());
                    break;

                case Model.Enemy__LastBoss.attackGroups.SpreadStunBulletSet:
                    _spreadLaserWithStun.SetActive(true);
                    _lastBossModelAnim.SetTrigger(animationParameters.fireSpreadLaserWithStun.ToString());
                    _spreadLaserWithStunAnim.SetTrigger(animationParameters.fireSpreadLaserWithStun.ToString());
                    break;

                case Model.Enemy__LastBoss.attackGroups.CreateEnemy__SelfDestruct:
                    // 自爆型の敵を生成する時は、武器のオブジェクトは全て非アクティブにして、
                    // 本体はthickBeamの待機状態の動きをする
                    _lastBossModelAnim.SetTrigger(animationParameters.waitThickBeam.ToString());
                    break;
            }
        }

        // 武器を切り替える
        // 武器を切り替えた瞬間に攻撃が始まることを想定している
        private void SwitchWeapon(Model.Enemy__LastBoss.attackGroups attackGroup)
        {
            if (attackGroup == Model.Enemy__LastBoss.attackGroups._none)
            {
                return;
            }

            // 一旦全て非アクティブにする
            DeactivateAllWeapons();

            PlayAnimation(attackGroup);
        }
    }
}
