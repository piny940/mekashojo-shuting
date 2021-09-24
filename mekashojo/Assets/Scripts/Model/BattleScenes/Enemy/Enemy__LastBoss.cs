using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class Enemy__LastBoss : DamageFactorManager
    {
        private const float FIRING_INTERVAL = 5;

        // 極太ビーム用の定数
        private const float THICK_BEAM_NOTIFYING_TIME = 3;
        private const float THICK_BEAM_TIME = 5;

        // 拡散ビーム用の定数
        private const float SPREAD_BEAM_NOTIFYING_TIME = 1;
        private const float SPREAD_BEAM_TIME = 5;

        // 拡散バルカン用の定数
        private const int SPREAD_BALKAN_SHORT_INTERVAL = 5;
        private const int SPREAD_BALKAN_FIRING_AMOUNT = 80;
        private const int SPREAD_BALKAN_FIRING_AMOUNT_PER_ONCE = 15;

        // 拡散ボム用の定数
        private const int SPREAD_BOMB_SHORT_INTERVAL = 30;
        private const int SPREAD_BOMB_FIRING_AMOUNT = 3;
        private const int SPREAD_BOMB_FIRING_AMOUNT_PER_ONCE = 15;

        //TODO 斬撃用の定数

        // 追尾ミサイル用の定数
        private const int GUIDED_MISSILE_SHORT_INTERVAL = 20;
        private const int GUIDED_MISSILE_FIRING_AMOUNT = 45; // 攻撃が仕様書通り15秒間続くよう調整
        // TODO:GuidedBulletの挙動を調整しないとうまく動作しないため、
        // それまで仮に1としておく。↓仕様書通りにするなら8にすべきところ
        private const int GUIDED_MISSILE_FIRING_AMOUNT_PER_ONCE = 1;

        // 追尾ミサイルと一緒に出てくる拡散レーザー用の定数
        private const float SPREAD_LASER_WITH_MISSILE_NOTIFYING_TIME = 1;
        private const float SPREAD_LASER_WITH_MISSILE_TIME = 5;

        // 自爆型ドローン召喚用の定数
        private const float CREATE_ENEMY_SHORT_INTERVAL = 0.5f;
        private const float CREATE_ENEMY_AMOUNT = 20;

        // TODO ビームソード用の定数

        // スタン弾用の定数
        private const int SPREAD_STUN_BULLET_SHORT_INTERVAL = 40;
        private const int SPREAD_STUN_BULLET_FIRING_AMOUNT = 5;
        private const int SPREAD_STUN_BULLET_FIRING_AMOUNT_PER_ONCE = 15;

        // スタン弾と一緒に出てくる拡散レーザー用の定数
        private const float SPREAD_LASER_WITH_STUN_NOTIFYING_TIME = 1;
        private const float SPREAD_LASER_WITH_STUN_TIME = 5;

        // 各種類の攻撃をする可能性の比
        private readonly Dictionary<attackGroups, float> _attackProbabilityRatios
            = new Dictionary<attackGroups, float>()
            {
                { attackGroups.ThickBeam, 1 },
                { attackGroups.SpreadBombSet, 1 },
                { attackGroups.Slash, 0 },
                { attackGroups.GuidedMissileSet, 1 },
                { attackGroups.CreateEnemy__SelfDestruct, 1 },
                { attackGroups.BeamSword, 0 },
                { attackGroups.SpreadStunBulletSet, 1 },
            };

        private attackGroups _proceedingAttackGourpName = attackGroups._none; // 今どの攻撃をしているか
        private Dictionary<attackType, float> _damageAmounts { get; set; }
        private Dictionary<attackType, float> _bulletSpeeds { get; set; }
        private float _time = 0;

        // 各攻撃を行っているかどうか
        private Dictionary<attackType, bool> _isAttackingTable
            = new Dictionary<attackType, bool>()
            {
                { attackType.ThickBeam, false },
                { attackType.SpreadBeam, false },
                { attackType.SpreadBalkan, false },
                { attackType.SpreadBomb, false },
                { attackType.Slash, false },
                { attackType.GuidedMissile, false },
                { attackType.SpreadLaserWithMissile, false },
                { attackType.CreateEnemy__SelfDestruct, false },
                { attackType.BeamSword, false },
                { attackType.SpreadStunBullet, false },
                { attackType.SpreadLaserWithStun, false },
            };

        // 各ビームの状態
        private beamFiringProcesses _thickBeamStatus = beamFiringProcesses.HasStoppedBeam;
        private beamFiringProcesses _spreadBeamStatus = beamFiringProcesses.HasStoppedBeam;
        private beamFiringProcesses _spreadLaserWithMissileStatus = beamFiringProcesses.HasStoppedBeam;
        private beamFiringProcesses _spreadLaserWithStunStatus = beamFiringProcesses.HasStoppedBeam;

        // 弾の発射の設定
        private BulletProcessInfo _spreadBalkanProcessInfo;
        private BulletProcessInfo _spreadBombProcessInfo;
        private BulletProcessInfo _guidedMissileProcessInfo;
        private BulletProcessInfo _spreadStunBulletProcessInfo;

        // 親クラスで定義したProceedEnemyCreatingメソッドの構造上、
        // 生成する敵の種類が自爆型だけだったとしても生成率の辞書を作成しないといけない
        private Dictionary<Controller.NormalEnemyData.normalEnemyType, float> _produceProbabilityRatios
            = new Dictionary<Controller.NormalEnemyData.normalEnemyType, float>()
            { {Controller.NormalEnemyData.normalEnemyType.SelfDestruct, 1 } };

        protected override DamageFactorData.damageFactorType factorType { get; set; }

        public static readonly float maxHP = 15000;
        public static readonly float spreadBalkanDisappearTime = 0.9f;
        public static readonly float spreadBombDisappearTime = 1f;
        public static IReadOnlyDictionary<attackType, float> damageAmounts { get; private set; }
        public static IReadOnlyDictionary<attackType, float> bulletSpeeds { get; private set; }

        public UnityEvent<beamFiringProcesses> OnThickBeamStatusChanged
            = new UnityEvent<beamFiringProcesses>();

        public UnityEvent<beamFiringProcesses> OnSpreadBeamStatusChanged
            = new UnityEvent<beamFiringProcesses>();

        public UnityEvent<beamFiringProcesses> OnSpreadLaserWithMissileStatusChanged
            = new UnityEvent<beamFiringProcesses>();

        public UnityEvent<beamFiringProcesses> OnSpreadLaserWithStunStatusChanged
            = new UnityEvent<beamFiringProcesses>();

        public beamFiringProcesses thickBeamStatus
        {
            get { return _thickBeamStatus; }
            set
            {
                _thickBeamStatus = value;
                OnThickBeamStatusChanged?.Invoke(value);
            }
        }

        public beamFiringProcesses spreadBeamStatus
        {
            get { return _spreadBeamStatus; }
            set
            {
                _spreadBeamStatus = value;
                OnSpreadBeamStatusChanged?.Invoke(value);
            }
        }

        public beamFiringProcesses spreadLaserWithMissileStatus
        {
            get { return _spreadLaserWithMissileStatus; }
            set
            {
                _spreadLaserWithMissileStatus = value;
                OnSpreadLaserWithMissileStatusChanged?.Invoke(value);
            }
        }

        public beamFiringProcesses spreadLaserWithStunStatus
        {
            get { return _spreadLaserWithStunStatus; }
            set
            {
                _spreadLaserWithStunStatus = value;
                OnSpreadLaserWithStunStatusChanged?.Invoke(value);
            }
        }

        // ラスボスがする攻撃(同時にする攻撃は一つにまとめてある)
        public enum attackGroups
        {
            _none,
            ThickBeam,
            SpreadBombSet,
            GuidedMissileSet,
            SpreadStunBulletSet,
            Slash,
            CreateEnemy__SelfDestruct,
            BeamSword,
        }

        // ラスボスがする攻撃の種類
        public enum attackType
        {
            ThickBeam,
            SpreadBeam,
            SpreadBalkan,
            SpreadBomb,
            Slash,
            GuidedMissile,
            SpreadLaserWithMissile,
            CreateEnemy__SelfDestruct,
            BeamSword,
            SpreadStunBullet,
            SpreadLaserWithStun,
        }

        public Enemy__LastBoss(PauseManager pauseManager, EnemyManager enemyManager, PlayerStatusManager playerStatusManager)
                : base(pauseManager, enemyManager, playerStatusManager)
        {
            _damageAmounts = new Dictionary<attackType, float>()
            {
                { attackType.ThickBeam, 500 },
                { attackType.SpreadBeam, 80 },
                { attackType.SpreadBalkan, 10 },
                { attackType.SpreadBomb, 120 },
                { attackType.Slash, 200 },
                { attackType.GuidedMissile, 120 },
                { attackType.SpreadLaserWithMissile, 100 },
                { attackType.BeamSword, 300 },
                { attackType.SpreadStunBullet, 20 },
                { attackType.SpreadLaserWithStun, 80 },
            };

            damageAmounts = new ReadOnlyDictionary<attackType, float>(_damageAmounts);

            _bulletSpeeds = new Dictionary<attackType, float>()
            {
                { attackType.SpreadBalkan, 10 },
                { attackType.SpreadBomb, 5 },
                { attackType.GuidedMissile, 5 },
                { attackType.SpreadStunBullet, 8 },
            };

            bulletSpeeds = new ReadOnlyDictionary<attackType, float>(_bulletSpeeds);

            factorType = DamageFactorData.damageFactorType.Boss;

            _spreadBalkanProcessInfo = new BulletProcessInfo()
            {
                shortInterval_Frame = SPREAD_BALKAN_SHORT_INTERVAL,
                firePath = "LastBoss__" + attackType.SpreadBalkan.ToString(),
                firingAmount = SPREAD_BALKAN_FIRING_AMOUNT,
                bulletVelocities = new List<Vector3>(),
            };

            for (int i = 0; i < SPREAD_BALKAN_FIRING_AMOUNT_PER_ONCE; i++)
            {
                //弾を発射する方向を計算
                _spreadBalkanProcessInfo.bulletVelocities.Add(
                    bulletSpeeds[attackType.SpreadBalkan]
                    * new Vector3(
                        Mathf.Cos(Mathf.PI / 2 + Mathf.PI * i / SPREAD_BALKAN_FIRING_AMOUNT_PER_ONCE),
                        Mathf.Sin(Mathf.PI / 2 + Mathf.PI * i / SPREAD_BALKAN_FIRING_AMOUNT_PER_ONCE),
                        0)
                    );
            }

            _spreadBombProcessInfo = new BulletProcessInfo()
            {
                shortInterval_Frame = SPREAD_BOMB_SHORT_INTERVAL,
                firePath = "LastBoss__" + attackType.SpreadBomb.ToString(),
                firingAmount = SPREAD_BOMB_FIRING_AMOUNT,
                bulletVelocities = new List<Vector3>(),
            };

            for (int i = 0; i < SPREAD_BOMB_FIRING_AMOUNT_PER_ONCE; i++)
            {
                //弾を発射する方向を計算
                _spreadBombProcessInfo.bulletVelocities.Add(
                    bulletSpeeds[attackType.SpreadBomb]
                    * new Vector3(
                        Mathf.Cos(Mathf.PI / 2 + Mathf.PI * i / SPREAD_BOMB_FIRING_AMOUNT_PER_ONCE),
                        Mathf.Sin(Mathf.PI / 2 + Mathf.PI * i / SPREAD_BOMB_FIRING_AMOUNT_PER_ONCE),
                        0)
                    );
            }

            _guidedMissileProcessInfo = new BulletProcessInfo()
            {
                shortInterval_Frame = GUIDED_MISSILE_SHORT_INTERVAL,
                firePath = "LastBoss__" + attackType.GuidedMissile.ToString(),
                firingAmount = GUIDED_MISSILE_FIRING_AMOUNT,
                bulletVelocities = new List<Vector3>(),
            };

            for (int i = 0; i < GUIDED_MISSILE_FIRING_AMOUNT_PER_ONCE; i++)
            {
                //弾を発射する方向を計算
                _guidedMissileProcessInfo.bulletVelocities.Add(
                    bulletSpeeds[attackType.GuidedMissile]
                    * new Vector3(
                        Mathf.Cos(Mathf.PI / 2 + Mathf.PI * i / GUIDED_MISSILE_FIRING_AMOUNT_PER_ONCE),
                        Mathf.Sin(Mathf.PI / 2 + Mathf.PI * i / GUIDED_MISSILE_FIRING_AMOUNT_PER_ONCE),
                        0)
                    );
            }

            _spreadStunBulletProcessInfo = new BulletProcessInfo()
            {
                shortInterval_Frame = SPREAD_STUN_BULLET_SHORT_INTERVAL,
                firePath = "LastBoss__" + attackType.SpreadStunBullet.ToString(),
                firingAmount = SPREAD_STUN_BULLET_FIRING_AMOUNT,
                bulletVelocities = new List<Vector3>(),
            };

            for (int i = 0; i < SPREAD_STUN_BULLET_FIRING_AMOUNT_PER_ONCE; i++)
            {
                //弾を発射する方向を計算
                _spreadStunBulletProcessInfo.bulletVelocities.Add(
                    bulletSpeeds[attackType.SpreadStunBullet]
                    * new Vector3(
                        Mathf.Cos(Mathf.PI / 2 + Mathf.PI * i / SPREAD_STUN_BULLET_FIRING_AMOUNT_PER_ONCE),
                        Mathf.Sin(Mathf.PI / 2 + Mathf.PI * i / SPREAD_STUN_BULLET_FIRING_AMOUNT_PER_ONCE),
                        0)
                    );
            }
        }

        public void RunEveryFrame()
        {
            StopOnPausing();
            ProceedAttack();
        }

        // ビームの状態を変える処理
        // 呼び出すタイミングは親クラスに管理されている
        protected override void ChangeBeamStatus(beamFiringProcesses status)
        {
            switch (_proceedingAttackGourpName)
            {
                case attackGroups.ThickBeam:
                    thickBeamStatus = status;
                    break;

                case attackGroups.SpreadBombSet:
                    spreadBeamStatus = status;
                    break;

                case attackGroups.GuidedMissileSet:
                    spreadLaserWithMissileStatus = status;
                    break;

                case attackGroups.SpreadStunBulletSet:
                    spreadLaserWithStunStatus = status;
                    break;

                default:
                    break;
            }
        }

        private void ProceedAttack()
        {
            if (!pauseManager.isGameGoing) return;

            // 攻撃を始める処理
            if (_time > FIRING_INTERVAL && _proceedingAttackGourpName == attackGroups._none)
            {
                _time = 0;
                _proceedingAttackGourpName = RandomChoosing.ChooseRandomly(_attackProbabilityRatios);
                StartAttack(_proceedingAttackGourpName);
            }

            // 攻撃中でない場合は時間をカウントする
            if (_proceedingAttackGourpName == attackGroups._none)
            {
                _time += Time.deltaTime;
                return;
            }

            // 攻撃本体
            ProceedThickBeam();

            ProceedSpreadBombSet();

            ProceedGuidedMissileSet();

            ProceedCreatingEnemy();

            ProceedSpreadStunBullet();
        }

        // AttackGroupの抽選を行った後に呼ぶメソッド
        private void StartAttack(attackGroups attackGroup)
        {
            switch (attackGroup)
            {
                case attackGroups.ThickBeam:
                    _isAttackingTable[attackType.ThickBeam] = true;
                    break;

                case attackGroups.SpreadBombSet:
                    _isAttackingTable[attackType.SpreadBeam] = true;
                    _isAttackingTable[attackType.SpreadBalkan] = true;
                    _isAttackingTable[attackType.SpreadBomb] = true;
                    break;

                case attackGroups.Slash:
                    _isAttackingTable[attackType.Slash] = true;
                    break;

                case attackGroups.GuidedMissileSet:
                    _isAttackingTable[attackType.GuidedMissile] = true;
                    _isAttackingTable[attackType.SpreadLaserWithMissile] = true;
                    break;

                case attackGroups.CreateEnemy__SelfDestruct:
                    _isAttackingTable[attackType.CreateEnemy__SelfDestruct] = true;
                    break;

                case attackGroups.BeamSword:
                    _isAttackingTable[attackType.BeamSword] = true;
                    break;

                case attackGroups.SpreadStunBulletSet:
                    _isAttackingTable[attackType.SpreadStunBullet] = true;
                    _isAttackingTable[attackType.SpreadLaserWithStun] = true;
                    break;
            }
        }

        // ビーム系単体の攻撃の処理
        private void ProceedThickBeam()
        {
            if (_proceedingAttackGourpName != attackGroups.ThickBeam) return;

            if (_isAttackingTable[attackType.ThickBeam])
                _isAttackingTable[attackType.ThickBeam]
                    = ProceedBeamFiring(THICK_BEAM_NOTIFYING_TIME, THICK_BEAM_TIME);

            if (!_isAttackingTable[attackType.ThickBeam])
                _proceedingAttackGourpName = attackGroups._none;
        }

        // 拡散ボム・拡散ビーム・拡散バルカンの処理
        private void ProceedSpreadBombSet()
        {
            if (_proceedingAttackGourpName != attackGroups.SpreadBombSet) return;

            // 攻撃本体
            if (_isAttackingTable[attackType.SpreadBeam])
                _isAttackingTable[attackType.SpreadBeam]
                    = ProceedBeamFiring(SPREAD_BEAM_NOTIFYING_TIME, SPREAD_BEAM_TIME);

            if (_isAttackingTable[attackType.SpreadBalkan])
                _isAttackingTable[attackType.SpreadBalkan]
                    = ProceedBulletFiring(_spreadBalkanProcessInfo);

            if (_isAttackingTable[attackType.SpreadBomb])
                _isAttackingTable[attackType.SpreadBomb]
                    = ProceedBulletFiring(_spreadBombProcessInfo);

            // 攻撃終了時の処理
            // 全ての攻撃が終了したら実行する
            if (!_isAttackingTable[attackType.SpreadBeam]
                && !_isAttackingTable[attackType.SpreadBalkan]
                && !_isAttackingTable[attackType.SpreadBomb])
                _proceedingAttackGourpName = attackGroups._none;
        }

        // TODO斬撃の処理

        // 追尾ミサイル、拡散レーザーの処理
        private void ProceedGuidedMissileSet()
        {
            if (_proceedingAttackGourpName != attackGroups.GuidedMissileSet) return;

            // 攻撃本体
            if (_isAttackingTable[attackType.GuidedMissile])
                _isAttackingTable[attackType.GuidedMissile]
                    = ProceedBulletFiring(_guidedMissileProcessInfo);

            if (_isAttackingTable[attackType.SpreadLaserWithMissile])
                _isAttackingTable[attackType.SpreadLaserWithMissile]
                    = ProceedBeamFiring(SPREAD_LASER_WITH_MISSILE_NOTIFYING_TIME, SPREAD_LASER_WITH_MISSILE_TIME);

            // 攻撃終了時の処理
            // 全ての攻撃が終了したら実行する
            if (!_isAttackingTable[attackType.GuidedMissile]
                && !_isAttackingTable[attackType.SpreadLaserWithMissile])
                _proceedingAttackGourpName = attackGroups._none;
        }

        // 敵生成の処理
        private void ProceedCreatingEnemy()
        {
            if (_proceedingAttackGourpName != attackGroups.CreateEnemy__SelfDestruct) return;

            // 攻撃本体
            if (_isAttackingTable[attackType.CreateEnemy__SelfDestruct])
                _isAttackingTable[attackType.CreateEnemy__SelfDestruct]
                    = ProceedEnemyCreating(CREATE_ENEMY_SHORT_INTERVAL, CREATE_ENEMY_AMOUNT, _produceProbabilityRatios);

            // 攻撃終了時の処理
            if (!_isAttackingTable[attackType.CreateEnemy__SelfDestruct])
                _proceedingAttackGourpName = attackGroups._none;
        }

        // TODOビームソードの処理

        // スタン弾・拡散レーザーの処理
        private void ProceedSpreadStunBullet()
        {
            if (_proceedingAttackGourpName != attackGroups.SpreadStunBulletSet) return;

            // 攻撃本体
            if (_isAttackingTable[attackType.SpreadStunBullet])
                _isAttackingTable[attackType.SpreadStunBullet]
                    = ProceedBulletFiring(_spreadStunBulletProcessInfo);

            if (_isAttackingTable[attackType.SpreadLaserWithStun])
                _isAttackingTable[attackType.SpreadLaserWithStun]
                    = ProceedBeamFiring(SPREAD_LASER_WITH_STUN_NOTIFYING_TIME, SPREAD_LASER_WITH_STUN_TIME);

            // 攻撃終了時の処理
            // 全ての攻撃が終了したら実行する
            if (!_isAttackingTable[attackType.SpreadStunBullet]
                && !_isAttackingTable[attackType.SpreadLaserWithStun])
                _proceedingAttackGourpName = attackGroups._none;
        }
    }
}

