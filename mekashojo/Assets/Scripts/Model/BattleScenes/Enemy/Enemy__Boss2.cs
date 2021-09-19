using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class Enemy__Boss2 : DamageFactorManager
    {
        private const float FIRING_INTERVAL = 5;

        // 拡散バルカン用の定数
        private const int SPREAD_BALKAN_SHORT_INTERVAL = 5;
        private const int SPREAD_BALKAN_FIRING_AMOUNT = 80;
        private const int SPREAD_BALKAN_FIRING_AMOUNT_PER_ONCE = 15;

        // 拡散ミサイル用の定数
        private const int SPREAD_MISSILE_SHORT_INTERVAL = 5;
        private const int SPREAD_MISSILE_FIRING_AMOUNT = 60;
        private const int SPREAD_MISSILE_FIRING_AMOUNT_PER_ONCE = 15;

        // 拡散レーザー用の定数
        private const float BEAM_NOTIFYING_TIME = 1;
        private const float BEAM_TIME = 5;

        // 各種類の攻撃をする可能性の比
        private readonly Dictionary<attackType, float> _attackProbabilityRatios
            = new Dictionary<attackType, float>()
            {
                { attackType.SpreadBalkan, 1 },
                { attackType.SpreadMissile, 1 },
                { attackType.SpreadLaser, 1 },
            };

        private attackType _proceedingAttackTypeName = attackType._none; // 今どの攻撃をしているか
        private Dictionary<attackType, float> _damageAmounts { get; set; }
        private Dictionary<attackType, float> _bulletSpeeds { get; set; }
        private float _time = 0;

        // 各ビームの状態
        private beamFiringProcesses _spreadLaserStatus = beamFiringProcesses.HasStoppedBeam;

        private BulletProcessInfo _spreadBalkanProcessInfo;
        private BulletProcessInfo _spreadMissileProcessInfo;

        protected override DamageFactorData.damageFactorType factorType { get; set; }

        public static float maxHP = 4000;
        public static IReadOnlyDictionary<attackType, float> damageAmounts { get; private set; }
        public static IReadOnlyDictionary<attackType, float> bulletSpeeds { get; private set; }

        public UnityEvent<beamFiringProcesses> OnSpreadLaserStatusChanged
            = new UnityEvent<beamFiringProcesses>();

        public beamFiringProcesses spreadLaserStatus
        {
            get { return _spreadLaserStatus; }
            set
            {
                _spreadLaserStatus = value;
                OnSpreadLaserStatusChanged?.Invoke(value);
            }
        }

        // ステージ1のボスがする攻撃の種類
        public enum attackType
        {
            _none,
            SpreadBalkan,
            SpreadMissile,
            SpreadLaser,
        }

        public Enemy__Boss2(PauseManager pauseManager, EnemyManager enemyManager, PlayerStatusManager playerStatusManager)
                : base(pauseManager, enemyManager, playerStatusManager)
        {
            _damageAmounts = new Dictionary<attackType, float>()
            {
                { attackType.SpreadBalkan, 20 },
                { attackType.SpreadMissile, 60 },
                { attackType.SpreadLaser, 80 },
            };

            damageAmounts = new ReadOnlyDictionary<attackType, float>(_damageAmounts);

            _bulletSpeeds = new Dictionary<attackType, float>()
            {
                { attackType.SpreadBalkan, 10 },
                { attackType.SpreadMissile, 6 },
            };

            bulletSpeeds = new ReadOnlyDictionary<attackType, float>(_bulletSpeeds);

            factorType = DamageFactorData.damageFactorType.Boss;

            // 拡散バルカン発射の設定
            _spreadBalkanProcessInfo = new BulletProcessInfo()
            {
                shortInterval_Frame = SPREAD_BALKAN_SHORT_INTERVAL,
                firePath = "Boss2__" + attackType.SpreadBalkan.ToString(),
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

            // 拡散ミサイル発射の設定
            _spreadMissileProcessInfo = new BulletProcessInfo()
            {
                shortInterval_Frame = SPREAD_MISSILE_SHORT_INTERVAL,
                firePath = "Boss2__" + attackType.SpreadMissile.ToString(),
                firingAmount = SPREAD_MISSILE_FIRING_AMOUNT,
                bulletVelocities = new List<Vector3>(),
            };

            for (int i = 0; i < SPREAD_MISSILE_FIRING_AMOUNT_PER_ONCE; i++)
            {
                //弾を発射する方向を計算
                _spreadMissileProcessInfo.bulletVelocities.Add(
                    bulletSpeeds[attackType.SpreadMissile]
                    * new Vector3(
                        Mathf.Cos(Mathf.PI / 2 + Mathf.PI * i / SPREAD_MISSILE_FIRING_AMOUNT_PER_ONCE),
                        Mathf.Sin(Mathf.PI / 2 + Mathf.PI * i / SPREAD_MISSILE_FIRING_AMOUNT_PER_ONCE),
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
            if (_proceedingAttackTypeName == attackType.SpreadLaser)
                spreadLaserStatus = status;
        }

        private void ProceedAttack()
        {
            // 攻撃を始める処理
            if (_time > FIRING_INTERVAL && _proceedingAttackTypeName == attackType._none)
            {
                _time = 0;
                _proceedingAttackTypeName = RandomChoosing.ChooseRandomly(_attackProbabilityRatios);
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
                case attackType.SpreadBalkan:
                    ProceedSpreadBalkan();
                    break;

                case attackType.SpreadMissile:
                    ProceedSpreadMissile();
                    break;

                case attackType.SpreadLaser:
                    ProceedSpreadLaser();
                    break;
            }
        }

        // 拡散バルカン攻撃の処理
        private void ProceedSpreadBalkan()
        {
            // 攻撃本体
            bool isAttacking = ProceedBulletFiring(_spreadBalkanProcessInfo);

            // 攻撃終了時の処理
            if (!isAttacking) _proceedingAttackTypeName = attackType._none;
        }

        // 拡散ミサイルの攻撃
        private void ProceedSpreadMissile()
        {
            // 攻撃本体
            bool isAttacking = ProceedBulletFiring(_spreadMissileProcessInfo);

            // 攻撃終了時の処理
            if (!isAttacking) _proceedingAttackTypeName = attackType._none;
        }

        // 拡散レーザー攻撃の処理
        private void ProceedSpreadLaser()
        {
            bool isAttacking = ProceedBeamFiring(BEAM_NOTIFYING_TIME, BEAM_TIME);
            if (!isAttacking) _proceedingAttackTypeName = attackType._none;
        }
    }
}
