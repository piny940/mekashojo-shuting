using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class Enemy__Boss4 : DamageFactorManager
    {
        private const float FIRING_INTERVAL = 3;

        // 敵生成用の定数
        private const float CREATE_ENEMY_SHORT_INTERVAL = 0.5f;
        private const float CREATE_ENEMY_AMOUNT = 20;

        // 攻撃力減少用の定数
        private const float POWER_REDUCTION_DURATION = 10;
        private const float POWER_REDUCTION_RATE = 0.7f;

        // 移動速度減少用の定数
        private const float SPEED_REDUCTION_DURATION = 4;
        private const float SPEED_REDUCTION_RATE = 0.5f;

        // ダメージ軽減率減少用の定数
        private const float SHIELD_REDUCTION_DURATION = 10;
        private const float SHIELD_REDUCTION_RATE = 0.5f;

        // スタン用の定数
        private const float STUN_DURATION = 2;

        // 無敵シールドの使用時間
        private const float DAMAGE_REDUCTION_RATE = 1;
        public readonly float shieldLimitTime = 10;

        public static readonly float maxHP = 10000;

        private attackType _proceedingAttackTypeName = attackType._none; // 今どの攻撃をしているか
        private float _time = 0;
        private float _shieldRestedTime = 0;
        private EnemyDamageManager _enemyDamageManager;
        private PlayerDebuffManager _playerDebuffManager;

        // 各種類の攻撃をする可能性の比
        private readonly Dictionary<attackType, float> _attackProbabilityRatios
            = new Dictionary<attackType, float>()
            {
                { attackType.CreateEnemy, 0 },
                { attackType.PowerReduction, 1 },
                { attackType.SpeedReduction, 1 },
                { attackType.ShieldReduction, 1 },
                { attackType.Stun, 1 },
                { attackType.InvincibleShield, 1 },
            };

        private Dictionary<Controller.NormalEnemyData.normalEnemyType, float> _produceProbabilityRatios
            = new Dictionary<Controller.NormalEnemyData.normalEnemyType, float>()
            {
                { Controller.NormalEnemyData.normalEnemyType.SpreadBullet, 1 },
                { Controller.NormalEnemyData.normalEnemyType.SingleBullet, 1 },
                { Controller.NormalEnemyData.normalEnemyType.StunBullet, 1 },
                { Controller.NormalEnemyData.normalEnemyType.FastBullet, 1 },
                { Controller.NormalEnemyData.normalEnemyType.SlowBullet, 1 },
                { Controller.NormalEnemyData.normalEnemyType.Missile, 1 },
                { Controller.NormalEnemyData.normalEnemyType.RepeatedFire, 1 },
                { Controller.NormalEnemyData.normalEnemyType.WideBeam, 1 },
                { Controller.NormalEnemyData.normalEnemyType.GuidedBullet, 1 },
                { Controller.NormalEnemyData.normalEnemyType.WideSpreadBullet, 1 },
                { Controller.NormalEnemyData.normalEnemyType.SelfDestruct, 1 },
            };

        protected override DamageFactorData.damageFactorType factorType { get; set; }

        protected override void ChangeBeamStatus(beamFiringProcesses process) { }

        public UnityEvent<float> OnShieldRestedTimeChanged = new UnityEvent<float>();

        public float shieldRestedTime // シールドをあと何秒使い続けることができるか
        {
            get { return _shieldRestedTime; }
            set
            {
                _shieldRestedTime = value;
                OnShieldRestedTimeChanged?.Invoke(value);
            }
        }

        // ステージ4のボスがする攻撃の種類
        public enum attackType
        {
            _none,
            CreateEnemy,
            PowerReduction,
            SpeedReduction,
            ShieldReduction,
            Stun,
            InvincibleShield,
        }

        public Enemy__Boss4(EnemyDamageManager enemyDamageManager, PlayerDebuffManager playerDebuffManager, StageStatusManager stageStatusManager, EnemyManager enemyManager, PlayerStatusManager playerStatusManager)
                : base(stageStatusManager, enemyManager, playerStatusManager)
        {
            factorType = DamageFactorData.damageFactorType.Boss;
            _enemyDamageManager = enemyDamageManager;
            _playerDebuffManager = playerDebuffManager;
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
            if (_time >= FIRING_INTERVAL && _proceedingAttackTypeName == attackType._none)
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
            ProceedCreatingEnemy();

            ProceedPowerReduction();

            ProceedSpeedReduction();

            ProceedShieldReduction();

            ProceedStun();

            ProceedInvincibleShield();
        }

        // 敵生成の処理
        private void ProceedCreatingEnemy()
        {
            if (_proceedingAttackTypeName != attackType.CreateEnemy) return;

            // 攻撃本体
            bool isAttacking
                = ProceedEnemyCreating(CREATE_ENEMY_SHORT_INTERVAL, CREATE_ENEMY_AMOUNT, _produceProbabilityRatios);

            // 攻撃終了時の処理
            if (!isAttacking) _proceedingAttackTypeName = attackType._none;
        }

        // 攻撃力減少の処理
        private void ProceedPowerReduction()
        {
            if (_proceedingAttackTypeName != attackType.PowerReduction) return;

            _playerDebuffManager.AddDebuff(
                    PlayerDebuffManager.debuffTypes.PowerReduction,
                    POWER_REDUCTION_DURATION,
                    POWER_REDUCTION_RATE
                    );

            _proceedingAttackTypeName = attackType._none;
        }

        // 移動速度減少の処理
        private void ProceedSpeedReduction()
        {
            if (_proceedingAttackTypeName != attackType.SpeedReduction) return;

            _playerDebuffManager.AddDebuff(
                    PlayerDebuffManager.debuffTypes.SpeedReduction,
                    SPEED_REDUCTION_DURATION,
                    SPEED_REDUCTION_RATE
                    );

            _proceedingAttackTypeName = attackType._none;
        }

        // ダメージ軽減率減少の処理
        private void ProceedShieldReduction()
        {
            if (_proceedingAttackTypeName != attackType.ShieldReduction) return;

            _playerDebuffManager.AddDebuff(
                    PlayerDebuffManager.debuffTypes.ShieldReduction,
                    SHIELD_REDUCTION_DURATION,
                    SHIELD_REDUCTION_RATE
                    );

            _proceedingAttackTypeName = attackType._none;
        }

        // スタンの処理
        private void ProceedStun()
        {
            if (_proceedingAttackTypeName != attackType.Stun) return;

            _playerDebuffManager.AddDebuff(
                    PlayerDebuffManager.debuffTypes.Stun,
                    STUN_DURATION
                    );

            _proceedingAttackTypeName = attackType._none;
        }

        // 無敵シールドの処理
        private void ProceedInvincibleShield()
        {
            // 抽選で無敵シールドの使用が選ばれたら、シールドを使い始める
            if (_proceedingAttackTypeName == attackType.InvincibleShield)
            {
                _proceedingAttackTypeName = attackType._none;

                // シールド使用中に再度抽選で無敵シールドが選ばれた場合、即座に他の攻撃に移るようにしてある
                // (_timeをFIRING_INTERVALにすれば次のフレームで再度抽選が行われる)
                if (shieldRestedTime > 0) _time = FIRING_INTERVAL;
                else
                {
                    shieldRestedTime = shieldLimitTime;
                    _enemyDamageManager.damageReductionRate = DAMAGE_REDUCTION_RATE;
                }
            }

            // シールド使用中の処理
            if (shieldRestedTime > 0) shieldRestedTime -= Time.deltaTime;

            // シールド使用終了時の処理
            if (shieldRestedTime < 0
                && _enemyDamageManager.damageReductionRate == DAMAGE_REDUCTION_RATE)
            {
                _enemyDamageManager.damageReductionRate = 0;
            }
        }
    }
}
