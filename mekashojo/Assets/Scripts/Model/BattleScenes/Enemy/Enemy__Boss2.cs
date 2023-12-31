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
        private const int SPREAD_BALKAN_FIRING_AMOUNT = 50;
        private const int SPREAD_BALKAN_FIRING_AMOUNT_PER_ONCE = 15;

        // 拡散ミサイル用の定数
        private const int SPREAD_MISSILE_SHORT_INTERVAL = 5;
        private const int SPREAD_MISSILE_FIRING_AMOUNT = 40;
        private const int SPREAD_MISSILE_FIRING_AMOUNT_PER_ONCE = 15;

        // 拡散レーザー用の定数
        private const float SPREAD_LASER_NOTIFYING_TIME = 0;
        private const float SPREAD_LASER_TIME = 5;

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

        public static readonly float maxHP = 4000;
        public static readonly float spreadBalkanDisappearTime = 0.8f;
        public static readonly float spreadMissileDisappearTime = 1f;
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

        // ステージ2のボスがする攻撃の種類
        public enum attackType
        {
            _none,
            SpreadBalkan,
            SpreadMissile,
            SpreadLaser,
        }

        public Enemy__Boss2(StageStatusManager stageStatusManager, EnemyManager enemyManager, PlayerStatusManager playerStatusManager)
                : base(stageStatusManager, enemyManager, playerStatusManager)
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
            if (!stageStatusManager.isGameGoing
                || stageStatusManager.currentStageStatus != StageStatusManager.stageStatus.BossBattle)
                return;

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
            }

            // 攻撃本体
            ProceedBulletAttack(attackType.SpreadBalkan, _spreadBalkanProcessInfo);

            ProceedBulletAttack(attackType.SpreadMissile, _spreadMissileProcessInfo);

            ProceedBeamAttack(attackType.SpreadLaser, SPREAD_LASER_NOTIFYING_TIME, SPREAD_LASER_TIME);
        }

        // ビーム系の攻撃の処理
        private void ProceedBeamAttack(attackType type, float beamNotifyingTime, float beamTime)
        {
            if (_proceedingAttackTypeName != type) return;

            bool isAttacking = ProceedBeamFiring(beamNotifyingTime, beamTime);

            if (!isAttacking) _proceedingAttackTypeName = attackType._none;
        }

        // 弾丸系の攻撃の処理
        private void ProceedBulletAttack(attackType type, BulletProcessInfo info)
        {
            if (_proceedingAttackTypeName != type) return;

            // 攻撃本体
            bool isAttacking = ProceedBulletFiring(info);

            // 攻撃終了時の処理
            if (!isAttacking) _proceedingAttackTypeName = attackType._none;
        }
    }
}
