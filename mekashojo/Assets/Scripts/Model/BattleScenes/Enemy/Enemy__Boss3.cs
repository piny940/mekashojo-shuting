using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class Enemy__Boss3 : DamageFactorManager
    {
        private const float FIRING_INTERVAL = 5;

        // 斬撃用の定数
        private const int SLASH_SHORT_INTERVAL = 30;
        private const int SLASH_FIRING_AMOUNT = 4;
        private const int SLASH_FIRING_AMOUNT_PER_ONCE = 5;

        // 投げクナイ用の定数
        private const int KUNAI_SHORT_INTERVAL = 20;
        private const int KUNAI_FIRING_AMOUNT = 5;
        private const int KUNAI_FIRING_AMOUNT_PER_ONCE = 10;

        // 投げビームダガー用の定数
        private const int BEAM_DAGGER_SHORT_INTERVAL = 20;
        private const int BEAM_DAGGER_FIRING_AMOUNT = 5;
        private const int BEAM_DAGGER_FIRING_AMOUNT_PER_ONCE = 10;

        // 各種類の攻撃をする可能性の比
        private readonly Dictionary<attackType, float> _attackProbabilityRatios
            = new Dictionary<attackType, float>()
            {
                { attackType.Slash, 1 },
                { attackType.Kunai, 1 },
                { attackType.BeamDagger, 1 },
            };

        private attackType _proceedingAttackTypeName = attackType._none;

        private Dictionary<attackType, float> _damageAmounts { get; set; }
        private Dictionary<attackType, float> _bulletSpeeds { get; set; }
        private float _time = 0;

        private BulletProcessInfo _slashProcessInfo;
        private BulletProcessInfo _kunaiProcessInfo;
        private BulletProcessInfo _beamDaggerProcessInfo;

        public static readonly float maxHP = 3500;

        public UnityEvent<attackType> OnProceedingAttackTypeNameChanged
            = new UnityEvent<attackType>();

        public attackType proceedingAttackTypeName
        {
            get { return _proceedingAttackTypeName; }
            set
            {
                _proceedingAttackTypeName = value;
                OnProceedingAttackTypeNameChanged?.Invoke(value);
            }
        }

        public static IReadOnlyDictionary<attackType, float> damageAmounts { get; private set; }
        public static IReadOnlyDictionary<attackType, float> bulletSpeeds { get; private set; }

        // ステージ3のボスがする攻撃の種類
        public enum attackType
        {
            _none,
            Slash,
            Kunai,
            BeamDagger,
        }

        protected override DamageFactorData.damageFactorType factorType { get; set; }

        protected override void ChangeBeamStatus(beamFiringProcesses process) { }

        public Enemy__Boss3(StageStatusManager stageStatusManager, EnemyManager enemyManager, PlayerStatusManager playerStatusManager)
                : base(stageStatusManager, enemyManager, playerStatusManager)
        {
            _damageAmounts = new Dictionary<attackType, float>()
            {
                { attackType.Slash, 150 },
                { attackType.Kunai, 50 },
                { attackType.BeamDagger, 60 },
            };

            damageAmounts = new ReadOnlyDictionary<attackType, float>(_damageAmounts);

            _bulletSpeeds = new Dictionary<attackType, float>()
            {
                { attackType.Slash, 6 },
                { attackType.Kunai, 10 },
                { attackType.BeamDagger, 10 },
            };

            bulletSpeeds = new ReadOnlyDictionary<attackType, float>(_bulletSpeeds);

            factorType = DamageFactorData.damageFactorType.Boss;

            // 斬撃の設定
            _slashProcessInfo = new BulletProcessInfo()
            {
                shortInterval_Frame = SLASH_SHORT_INTERVAL,
                firePath = "Boss3__" + attackType.Slash.ToString(),
                firingAmount = SLASH_FIRING_AMOUNT,
                bulletVelocities = new List<Vector3>(),
            };

            for (int i = 0; i < SLASH_FIRING_AMOUNT_PER_ONCE; i++)
            {
                //弾を発射する方向を計算
                _slashProcessInfo.bulletVelocities.Add(
                    bulletSpeeds[attackType.Slash]
                    * new Vector3(
                        Mathf.Cos(Mathf.PI / 2 + Mathf.PI * i / SLASH_FIRING_AMOUNT_PER_ONCE),
                        Mathf.Sin(Mathf.PI / 2 + Mathf.PI * i / SLASH_FIRING_AMOUNT_PER_ONCE),
                        0)
                    );
            }

            // 投げクナイの設定
            _kunaiProcessInfo = new BulletProcessInfo()
            {
                shortInterval_Frame = KUNAI_SHORT_INTERVAL,
                firePath = "Boss3__" + attackType.Slash.ToString(),
                firingAmount = KUNAI_FIRING_AMOUNT,
                bulletVelocities = new List<Vector3>(),
            };

            for (int i = 0; i < KUNAI_FIRING_AMOUNT_PER_ONCE; i++)
            {
                //弾を発射する方向を計算
                _kunaiProcessInfo.bulletVelocities.Add(
                    bulletSpeeds[attackType.Slash]
                    * new Vector3(
                        Mathf.Cos(Mathf.PI / 2 + Mathf.PI * i / KUNAI_FIRING_AMOUNT_PER_ONCE),
                        Mathf.Sin(Mathf.PI / 2 + Mathf.PI * i / KUNAI_FIRING_AMOUNT_PER_ONCE),
                        0)
                    );
            }

            // 投げビームダガーの設定
            _beamDaggerProcessInfo = new BulletProcessInfo()
            {
                shortInterval_Frame = BEAM_DAGGER_SHORT_INTERVAL,
                firePath = "Boss3__" + attackType.BeamDagger.ToString(),
                firingAmount = BEAM_DAGGER_FIRING_AMOUNT,
                bulletVelocities = new List<Vector3>(),
            };

            for (int i = 0; i < BEAM_DAGGER_FIRING_AMOUNT_PER_ONCE; i++)
            {
                //弾を発射する方向を計算
                _beamDaggerProcessInfo.bulletVelocities.Add(
                    bulletSpeeds[attackType.BeamDagger]
                    * new Vector3(
                        Mathf.Cos(Mathf.PI / 2 + Mathf.PI * i / BEAM_DAGGER_FIRING_AMOUNT_PER_ONCE),
                        Mathf.Sin(Mathf.PI / 2 + Mathf.PI * i / BEAM_DAGGER_FIRING_AMOUNT_PER_ONCE),
                        0)
                    );
            }
        }

        public void RunEveryFrame()
        {
            StopOnPausing();
            ProceedAttack();
        }

        private void ProceedAttack()
        {
            if (!stageStatusManager.isGameGoing
                || stageStatusManager.currentStageStatus != StageStatusManager.stageStatus.BossBattle)
                return;

            // 攻撃を始める処理
            if (_time > FIRING_INTERVAL && proceedingAttackTypeName == attackType._none)
            {
                _time = 0;
                proceedingAttackTypeName = RandomChoosing.ChooseRandomly(_attackProbabilityRatios);
            }

            // 攻撃中でない場合は時間をカウントする
            if (proceedingAttackTypeName == attackType._none)
            {
                _time += Time.deltaTime;
            }

            // 攻撃本体
            ProceedBulletAttack(attackType.Slash, _slashProcessInfo);

            ProceedBulletAttack(attackType.Kunai, _kunaiProcessInfo);

            ProceedBulletAttack(attackType.BeamDagger, _beamDaggerProcessInfo);
        }

        // 弾丸系の攻撃の処理
        private void ProceedBulletAttack(attackType type, BulletProcessInfo info)
        {
            if (proceedingAttackTypeName != type) return;

            // 攻撃本体
            bool isAttacking = ProceedBulletFiring(info);

            // 攻撃終了時の処理
            if (!isAttacking) proceedingAttackTypeName = attackType._none;
        }
    }
}
