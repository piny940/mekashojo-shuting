using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class Enemy__Boss1 : DamageFactorManager
    {
        private const float FIRING_INTERVAL = 5;

        // ビーム用の定数
        private const float BEAM_NOTIFYING_TIME = 1;
        private const float BEAM_TIME = 5;

        // ミサイル用の定数
        private const int MISSILE_SHORT_INTERVAL = 0; // 一発しか撃たないから0でいい
        private const int MISSILE_FIRING_AMOUNT = 1;

        // 拡散弾用の定数
        private const int SPREAD_BULLET_SHORT_INTERVAL = 5;
        private const int SPREAD_BULLET_FIRING_AMOUNT = 60; // 攻撃が仕様書通り5秒間続くよう調整
        private const int SPREAD_BULLET_FIRING_AMOUNT_PER_ONCE = 15;

        // 広範囲ビーム用の定数
        private const float WIDE_BEAM_NOTIFYING_TIME = 1;
        private const float WIDE_BEAM_TIME = 5;

        // 誘導弾用の定数
        private const int GUIDED_BULLET_SHORT_INTERVAL = 20;
        private const int GUIDED_BULLET_FIRING_AMOUNT = 45; // 攻撃が仕様書通り15秒間続くよう調整
        // TODO:GuidedBulletの挙動を調整しないとうまく動作しないため、
        // それまで仮に1としておく。↓仕様書通りにするなら10にすべきところ
        private const int GUIDED_BULLET_FIRING_AMOUNT_PER_ONCE = 1;

        // 拡散ビーム用の定数
        private const float SPREAD_BEAM_NOTIFYING_TIME = 1;
        private const float SPREAD_BEAM_TIME = 5;

        // 各種類の攻撃をする可能性の比
        private readonly Dictionary<attackType, float> _attackProbabilityRatios
            = new Dictionary<attackType, float>()
            {
                { attackType.Beam, 1 },
                { attackType.Missile, 1 },
                { attackType.SpreadBullet, 1 },
                { attackType.WideBeam, 1 },
                { attackType.GuidedBullet, 1 },
                { attackType.SpreadBeam, 1 },
            };

        private attackType _proceedingAttackTypeName = attackType._none; // 今どの攻撃をしているか
        private Dictionary<attackType, float> _damageAmounts { get; set; }
        private Dictionary<attackType, float> _bulletSpeeds { get; set; }
        private float _time = 0;

        // 各ビームの状態
        private beamFiringProcesses _beamStatus = beamFiringProcesses.HasStoppedBeam;
        private beamFiringProcesses _wideBeamStatus = beamFiringProcesses.HasStoppedBeam;
        private beamFiringProcesses _spreadBeamStatus = beamFiringProcesses.HasStoppedBeam;

        private BulletProcessInfo _missileProcessInfo;
        private BulletProcessInfo _spreadBulletProcessInfo;
        private BulletProcessInfo _guidedBulletProcessInfo;

        protected override DamageFactorData.damageFactorType factorType { get; set; }

        public static readonly float maxHP = 2000;
        public static readonly float spreadBulletDisappearTime = 0.4f;
        public static IReadOnlyDictionary<attackType, float> damageAmounts { get; private set; }
        public static IReadOnlyDictionary<attackType, float> bulletSpeeds { get; private set; }

        public UnityEvent<beamFiringProcesses> OnBeamStatusChanged
            = new UnityEvent<beamFiringProcesses>();

        public UnityEvent<beamFiringProcesses> OnWideBeamStatusChanged
            = new UnityEvent<beamFiringProcesses>();

        public UnityEvent<beamFiringProcesses> OnSpreadBeamStatusChanged
            = new UnityEvent<beamFiringProcesses>();

        public beamFiringProcesses beamStatus
        {
            get { return _beamStatus; }
            set
            {
                _beamStatus = value;
                OnBeamStatusChanged?.Invoke(value);
            }
        }

        public beamFiringProcesses wideBeamStatus
        {
            get { return _wideBeamStatus; }
            set
            {
                _wideBeamStatus = value;
                OnWideBeamStatusChanged?.Invoke(value);
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

        // ステージ1のボスがする攻撃の種類
        public enum attackType
        {
            _none,
            Beam,
            Missile,
            SpreadBullet,
            WideBeam,
            GuidedBullet,
            SpreadBeam,
        }

        public Enemy__Boss1(PauseManager pauseManager, EnemyManager enemyManager, PlayerStatusManager playerStatusManager)
                : base(pauseManager, enemyManager, playerStatusManager)
        {
            _damageAmounts = new Dictionary<attackType, float>()
            {
                { attackType.Beam, 40 },
                { attackType.Missile, 60 },
                { attackType.SpreadBullet, 20 },
                { attackType.WideBeam, 80 },
                { attackType.GuidedBullet, 60 },
                { attackType.SpreadBeam, 80 },
            };

            damageAmounts = new ReadOnlyDictionary<attackType, float>(_damageAmounts);

            _bulletSpeeds = new Dictionary<attackType, float>()
            {
                { attackType.GuidedBullet, 10 },
                { attackType.Missile, 6 },
                { attackType.SpreadBullet, 10 },
            };

            bulletSpeeds = new ReadOnlyDictionary<attackType, float>(_bulletSpeeds);

            factorType = DamageFactorData.damageFactorType.Boss;

            // ミサイル発射の設定
            // 発射速度は攻撃時に設定する
            _missileProcessInfo = new BulletProcessInfo()
            {
                shortInterval_Frame = MISSILE_SHORT_INTERVAL,
                firePath = "Boss1__" + attackType.Missile.ToString(),
                firingAmount = MISSILE_FIRING_AMOUNT,
            };

            // 拡散弾発射の設定
            _spreadBulletProcessInfo = new BulletProcessInfo()
            {
                shortInterval_Frame = SPREAD_BULLET_SHORT_INTERVAL,
                firePath = "Boss1__" + attackType.SpreadBullet.ToString(),
                firingAmount = SPREAD_BULLET_FIRING_AMOUNT,
                bulletVelocities = new List<Vector3>(),
            };

            for (int i = 0; i < SPREAD_BULLET_FIRING_AMOUNT_PER_ONCE; i++)
            {
                //弾を発射する方向を計算
                _spreadBulletProcessInfo.bulletVelocities.Add(
                    bulletSpeeds[attackType.SpreadBullet]
                    * new Vector3(
                        Mathf.Cos(Mathf.PI / 2 + Mathf.PI * i / SPREAD_BULLET_FIRING_AMOUNT_PER_ONCE),
                        Mathf.Sin(Mathf.PI / 2 + Mathf.PI * i / SPREAD_BULLET_FIRING_AMOUNT_PER_ONCE),
                        0)
                    );
            }

            // 誘導弾の設定
            _guidedBulletProcessInfo = new BulletProcessInfo()
            {
                shortInterval_Frame = GUIDED_BULLET_SHORT_INTERVAL,
                firePath = "Boss1__" + attackType.GuidedBullet.ToString(),
                firingAmount = GUIDED_BULLET_FIRING_AMOUNT,
                bulletVelocities = new List<Vector3>(),
            };

            for (int i = 0; i < GUIDED_BULLET_FIRING_AMOUNT_PER_ONCE; i++)
            {
                //弾を発射する方向を計算
                _guidedBulletProcessInfo.bulletVelocities.Add(
                    bulletSpeeds[attackType.GuidedBullet]
                    * new Vector3(
                        Mathf.Cos(Mathf.PI / 2 + Mathf.PI * i / GUIDED_BULLET_FIRING_AMOUNT_PER_ONCE),
                        Mathf.Sin(Mathf.PI / 2 + Mathf.PI * i / GUIDED_BULLET_FIRING_AMOUNT_PER_ONCE),
                        0)
                    );
            }
        }

        public void RunEveryFrame(Vector3 position, Vector3 playerPosition)
        {
            StopOnPausing();
            ProceedAttack(position, playerPosition);
        }

        // ビームの状態を変える処理
        // 呼び出すタイミングは親クラスに管理されている
        protected override void ChangeBeamStatus(beamFiringProcesses status)
        {
            switch (_proceedingAttackTypeName)
            {
                case attackType.Beam:
                    beamStatus = status;
                    break;

                case attackType.WideBeam:
                    wideBeamStatus = status;
                    break;

                case attackType.SpreadBeam:
                    spreadBeamStatus = status;
                    break;

                default:
                    break;
            }
        }

        private void ProceedAttack(Vector3 position, Vector3 playerPosition)
        {
            // 攻撃を始める処理
            if (_time > FIRING_INTERVAL && _proceedingAttackTypeName == attackType._none)
            {
                _time = 0;
                _proceedingAttackTypeName = RandomChoosing.ChooseRandomly(_attackProbabilityRatios);

                // 攻撃開始時に処理が必要なのはミサイルだけなのでif文で済ませておく
                if (_proceedingAttackTypeName == attackType.Missile)
                {
                    StartMissile(position, playerPosition);
                }
            }

            // 攻撃中でない場合は時間をカウントする
            if (_proceedingAttackTypeName == attackType._none)
            {
                _time += Time.deltaTime;
                return;
            }

            // 攻撃本体
            switch (_proceedingAttackTypeName)
            {
                case attackType.Beam:
                    ProceedBeam();
                    break;

                case attackType.Missile:
                    ProceedMissile();
                    break;

                case attackType.SpreadBullet:
                    ProceedSpreadBullet();
                    break;

                case attackType.WideBeam:
                    ProceedWideBeam();
                    break;

                case attackType.GuidedBullet:
                    ProceedGuidedBullet();
                    break;

                case attackType.SpreadBeam:
                    ProceedSpreadBeam();
                    break;
            }
        }

        // ビーム攻撃の処理はどれもほぼ同じなので、まとめられるところはまとめてしまう
        private void ProceedBeamBase(float beamNotifyingTime, float beamTime)
        {
            bool isAttacking = ProceedBeamFiring(beamNotifyingTime, beamTime);
            if (!isAttacking) _proceedingAttackTypeName = attackType._none;
        }

        // ビーム攻撃の処理
        private void ProceedBeam()
        {
            ProceedBeamBase(BEAM_NOTIFYING_TIME, BEAM_TIME);
        }

        // ミサイル攻撃を始める処理
        private void StartMissile(Vector3 position, Vector3 playerPosition)
        {
            Vector3 newPlayerPosition = new Vector3(playerPosition.x, playerPosition.y, EnemyManager.enemyPosition__z);

            // 発射方向をプレイヤがいる向きに設定
            _missileProcessInfo.bulletVelocities
                = new List<Vector3>()
                { (newPlayerPosition - position) * bulletSpeeds[attackType.Missile] / Vector3.Magnitude(newPlayerPosition - position) };
        }

        // ミサイル攻撃の処理
        private void ProceedMissile()
        {
            // 攻撃本体
            bool isAttacking = ProceedBulletFiring(_missileProcessInfo);

            // 攻撃終了時の処理
            if (!isAttacking) _proceedingAttackTypeName = attackType._none;
        }

        // 拡散弾の攻撃
        private void ProceedSpreadBullet()
        {
            // 攻撃本体
            bool isAttacking = ProceedBulletFiring(_spreadBulletProcessInfo);

            // 攻撃終了時の処理
            if (!isAttacking) _proceedingAttackTypeName = attackType._none;
        }

        // 広範囲ビームの処理
        private void ProceedWideBeam()
        {
            ProceedBeamBase(WIDE_BEAM_NOTIFYING_TIME, WIDE_BEAM_TIME);
        }

        // 誘導弾の処理
        private void ProceedGuidedBullet()
        {
            // 攻撃本体
            bool isAttacking = ProceedBulletFiring(_guidedBulletProcessInfo);

            // 攻撃終了時の処理
            if (!isAttacking) _proceedingAttackTypeName = attackType._none;
        }

        // 拡散ビームの処理
        private void ProceedSpreadBeam()
        {
            ProceedBeamBase(SPREAD_BEAM_NOTIFYING_TIME, SPREAD_BEAM_TIME);
        }
    }
}
